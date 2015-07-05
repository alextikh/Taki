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
	char getColor() const;
	char getType() const;
	void setType(char type);
	void setColor(char color);
	bool operator==(const Card &other) const;

private:
	char _type;
	char _color;
};

#endif