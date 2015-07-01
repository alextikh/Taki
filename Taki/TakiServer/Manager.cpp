#include "Manager.h"
#include <iostream>
Manager::Manager()
{
	int rc;
	string err;

	rc = sqlite3_open("Taki.db", &_db);

	if (rc)
	{
		err = "Can't open database: ";
		err += sqlite3_errmsg(_db);
		throw err;
	}
}

Manager::~Manager()
{
	_user_map.clear();
	_room_vector.clear();
	sqlite3_close(_db);
}

User *Manager::register_user(const string &username, const string &password, const SOCKET& sock)
{
	int rc;
	User *user;
	/*char sql_command[SQL_COMMAND_LEN] = "INSERT INTO users(username, password_hash) VALUES(\"";
	char *err = NULL;
	const char * const unique_err = "Error: UNIQUE constraint failed: users.username";
	strcat(sql_command, username.c_str());
	strcat(sql_command, "\", \"");
	strcat(sql_command, password.c_str());
	strcat(sql_command, "\");");
	if ((rc = sqlite3_exec(_db, sql_command, NULL, NULL, &err)) != SQLITE_OK &&
		strcmp(err, unique_err) == 0)
	{
		return nullptr;
	}*/
	user = new User(username, nullptr, false, sock);
	_user_map.insert(pair<SOCKET, User>(sock, *user));
	return user;
}

User *Manager::login_user(const string &username, const string &password, const SOCKET& sock)
{
	int rc;
	User *user;
	char sql_command[SQL_COMMAND_LEN] = "SELECT username, password_hash FROM users WHERE username = '";
	strcat(sql_command, username.c_str());
	strcat(sql_command, "' AND password_hash = ')");
	strcat(sql_command, password.c_str());
	strcat(sql_command, "';");
	sqlite3_exec(_db, sql_command, NULL, NULL, NULL);
	if (sqlite3_changes(_db) == 0)
	{
		return nullptr;
	}
	user = new User(username, nullptr, false, sock);
	_user_map.insert(pair<SOCKET, User>(sock, *user));
	return user;
}

void Manager::client_requests_thread(const SOCKET& sock)
{
	vector<string> argv;
	string arg, msg;
	User *user;
	int code;
	char buf[BUF_LEN];
	bool communicate = true;
	while (communicate)
	{
		if (recv(sock, buf, BUF_LEN, 0) == SOCKET_ERROR)
		{
			//TODO: delete user from runtime server data

			closesocket(sock);
			return;
		}
		if (get_args(buf, argv) != INVALID_MSG_SYNTAX)
		{
			if (argv.size() >= 0)
			{
				arg = argv.front();
				if (all_of(arg.begin(), arg.end(), [](char ch){ return isdigit(ch); }))
				{
					code = stoi(arg);
				}
				switch (code)
				{
				case EN_REGISTER:
					if (argv.size() == 3)
					{
						user = register_user(argv[1], argv[2], sock);
						if (user != nullptr)
						{
							msg = "@" + to_string(PGM_SCC_REGISTER) + "|" + createRoomList() + "|";
							if (send(sock, msg.c_str(), msg.length() + 1, 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								std::cout << WSAGetLastError();
								terminate;
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_NAME_TAKEN) + "|" + argv[1];
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
					}
						else
					{
						msg = "@" + to_string(PGM_ERR_REGISTER_INFO) = "|args|invalid number of arguments||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					break;
					//////////////////////////////////////////////////////////

				case EN_LOGIN:
					if (argv.size() == 3)
					{
						user = login_user(argv[1], argv[2], sock);
						if (user != nullptr)
						{
							msg = "@" + to_string(PGM_SCC_LOGIN) + "|" + createRoomList() + "|";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_LOGIN) = "|invalid username or password||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
					}
					else
					{
						msg = "@" + to_string(PGM_ERR_LOGIN) = "|invalid number of arguments||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					break;
					//////////////////////////////////////////////////////////////

				case EN_LOGOUT:
					if (argv.size() == 1)
					{
						Room *room_ptr;
						if ((room_ptr = user->getRoom()) != nullptr)
						{
							delete room_ptr;
							_room_vector.erase(find(_room_vector.begin(), _room_vector.end(), *room_ptr));
							msg = "@" + to_string(PGM_ERR_LOGIN) = "|invalid username or password||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
						delete user;
						_user_map.erase(sock);
					}
					break;
					//////////////////////////////////////////////////////

				case RM_ROOM_LIST:
					if (argv.size() == 1)
					{
						msg = "@" + to_string(PGM_CTR_ROOM_LIST) + "|" + createRoomList() + "|";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					break;
					///////////////////////////////////////////////////////

				case RM_CREATE_GAME:
					if (argv.size() == 2)
					{
						_room_vector.push_back(Room(argv[1], user));
						msg = "@" + to_string(PGM_SCC_GAME_CREATED) + "||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					else
					{
						msg = "@" + to_string(PGM_ERR_GAME_CREATED) = "|invalid number of arguments||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					break;
					////////////////////////////////////////////////////////////

				case RM_JOIN_GAME:
					if (argv.size() == 2)
					{
						vector<Room>::iterator it;
						if ((it = find_if(_room_vector.begin(), _room_vector.end(),
							[argv](Room currRoom){ return currRoom.get_admin()->getUserName() == argv[1]; })) != _room_vector.end())
						{
							if (it->is_open() && it->get_num_players() < MAX_PLAYERS)
							{
								msg = "@" + to_string(PGM_SCC_GAME_JOIN) + "|";
								vector<User> players = it->get_players();
								for (vector<User>::iterator it2 = players.begin(); it2 != players.end(); ++it2)
								{
									msg += it2->getUserName() + "|";
								}
								msg += "|";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									return;
								}
							}
							else
							{
								msg = "@" + to_string(PGM_ERR_ROOM_FULL) + "|" + argv[1] + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									return;
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_ROOM_NOT_FOUND) + "|" + argv[1] + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
					}
					break;
					//////////////////////////////////////////////

				case RM_START_GAME:
					if (argv.size() == 1)
					{
						vector<Room>::iterator it;
						if ((it = find_if(_room_vector.begin(), _room_vector.end(),
							[user](Room currRoom){ return currRoom.get_admin() == user; })) != _room_vector.end())
						{
							it->start_game();
							msg = "@" + to_string(PGM_CTR_GAME_STARTED) + "|";
							vector<User> players = it->get_players();
							for (vector<User>::iterator it2 = players.begin(); it2 != players.end(); ++it2)
							{
								msg += it2->getUserName() + "|";
							}
							msg += "|";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								return;
							}
						}
					}
					break;
				}
			}
		}
	}
}

int Manager::get_args(const string &msg, vector<string> &argv) const
{
	argv.clear();
	string arg;
	int i = 0;
	if (msg[i] != '@')
	{
		return INVALID_MSG_SYNTAX;
	}
	while (i < msg.length() && msg[i + 1] != '|')
	{
		arg = "";
		for (++i; i < msg.length() && msg[i] != '|'; ++i)
		{
			arg += msg[i];
		}
		argv.push_back(arg);
	}
	if (i == msg.length() - 1)
	{
		return INVALID_MSG_SYNTAX;
	}
	return !INVALID_MSG_SYNTAX;
}

string Manager::createRoomList() const
{
	string lst;
	for (vector<Room>::const_iterator it = _room_vector.begin(); it != _room_vector.end(); ++it)
	{
		lst += it->get_room_name() + "|" + it->get_admin()->getUserName() + "|" + to_string(it->get_num_players())
			+ "|" + to_string((it->is_open()) ? ROOM_OPEN : ROOM_CLOSED) + "|";
	}
	return lst;
}