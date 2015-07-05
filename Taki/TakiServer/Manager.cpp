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
	int rc;
	User *user;
	char sql_command[SQL_COMMAND_LEN] = "INSERT INTO users(username, password_hash) VALUES('";
	char *err = NULL;
	const char * const unique_err = "UNIQUE constraint failed: users.username";
	strcat(sql_command, username.c_str());
	strcat(sql_command, "', '");
	strcat(sql_command, password.c_str());
	strcat(sql_command, "');");
	rc = sqlite3_exec(_db, sql_command, NULL, NULL, &err);
	if (rc != SQLITE_OK && strcmp(err, unique_err) == 0)
	{
		std::cout << err;
		return nullptr;
	}
	user = new User(username, nullptr, false, sock);
	_user_map[sock] = user;
	return user;
}

User *Manager::login_user(const string &username, const string &password, const SOCKET& sock)
{
	int rc;
	User *user;
	char sql_command[SQL_COMMAND_LEN] = "UPDATE users SET username='";
	strcat(sql_command, username.c_str());
	strcat(sql_command, "' WHERE username='");
	strcat(sql_command, username.c_str());
	strcat(sql_command, "' AND password_hash='");
	strcat(sql_command, password.c_str());
	strcat(sql_command, "';");
	rc = sqlite3_exec(_db, sql_command, NULL, NULL, NULL);
	if (sqlite3_changes(_db) == 0)
	{
		return nullptr;
	}
	if (find_if(_user_map.begin(), _user_map.end(),
		[username](pair<SOCKET, User *> currPair) { return currPair.second->getUserName() == username; }) != _user_map.end())
	{
		return nullptr;
	}
	user = new User(username, nullptr, false, sock);
	_user_map[sock] = user;
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
			closesocket(sock);
			ExitThread(1);
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
						if (argv[1].length() <= MAX_USERNAME_LEN && argv[2].length() <= MAX_PASSWORD_LEN)
						{
							user = register_user(argv[1], argv[2], sock);
							if (user != nullptr)
							{
								msg = "@" + to_string(PGM_SCC_REGISTER) + "|" + createRoomList() + "|";
								if (send(sock, msg.c_str(), msg.length() + 1, 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									std::cout << WSAGetLastError();
									ExitThread(1);
								}
							}
							else
							{
								msg = "@" + to_string(PGM_ERR_NAME_TAKEN) + "|" + argv[1];
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					//////////////////////////////////////////////////////////

				case EN_LOGIN:
					if (argv.size() == 3)
					{
						if (argv[1].length() <= MAX_USERNAME_LEN && argv[2].length() <= MAX_USERNAME_LEN)
						{
							if ((user = login_user(argv[1], argv[2], sock)) != nullptr)
							{
								msg = "@" + to_string(PGM_SCC_LOGIN) + "|" + createRoomList() + "|";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
							else
							{
								msg = "@" + to_string(PGM_ERR_LOGIN) + "|";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					//////////////////////////////////////////////////////////////

				case EN_LOGOUT:
					if (argv.size() == 1)
					{
						delete user;
						_user_map.erase(sock);
						closesocket(sock);
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
							ExitThread(1);
						}
					}
					break;
					///////////////////////////////////////////////////////

				case RM_CREATE_GAME:
					if (argv.size() == 2)
					{
						if (argv[1].length() <= MAX_ROOMNAME_LEN)
						{
							_room_vector.push_back(new Room(argv[1], *user));
							msg = "@" + to_string(PGM_SCC_GAME_CREATED) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					////////////////////////////////////////////////////////////

				case RM_CLOSE_GAME:
					if (argv.size() == 1)
					{
						if (user->isAdmin())
						{
							Room *room = user->getRoom();
							vector<Room *>::iterator it = find_if(_room_vector.begin(), _room_vector.end(),
								[room](Room *currRoom) { return *currRoom == *room; });
							room->delete_user(*user);
							vector<User *> players = room->get_players();
							msg = "@" + to_string(PGM_CTR_ROOM_CLOSED) + "||";
							for (vector<User *>::iterator it2 = players.begin(); it2 != players.end(); ++it2)
							{
								send((*it2)->getUserSocket(), msg.c_str(), msg.length(), 0);
								room->delete_user(**it2);
							}
							delete room;
							_room_vector.erase(it);
							msg = "@" + to_string(PGM_SCC_GAME_CLOSE) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					////////////////////////////////////////////////////////////

				case RM_JOIN_GAME:
					if (argv.size() == 2)
					{
						vector<Room *>::iterator it;
						if ((it = find_if(_room_vector.begin(), _room_vector.end(),
							[argv](Room *currRoom){ return currRoom->get_admin()->getUserName() == argv[1]; })) != _room_vector.end())
						{
							if ((*it)->is_open() && (*it)->get_num_players() < MAX_PLAYERS)
							{
								msg = "@" + to_string(PGM_SCC_GAME_JOIN) + "|";
								vector<User *> players = (*it)->get_players();
								string userAddedMsg = "@" + to_string(PGM_CTR_NEW_USER) + "|" + user->getUserName() + "||";
								for (vector<User *>::iterator it2 = players.begin(); it2 != players.end(); ++it2)
								{
									msg += (*it2)->getUserName() + "|";
									send((*it2)->getUserSocket(), userAddedMsg.c_str(), userAddedMsg.length(), 0);
								}
								msg += "|";
								(*it)->add_user(*user);
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
							else
							{
								msg = "@" + to_string(PGM_ERR_ROOM_FULL) + "|" + argv[1] + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_ROOM_NOT_FOUND) + "|" + argv[1] + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					//////////////////////////////////////////////

				case RM_LEAVE_GAME:
					if (argv.size() == 1)
					{
						Room *room = user->getRoom();
						room->delete_user(*user);
						vector<User *> players = room->get_players();
						msg = "@" + to_string(PGM_CTR_REMOVE_USER) + "|" + user->getUserName() + "||";
						for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
						{
							send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
						}
					}
					break;
					//////////////////////////////////////////////

				case RM_START_GAME:
					if (argv.size() == 1)
					{
						if (user->isAdmin())
						{
							Room *room = user->getRoom();
							if (room->get_num_players() >= MIN_PLAYERS_FOR_GAME)
							{
								vector<Card> player_deck;
								room->start_game();
								Card top_card = room->get_top_card();
								vector<User *> players = room->get_players();
								for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
								{
									room->get_player_deck(*it, player_deck);
									msg = "@" + to_string(PGM_CTR_GAME_STARTED) + "|" + to_string(room->get_num_players()) + "|";
									for (vector<Card>::iterator it2 = player_deck.begin(); it2 != player_deck.end(); ++it2)
									{
										msg += it2->getType();
										msg += it2->getColor();
										msg += ",";
									}
									msg += "|" + to_string(top_card.getType()) + to_string(top_card.getColor()) + "||";
									send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
								}
							}
							else
							{
								msg = "@" + to_string(PGM_ERR_TOO_FEW_USERS) + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_MER_ACCESS) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					///////////////////////////////////////////////////////

				case GM_PLAY:
					if (argv.size() == 2)
					{
						Room *room = user->getRoom();
						if (room != nullptr)
						{
							vector<Card> played_cards;
							if (get_cards(argv[1], played_cards) != INVALID_MSG_SYNTAX)
							{
								int status = room->play_turn(user, played_cards.front());
								if (status == GAM_SCC_TURN)
								{
									msg = "@" + to_string(GAM_SCC_TURN) + "|" + user->getUserName() + "||";
									if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
									{
										closesocket(sock);
										ExitThread(1);
									}
									vector<User *> players = room->get_players();
									msg = "@" + to_string(GAM_CTR_TURN_COMPELTE) + "|" + played_cards.front().getType()
										+ played_cards.front().getColor() + "|" + user->getUserName() + "||";
									for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
									{
										if (!(**it == *user))
										{
											send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
										}
									}
								}
								else if (status == GAM_ERR_ILLEGAL_CARD)
								{
									msg = "@" + to_string(GAM_ERR_ILLEGAL_CARD) + "||";
									if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
									{
										closesocket(sock);
										ExitThread(1);
									}
								}
								else if (status == GAM_ERR_ILLEGAL_ORDER)
								{
									msg = "@" + to_string(GAM_ERR_ILLEGAL_ORDER) + "||";
									if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
									{
										closesocket(sock);
										ExitThread(1);
									}
								}
								else if (status == PGM_MER_MESSAGE)
								{
									msg = "@" + to_string(PGM_MER_ACCESS) + "||";
									if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
									{
										closesocket(sock);
										ExitThread(1);
									}
								}
							}
							else
							{
								msg = "@" + to_string(PGM_MER_MESSAGE) + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_MER_ACCESS) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					//////////////////////////////////////////////////////////

				case GM_DRAW:
					if (argv.size() == 1)
					{
						Room *room = user->getRoom();
						if (room != nullptr && !room->is_open())
						{
							vector<Card> drawed_cards;
							if (room->draw_cards(user, drawed_cards))
							{
								msg = "@" + to_string(GAM_SCC_DRAW) + "|";
								for (vector<Card>::iterator it = drawed_cards.begin(); it != drawed_cards.end(); ++it)
								{
									msg += it->getType() + it->getColor() + "|";
								}
								msg += "|";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
								msg = "@" + to_string(GAM_CTR_DRAW_CARDS) + "|" + to_string(drawed_cards.size()) + "||";
								vector<User *> players = room->get_players();
								for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
								{
									if (!(**it == *user))
									{
										send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
									}
								}
							}
							else
							{
								msg = "@" + to_string(GAM_ERROR_WRONG_DRAW) + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_MER_ACCESS) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					break;
					//////////////////////////////////////////////////////////

				case GAM_SCC_TURN:
					if (argv.size() == 1)
					{
						Room *room = user->getRoom();
						if (room != nullptr)
						{
							int status = room->end_turn(user);
							if (status == GAM_SCC_TURN)
							{
								msg = "@" + to_string(GAM_SCC_TURN) + "|" + room->get_curr_player()->getUserName() + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
								vector<User *> players = room->get_players();
								msg = "@" + to_string(GAM_CTR_TURN_COMPELTE) + "|" + room->get_curr_player()->getUserName() + "||";
								for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
								{
									if (!(**it == *user))
									{
										send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
									}
								}
							}
							else if (status == GAM_CTR_GAME_ENDED)
							{
								msg = "@" + to_string(GAM_CTR_GAME_ENDED) + "|" + user->getUserName() + "||";
								vector<User *> players = room->get_players();
								for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
								{
									send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
								}
							}
							else if (status == GAM_ERR_LAST_CARD)
							{
								msg = "@" + to_string(GAM_ERR_LAST_CARD) + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
							else if (status == PGM_MER_ACCESS)
							{
								msg = "@" + to_string(PGM_MER_ACCESS) + "||";
								if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
								{
									closesocket(sock);
									ExitThread(1);
								}
							}
						}
						else
						{
							msg = "@" + to_string(PGM_MER_ACCESS) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
					else
					{
						msg = "@" + to_string(PGM_MER_MESSAGE) + "||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							closesocket(sock);
							ExitThread(1);
						}
					}
					break;
					//////////////////////////////////////////////////////////

				case CH_SEND:
					if (argv.size() == 2)
					{
						if (argv[1].length() <= MAX_CHAT_LEN)
						{
							vector<User *> players = user->getRoom()->get_players();
							msg = "@" + to_string(CH_SEND) + "|" + user->getUserName() + "|" + argv[1] + "||";
							for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
							{
								if (!(**it == *user))
								{
									send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
								}
							}
							msg = "@" + to_string(CHA_SCC) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
						else
						{
							msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
							if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
							{
								closesocket(sock);
								ExitThread(1);
							}
						}
					}
				}
			}
		}
		else
		{
			msg = "@" + to_string(PGM_MER_MESSAGE) + "||";
			if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
			{
				closesocket(sock);
				ExitThread(1);
			}
		}
	}
}

int Manager::get_args(const string &msg, vector<string> &argv) const
{
	argv.clear();
	string arg;
	unsigned int i = 0;
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
	for (vector<Room *>::const_iterator it = _room_vector.begin(); it != _room_vector.end(); ++it)
	{
		lst += (*it)->get_room_name() + "|" + (*it)->get_admin()->getUserName() + "|" + to_string((*it)->get_num_players())
			+ "|" + to_string(((*it)->is_open()) ? ROOM_OPEN : ROOM_CLOSED) + "|";
	}
	return lst;
}

int Manager::get_cards(const string &msg, vector<Card> &cards)
{
	cards.clear();
	int i = 0, j = msg.find(",", i);
	char type, color;
	while (j != msg.length())
	{
		if (j == string::npos)
		{
			j = msg.length();
		}
		if (j - i == 2)
		{
			type = msg[i];
			if (((type < '1' || type > '9') && type != CARD_PLUS && type != CARD_STOP && type != CARD_CHANGE_DIRECTION
				&& type != CARD_PLUS_2 && type != CARD_TAKI) || type == '2')
			{
				return INVALID_MSG_SYNTAX;
			}
			color = msg[i + 1];
			if (color != COLOR_RED && color != COLOR_GREEN && color != COLOR_YELLOW && color != COLOR_BLUE)
			{
				return INVALID_MSG_SYNTAX;
			}
		}
		else
		{
			if (j - i == 1)
			{
				type = msg[i];
				if (type == '%' || type == '*')
				{
					color = NO_COLOR;
				}
				else
				{
					return INVALID_MSG_SYNTAX;
				}
			}
			else
			{
				return INVALID_MSG_SYNTAX;
			}
		}
		cards.push_back(Card(color, type));
		if (j != msg.length())
		{
			i = j + 1;
			j = msg.find(",", i);
		}
	}
	return !INVALID_MSG_SYNTAX;
}