#include "User.h"

User::User(const string &user_name, const Room *room, const bool &is_admin, const SOCKET &user_socket)
	: _user_name(user_name), _room((Room *)room), _is_admin(is_admin), _user_socket(user_socket) { }

User::~User()
{
	delete _room;
}

string User::getUserName() const
{
	return _user_name;
}

Room* User::getRoom() const
{
	return _room;
}

void User::setRoom(Room *room)
{
	delete _room;
	_room = (Room *)room;
}

bool User::isAdmin() const
{
	return _is_admin;
}

void User::setAdmin(const bool &is_admin)
{
	_is_admin = is_admin;
}

SOCKET User::getUserSocket() const
{
	return _user_socket;
}

bool User::operator==(const User &other)
{
	return _user_name == other._user_name;
}