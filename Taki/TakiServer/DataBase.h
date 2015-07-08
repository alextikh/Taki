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

using std::string;
using std::vector;

class DataBase
{
public:
	DataBase();
	~DataBase();
	bool addGame(int startTime, int endTime, std::vector<string> players,string winner);
	bool register_user(const string &username, const string &password);
	bool login_user(const string &username, const string &password);
private:
	
	sqlite3 *_db;
};

#endif