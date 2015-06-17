#ifndef _CARD_H
#define _CARD_H

#include <string>
#include <time.h>
#include "status_code.h"

using std::string;

class Card
{
public:
	Card();
	Card(char color, char type);
	~Card();
	string to_string();
	char getColor();
	char getType();
	void setType(char type);
	void setColor(char color);

private:
	char _type;
	char _color;
};

#endif