#ifndef _MANAGER_H
#define _MANAGER_H

#include <WinSock2.h>
#include <algorithm>
#include <map>
#include "sqlite3.h"
#include <string>
#include <vector>
#include <cctype>
#include "User.h"
#include "Room.h"

using std::map;
using std::vector;
using std::pair;
using std::string;
using std::stoi;
using std::to_string;
using std::all_of;

#define BUF_LEN 2048
#define INVALID_MSG_SYNTAX -1

class Manager
{
public:
	Manager();
	~Manager();
	User *register_user(const string &username, const string &password, const SOCKET& sock);
	User *login_user(const string &username, const string &password, const SOCKET& sock);
	void client_requests_thread(const SOCKET& sock);

private:
	int get_args(const string &msg, vector<string> &argv);

	map<SOCKET, User> _user_map;
	vector<Room> _room_vector;
	sqlite3 *_db;
};

#endif