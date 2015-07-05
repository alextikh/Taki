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

char Card::getColor() const
{
	return _color;
}

char Card::getType() const
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

bool Card::operator==(const Card &other) const
{
	return _type == other._type && _color == other._color;
}