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
	sqlite3_close(_db);
}

bool Manager::register_user(const User &user, const string &password)
{
	return true;
}

void Manager::login_user(const User &user)
{
	;
}

bool Manager::is_exist(const User& user) const
{
	return true;
}

void Manager::client_requests_thread(const SOCKET& sock)
{
	vector<string> argv;
	string arg, msg;
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
						;
					}
					else
					{
						msg = "@" + to_string(PGM_ERR_REGISTERR_INFO) = "|invalid number of arguments||";
						if (send(sock, msg.c_str(), msg.length(), 0) == SOCKET_ERROR)
						{
							//TODO: delete user from runtime server data
							closesocket(sock);
							return;
						}
					}
					break;
				case EN_LOGIN:
					break;
				case EN_LOGOUT:
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