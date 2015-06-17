#include "Room.h"


Room::Room()
{
	this->bank = vector<Card>(NUM_CARDS_IN_BANK);
}


Room::~Room()
{
}

void Room::init_bank()
{
	int c = 0;
	for (int j = 1; j < 10; j++)
	{
		for (int k = 1; k <= 4; k++)
		{
			for (int i = 0; i < 2; i++)
			{//push cards with numbers
				char type = j + '0';
				if (j == 2) type = CARD_PLUS_2;
				if (k == 1)
				{
					this->bank[c].setColor(COLOR_BLUE);
				}
				if (k == 2)
				{
					this->bank[c].setColor(COLOR_GREEN);
				}
				if (k == 3)
				{
					this->bank[c].setColor(COLOR_RED);
				}
				if (k == 4)
				{
					this->bank[c].setColor(COLOR_YELLOW);
				}
				this->bank[c].setType(type);
				c++;
			}
		}
	}
	for (int k = 1; k <= 4; k++)
	{
		for (int i = 0; i < 2; i++)
		{//push stop cards
			if (k == 1)
			{
				this->bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->bank[c].setColor(COLOR_YELLOW);
			}
			this->bank[c].setType(CARD_STOP);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push change direction cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->bank[c].setColor(COLOR_YELLOW);
			}
			this->bank[c].setType(CARD_CHANGE_DIRECTION);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push plus cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->bank[c].setColor(COLOR_YELLOW);
			}
			this->bank[c].setType(CARD_PLUS);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push Taki cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->bank[c].setColor(COLOR_YELLOW);
			}
			this->bank[c].setType(CARD_TAKI);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push cange color cards 
		this->bank[c].setColor(NO_COLOR);
		this->bank[c].setType(CARD_CANGE_COLOR);
		c++;
	}

	for (int k = 1; k <= 2; k++)
	{//push superTaki cards
		this->bank[c].setColor(NO_COLOR);
		this->bank[c].setType(CARD_SUPER_TAKI);
		c++;
	}

}
bool Room::add_user(User &user)
{
	int i = 0;
	while (_players[i] == NULL)
	{
		i++;
	}
	_players[i] = new User(user);
	if (_players[i] != NULL) return true;
	else return false;
}

Card Room::get_random_card()
{
	return this->bank[rand() % NUM_CARDS_IN_BANK];
}

void Room::delete_user(User &user)
{
	for (int i = 0; i < 4; i++)
	{
		if (_players[i] == &user)
		{
			_players[i] = NULL;
		}
	}
}

bool Room::is_open() const
{
	return _in_game;
}

void Room::close()
{
	free(_players);
}

bool Room::is_in_room(const User &user) const
{
	for (int i = 0; i < 4; i++)
	{
		if (_players[i] != NULL)
		{
			if (_players[i] == &user) return true;
		}
	}
	return false;
}

bool Room::start_game()
{
	return _in_game;
}

bool Room::play_turn(vector<Card>& moves)
{
	int count = 0;
	for (size_t i = 0; i < moves.size() - 1; i++)
	{	//if ths color or the type of the next card is the same for the current card
		if (moves[i].getColor() == moves[i + 1].getColor() || moves[i].getType() == moves[i + 1].getType() ||
			//if current card is change color or direction
			moves[i].getType() == CARD_CANGE_COLOR || moves[i].getType() == CARD_CHANGE_DIRECTION ||
			// if current card is superTaki ir Taki card and the  subsequent cards is the same color
			((moves[i].getType() == CARD_SUPER_TAKI || (moves[i].getType() == CARD_TAKI) && moves[i].getColor() == moves[i + 1].getColor())))
		{
			count++;
		}
	}
	if ((moves.size() - 1) == count) return true;
	else return false;
}

bool Room::draw_cards(int card_number)
{
	if (this->bank.size() > card_number)
	{
		return true;
	}
	else return false;
}

bool Room::is_order_legal(vector<Card>& moves)
{
	size_t j;
	bool ok = false;
	if (moves.size() != 1)
	{
		for (size_t i = 0; i < moves.size(); i++)
		{   //If after card plus has card with the same type or color
			if (moves[i].getType() == CARD_PLUS && (moves[i + 1].getColor() == moves[i].getColor() || moves[i + 1].getType() == moves[i].getType()))
			{
				ok = true;
			}
			//If after card Taki or SuperTaki have a series of normal cards
			else if (moves[i].getType() == CARD_SUPER_TAKI || moves[i].getType() == CARD_TAKI)
			{
				for (j = i; j < moves.size(); j++)
				{
					if (moves[j].getColor() == moves[j + 1].getColor() || moves[j].getType() == moves[j + 1].getType())
					{
						i++;
					}
				}
				if (i == j) ok = true;
			}
			else return false;
			return ok;
		}
	}
	else return true;
}
int Room::is_turn_legal(vector<Card>& moves)
{
	if (play_turn(moves))
	{
		if (moves[moves.size() - 1].getType() != CARD_PLUS)
		{
			if (is_order_legal(moves))
			{
				if (draw_cards(moves.size()))
				{
					return true;
				}
				else return GAM_ERROR_WRONG_DRAW;
			}
			else return GAM_ERR_ILLEGAL_ORDER;
		}
		else return GAM_ERR_LAST_CARD;
	}
	else return GAM_ERR_ILLEGAL_CARD;
}

bool Room::is_draw_legal(int num_of_cards)
{
	if (draw_cards(num_of_cards) && (num_of_cards == 1 || num_of_cards == 2)) return true;
	else return false;
}

vector<Card> Room::shuffle_cards(int num_of_cards)
{
	srand(time(NULL));
	vector<Card> shuffle_cards(num_of_cards);
	for (int i = 0; i < num_of_cards; i++)
	{
		shuffle_cards[i] = this->get_random_card();
	}
	return shuffle_cards;
}

vector<vector<Card>> Room::shuffle_cards_start_game(int num_of_players) 
{
	srand(time(NULL));
	vector<vector<Card>> game_cards(num_of_players, vector<Card>(NUM_OF_CARDS));

	for (int i = 0; i < num_of_players; i++)
	{
		for (int j = 0; j < NUM_OF_CARDS; j++)
		{
			game_cards[i][j] = this->get_random_card();
		}
	}

	for (int i = 0; i < num_of_players; i++)
	{
		std::cout << "PLAYER " << i + 1 << std::endl << std::endl;
		for (int j = 0; j < NUM_OF_CARDS; j++)
		{
			std::cout << game_cards[i][j].to_string() << std::endl;
		}
	}
	return game_cards;
}
