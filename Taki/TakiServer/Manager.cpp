#include "Manager.h"

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
	//check if username already taken
	//add username to data base
	User *user = new User(username, nullptr, false, sock);
	_user_map.insert(pair<SOCKET, User>(sock, *user));
	return user;
}

User *Manager::login_user(const string &username, const string &password, const SOCKET& sock)
{
	//check if username and password match
	User *user = new User(username, nullptr, false, sock);
	_user_map.insert(pair<SOCKET, User>(sock, *user));
	return user;
}

void Manager::client_requests_thread(const SOCKET& sock)
{
	vector<string> argv;
	string arg, msg;
	User *user;
	Room *room;
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
				argv.erase(argv.begin());
				if (all_of(arg.begin(), arg.end(), [](char ch){ return isdigit(ch); }))
				{
					code = stoi(arg);
				}
				switch (code)
				{
				case EN_REGISTER:
					if (argv.size() == 2)
					{
						user = login_user(argv[1], argv[2], sock);
						if (user == nullptr)
						{
							closesocket(sock);
							return;
						}
					}
					else
					{
						msg = "@" + to_string(PGM_ERR_REGISTER_INFO) = "|invalid number of arguments||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							return;
						}
					}
					break;
				case EN_LOGIN:
					if (argv.size() == 2)
					{
						user = register_user(argv[1], argv[2], sock);
						if (user == nullptr)
						{
							closesocket(sock);
							return;
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
				case EN_LOGOUT:
					if ((room = user->getRoom()) != nullptr)
					{
						delete room;
						_room_vector.erase(find(_room_vector.begin(), _room_vector.end(), *room));
					}
					delete user;
					_user_map.erase(sock);
					break;
				}
			}
		}
	}
}

int Manager::get_args(const string &msg, vector<string> &argv)
{
	argv.empty();
	string arg;
	int i = 0;
	if (msg[i] != '@')
	{
		return INVALID_MSG_SYNTAX;
	}
	for (++i; msg[i] != '|'; ++i);
	while (msg[i + 1] != '|' && i != msg.length() - 1)
	{
		arg = "";
		for (++i; msg[i] != '|'; ++i);
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