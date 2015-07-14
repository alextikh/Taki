#include "Manager.h"

Manager::Manager()
{
}

Manager::~Manager()
{
	_user_map.clear();
	_room_vector.clear();
}

void Manager::client_requests_thread(SOCKET &sock)
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
			cout << WSAGetLastError() << endl;
			if (user != nullptr)
			{
				if (user->getRoom() != nullptr)
				{
					if (user->isAdmin())
					{
						argv.clear();
						argv.push_back(to_string(RM_CLOSE_GAME));
						close_game(sock, user, argv);
					}
					else
					{
						argv.clear();
						argv.push_back(to_string(RM_LEAVE_GAME));
						leave_game(sock, user, argv);
					}
				}
				delete user;
				_user_map.erase(sock);
			}
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
					register_user(sock, user, argv);
					break;
					//////////////////////////////////////////////////////////

				case EN_LOGIN:
					login_user(sock, user, argv);
					break;
					//////////////////////////////////////////////////////////////

				case EN_LOGOUT:
					logout(sock, user, argv);
					break;
					//////////////////////////////////////////////////////

				case RM_ROOM_LIST:
					room_list(sock, user, argv);
					break;
					///////////////////////////////////////////////////////

				case RM_CREATE_GAME:
					create_room(sock, user, argv);
					break;
					////////////////////////////////////////////////////////////

				case RM_CLOSE_GAME:
					close_game(sock, user, argv);
					break;
					////////////////////////////////////////////////////////////

				case RM_JOIN_GAME:
					join_game(sock, user, argv);
					break;
					//////////////////////////////////////////////

				case RM_LEAVE_GAME:
					leave_game(sock, user, argv);
					break;
					//////////////////////////////////////////////

				case RM_START_GAME:
					start_game(sock, user, argv);
					break;
					///////////////////////////////////////////////////////

				case GM_PLAY:
					play_card(sock, user, argv);
					break;
					//////////////////////////////////////////////////////////

				case GM_DRAW:
					draw_card(sock, user, argv);
					break;
					//////////////////////////////////////////////////////////

				case GAM_SCC_TURN:
					end_turn(sock, user, argv);
					break;
					//////////////////////////////////////////////////////////

				case CH_SEND:
					send_chat(sock, user, argv);
					break;
				}
			}
		}
	}
}

void Manager::register_user(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 3)
	{
		if (argv[1].length() <= MAX_USERNAME_LEN && argv[2].length() <= MAX_PASSWORD_LEN)
		{
			if (_db.register_user(argv[1], argv[2]))
			{
				user = new User(argv[1], nullptr, false, sock);
				_user_map[sock] = user;
				msg = "@" + to_string(PGM_SCC_REGISTER) + "|" + createRoomList() + "|";
				if (send(sock, msg.c_str(), msg.length() + 1, 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_NAME_TAKEN) + "|" + argv[1] + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
			if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
			{
				cout << WSAGetLastError() << endl;
				ExitThread(1);
			}
		}
	}
	else
	{
		sendAccessError(sock);
	}
}

void Manager::login_user(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 3)
	{
		if (argv[1].length() <= MAX_USERNAME_LEN && argv[2].length() <= MAX_USERNAME_LEN)
		{

			if (!userInMap(argv[1]) && _db.login_user(argv[1], argv[2]))
			{
				user = new User(argv[1], nullptr, false, sock);
				_user_map[sock] = user;
				msg = "@" + to_string(PGM_SCC_LOGIN) + "|" + createRoomList() + "|";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_LOGIN) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
			if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
			{
				cout << WSAGetLastError() << endl;
				ExitThread(1);
			}
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::logout(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr)
		{
			delete user;
			_user_map.erase(sock);
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::room_list(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr)
		{
			msg = "@" + to_string(PGM_CTR_ROOM_LIST) + "|" + createRoomList() + "|";
			if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
			{
				cout << WSAGetLastError() << endl;
				ExitThread(1);
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::create_room(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 2)
	{
		if (user != nullptr && user->getRoom() == nullptr)
		{
			if (argv[1].length() <= MAX_ROOMNAME_LEN)
			{
				_room_vector.push_back(new Room(argv[1], *user));
				msg = "@" + to_string(PGM_SCC_GAME_CREATED) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					closesocket(sock);
					ExitThread(1);
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					closesocket(sock);
					ExitThread(1);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::close_game(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr && user->getRoom() != nullptr && user->isAdmin())
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
				cout << WSAGetLastError() << endl;
				ExitThread(1);
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::join_game(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 2)
	{
		if (user != nullptr && user->getRoom() == nullptr)
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
						cout << WSAGetLastError() << endl;
						ExitThread(1);
					}
				}
				else
				{
					msg = "@" + to_string(PGM_ERR_ROOM_FULL) + "|" + argv[1] + "||";
					if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
					{
						cout << WSAGetLastError() << endl;
						ExitThread(1);
					}
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_ROOM_NOT_FOUND) + "|" + argv[1] + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::leave_game(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr && user->getRoom() != nullptr)
		{
			Room *room = user->getRoom();
			room->delete_user(*user);
			msg = "@" + to_string(PGM_SCC_GAME_LEAVE) + "||";
			if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
			{
				cout << WSAGetLastError() << endl;
				ExitThread(1);
			}
			vector<User *> players = room->get_players();
			if (room->in_game() && room->get_num_players() >= MIN_PLAYERS_FOR_GAME)
			{
				msg = "@" + to_string(GAM_CTR_GAME_ENDED) + "|" + (*players.begin())->getUserName() + "||";
				send((*players.begin())->getUserSocket(), msg.c_str(), msg.length(), 0);
				room->end_game();
			}
			else
			{
				msg = "@" + to_string(PGM_CTR_REMOVE_USER) + "|" + user->getUserName() + "||";
				for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
				{
					send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::start_game(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr && user->getRoom() != nullptr && user->isAdmin())
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
						if (next(it2) != player_deck.end())
						{
							msg += ",";
						}
					}
					msg += "|";
					msg += top_card.getType();
					msg += top_card.getColor();
					msg += "|" + room->get_curr_player()->getUserName() + "||";
					send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_TOO_FEW_USERS) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::play_card(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 2)
	{
		if (user != nullptr && user->getRoom() != nullptr && user->getRoom()->in_game())
		{
			Room *room = user->getRoom();
			vector<Card> played_cards;
			if (get_cards(argv[1], played_cards) != INVALID_MSG_SYNTAX)
			{
				int status = room->play_turn(user, played_cards.front());
				if (status == GAM_SCC_TURN)
				{
					msg = "@" + to_string(GAM_SCC_TURN) + "||";
					if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
					{
						closesocket(sock);
						cout << WSAGetLastError() << endl;
						ExitThread(1);
					}
					vector<User *> players = room->get_players();
					msg = "@" + to_string(GAM_CTR_TURN_COMPELTE) + "|" + played_cards.front().getType()
						+ played_cards.front().getColor() + "||";
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
						cout << WSAGetLastError() << endl;
						ExitThread(1);
					}
				}
				
				else if (status == PGM_MER_ACCESS)
				{
					sendAccessError(sock);
				}
			}
			else
			{
				sendProtocolError(sock);
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::draw_card(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr && user->getRoom() != nullptr && user->getRoom()->in_game())
		{
			Room *room = user->getRoom();
			vector<Card> drawn_cards;
			if (room->draw_cards(user, drawn_cards))
			{
				msg = "@" + to_string(GAM_SCC_DRAW) + "|";
				for (vector<Card>::iterator it = drawn_cards.begin(); it != drawn_cards.end(); ++it)
				{
					msg += it->getType();
					msg += it->getColor();
					if (next(it) != drawn_cards.end())
					{
						msg += ",";
					}
				}
				msg += "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
				msg = "@" + to_string(GAM_CTR_DRAW_CARDS) + "|" + to_string(drawn_cards.size()) + "||";
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
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::end_turn(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 1)
	{
		if (user != nullptr && user->getRoom() != nullptr && user->getRoom()->in_game())
		{
			Room *room = user->getRoom();
			int status = room->end_turn(user);
			if (status == GAM_SCC_TURN)
			{
				vector<User *> players = room->get_players();
				msg = "@" + to_string(GAM_CTR_TURN_ENDED) + "|" + room->get_curr_player()->getUserName() + "||";
				for (vector<User *>::iterator it = players.begin(); it != players.end(); ++it)
				{
					send((*it)->getUserSocket(), msg.c_str(), msg.length(), 0);
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
			else if (status == GAM_ERR_ILLEGAL_CARD)
			{
				msg = "@" + to_string(GAM_ERR_ILLEGAL_CARD) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
			else if (status == PGM_MER_ACCESS)
			{
				sendAccessError(sock);
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}

void Manager::send_chat(SOCKET &sock, User *&user, vector<string> &argv)
{
	string msg;
	if (argv.size() == 2)
	{
		if (user != nullptr && user->getRoom() != nullptr)
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
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
			else
			{
				msg = "@" + to_string(PGM_ERR_INFO_TOO_LONG) + "||";
				if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
				{
					cout << WSAGetLastError() << endl;
					ExitThread(1);
				}
			}
		}
		else
		{
			sendAccessError(sock);
		}
	}
	else
	{
		sendProtocolError(sock);
	}
}


void Manager::sendProtocolError(SOCKET &sock)
{
	string msg = "@" + to_string(PGM_MER_MESSAGE) + "||";
	if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
	{
		cout << WSAGetLastError() << endl;
		ExitThread(1);
	}
}

void Manager::sendAccessError(SOCKET &sock)
{
	string msg = "@" + to_string(PGM_MER_ACCESS) + "||";
	if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
	{
		cout << WSAGetLastError() << endl;
		ExitThread(1);
	}
}


bool Manager::userInMap(const string &username)
{
	map<SOCKET, User *>::iterator it = find_if(_user_map.begin(), _user_map.end(),
		[username](pair<SOCKET, User *> currPair){ return username == currPair.second->getUserName(); });
	return it != _user_map.end();
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
				&& type != CARD_PLUS_2 && type != CARD_TAKI && type != CARD_CANGE_COLOR && type != CARD_SUPER_TAKI) || type == '2')
			{
				return INVALID_MSG_SYNTAX;
			}
			color = msg[i + 1];
			if (color != COLOR_RED && color != COLOR_GREEN && color != COLOR_YELLOW && color != COLOR_BLUE)
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