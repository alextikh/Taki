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
	bool register_user(const User &user, const string &password);
	void login_user(const User &user);
	bool is_exist(const User &user) const;
	//User tryLogin(const string &user_name, const string &user_password);
	void client_requests_thread(const SOCKET& sock);

private:
	int get_args(const string &msg, vector<string> &argv);

	map<SOCKET, User> _user_map;
	vector<Room> _room_vector;
	sqlite3 *_db;
};

#endif