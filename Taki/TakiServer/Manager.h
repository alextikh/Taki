#ifndef _MANAGER_H
#define _MANAGER_H

#ifdef _MSC_VER
#define _CRT_SECURE_NO_WARNINGS
#endif

#include <WinSock2.h>
#include <algorithm>
#include <map>
#include "sqlite3.h"
#include <cstring>
#include <string>
#include <vector>
#include <cctype>
#include "User.h"
#include "Room.h"
#include "Card.h"

using std::map;
using std::vector;
using std::pair;
using std::string;
using std::stoi;
using std::to_string;
using std::all_of;
using std::find_if;
using std::next;

#define SQL_COMMAND_LEN 512
#define BUF_LEN 2048
#define INVALID_MSG_SYNTAX -1

#define ROOM_OPEN 1
#define ROOM_CLOSED 0

#define MIN_PLAYERS_FOR_GAME 2

#define MAX_CHAT_LEN 100
#define MAX_USERNAME_LEN 20
#define MAX_PASSWORD_LEN 20
#define MAX_ROOMNAME_LEN 20

class Manager
{
public:
	Manager();
	~Manager();
	void client_requests_thread(const SOCKET& sock);

private:
	User *register_user(const string &username, const string &password, const SOCKET& sock);
	User *login_user(const string &username, const string &password, const SOCKET& sock);
	int get_args(const string &msg, vector<string> &argv) const;
	int get_cards(const string &msg, vector<Card> &cards);
	string createRoomList() const;

	map<SOCKET, User *> _user_map;
	vector<Room *> _room_vector;
	sqlite3 *_db;
};

#endif