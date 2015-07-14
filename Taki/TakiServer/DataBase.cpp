#include "DataBase.h"


DataBase::DataBase()
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


DataBase::~DataBase()
{
	sqlite3_close(_db);
}


bool DataBase::register_user(const string &username, const string &password)
{
	int rc;
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
		return false;
	}
	return true;
}

bool DataBase::login_user(const string &username, const string &password)
{
	int rc;
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
		return false;
	}
	return true;
}

bool DataBase::add_game(const long long int startTime, const long long int endTime, const int turns)
{
	int rc;
	char sql_command[SQL_COMMAND_LEN] = "INSERT INTO games (game_start, game_end, turns_number) VALUES(";
	strcat(sql_command, to_string(startTime).c_str());
	strcat(sql_command, ", ");
	strcat(sql_command, to_string(endTime).c_str());
	strcat(sql_command, ", ");
	strcat(sql_command, to_string(turns).c_str());
	strcat(sql_command, ");");
	rc = sqlite3_exec(_db, sql_command, NULL, NULL, NULL);
	if (rc != SQLITE_OK)
	{
		return false;
	}
	return true;
}

bool DataBase::add_user_game(const string username, const bool is_winner)
{
	return true;
}