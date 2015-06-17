#ifndef _USER_H
#define _USER_H

#include <WinSock2.h>
#include <string>
#include "Room.h"

using std::string;

class Room;
class User;

class User
{
private:
	string _user_name;
	Room *_room;
	bool _is_admin;
	SOCKET _user_socket;

public:
	User();
	User(const string &user_name, const Room *room, const bool &is_admin, const SOCKET &user_socket);
	~User();
	string getUserName() const;
	Room* getRoom() const;
	void setRoom(Room *room);
	bool isAdmin() const;
	void setAdmin(const bool &is_admin);
	SOCKET getUserSocket() const;
};

#endif