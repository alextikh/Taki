#ifndef _DATA_BASE_H
#define _DATA_BASE_H

#ifdef _MSC_VER
#define _CRT_SECURE_NO_WARNINGS
#endif

#include "User.h"
#include "sqlite3.h"
#include <vector>
#include <string>

#define SQL_COMMAND_LEN 512

#include <string>

using std::string;
using std::to_string;

class DataBase
{
public:
	DataBase();
	~DataBase();

	bool register_user(const string &username, const string &password);
	bool login_user(const string &username, const string &password);
	bool add_game(const long long int startTime, const long long int endTime, const int turns);
	bool add_user_game(const string username, const int game_id, const bool is_winner);
	int get_last_game_id();

private:
	
	sqlite3 *_db;
};

#endif