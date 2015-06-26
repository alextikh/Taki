#include "Card.h"


Card::Card()
{
}

Card::Card(char color, char type)
{
	_type = type;
	_color = color;
}

Card::~Card()
{
}

string Card::to_string()
{
	return { _type, _color };
}

char Card::getColor()
{
	return _color;
}

char Card::getType()
{
	return _type;
}

void Card::setType(char type)
{
	_type = type;
}

void Card::setColor(char color)
{
	_color = color;
}