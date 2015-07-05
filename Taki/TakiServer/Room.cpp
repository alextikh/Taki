#include "Room.h"


Room::Room(const string &room_name, User &admin) : _room_name(room_name), _admin(&admin), _in_game(false)
{
	_players[0] = _admin;
	_admin->setAdmin(true);
	_admin->setRoom(this);
	for (int i = 1; i < MAX_PLAYERS; ++i)
	{
		_players[i] = nullptr;
	}
	this->_bank = vector<Card>(NUM_CARDS_IN_BANK);
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
					this->_bank[c].setColor(COLOR_BLUE);
				}
				if (k == 2)
				{
					this->_bank[c].setColor(COLOR_GREEN);
				}
				if (k == 3)
				{
					this->_bank[c].setColor(COLOR_RED);
				}
				if (k == 4)
				{
					this->_bank[c].setColor(COLOR_YELLOW);
				}
				this->_bank[c].setType(type);
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
				this->_bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->_bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->_bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->_bank[c].setColor(COLOR_YELLOW);
			}
			this->_bank[c].setType(CARD_STOP);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push change direction cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->_bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->_bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->_bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->_bank[c].setColor(COLOR_YELLOW);
			}
			this->_bank[c].setType(CARD_CHANGE_DIRECTION);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push plus cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->_bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->_bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->_bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->_bank[c].setColor(COLOR_YELLOW);
			}
			this->_bank[c].setType(CARD_PLUS);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push Taki cards
		for (int i = 0; i < 2; i++)
		{
			if (k == 1)
			{
				this->_bank[c].setColor(COLOR_BLUE);
			}
			if (k == 2)
			{
				this->_bank[c].setColor(COLOR_GREEN);
			}
			if (k == 3)
			{
				this->_bank[c].setColor(COLOR_RED);
			}
			if (k == 4)
			{
				this->_bank[c].setColor(COLOR_YELLOW);
			}
			this->_bank[c].setType(CARD_TAKI);
			c++;
		}
	}
	for (int k = 1; k <= 4; k++)
	{//push cange color cards 
		this->_bank[c].setColor(NO_COLOR);
		this->_bank[c].setType(CARD_CANGE_COLOR);
		c++;
	}

	for (int k = 1; k <= 2; k++)
	{//push superTaki cards
		this->_bank[c].setColor(NO_COLOR);
		this->_bank[c].setType(CARD_SUPER_TAKI);
		c++;
	}

}

bool Room::add_user(User &user)
{
	int i;
	for (i = 0; i < MAX_PLAYERS && _players[i] != nullptr; i++);
	if (i < MAX_PLAYERS)
	{
		user.setRoom(this);
		_players[i] = &user;
		return true;
	}
	else return false;
}

Card Room::get_random_card()
{
	return this->_bank[rand() % NUM_CARDS_IN_BANK];
}

void Room::delete_user(User &user)
{
	for (int i = 0; i < MAX_PLAYERS; i++)
	{
		if (*_players[i] == user)
		{
			if (user.isAdmin())
			{
				user.setAdmin(false);
				_admin = nullptr;
			}
			user.setRoom(nullptr);
			_players[i] = nullptr;
			for (int i = 0; i < MAX_PLAYERS - 1; ++i)
			{
				if (_players[i] == nullptr && _players[i + 1] != nullptr)
				{
					_players[i] = _players[i + 1];
					_players[i + 1] = nullptr;
				}
			}
			break;
		}
	}

}

bool Room::is_open() const
{
	return get_num_players() < MAX_PLAYERS && !_in_game;
}

void Room::close()
{
	delete[] _players;
}

bool Room::is_in_room(const User &user) const
{
	for (int i = 0; i < 4; i++)
	{
		if (_players[i] != nullptr)
		{
			if (_players[i] == &user) return true;
		}
	}
	return false;
}

void Room::start_game()
{
	_in_game = true;
	_game_ended = true;
	_draw_counter = 0;
	_curr_player_index = 0;
	_open_taki = false;
	_plus = false;
	_stop = false;
	_game_dir = DIR_NORMAL;
	shuffle_cards_start_game();
	_curr_color = _last_card.getColor();
}

int Room::play_turn(User *player, Card move)
{
	if (!_in_game)
	{
		return PGM_MER_ACCESS;
	}
	if (!(*player == *_players[_curr_player_index]))
	{
		return PGM_MER_ACCESS;
	}
	if (_draw_counter > 0 && move.getType() != CARD_PLUS_2)
	{
		return GAM_ERR_ILLEGAL_ORDER;
	}
	if (_curr_player_order.size() > 0 && !_open_taki)
	{
		return GAM_ERR_ILLEGAL_ORDER;
	}
	if (move.getColor() != _last_card.getColor())
	{
		if (move.getType() == CARD_CANGE_COLOR)
		{
			_curr_color = move.getColor();
		}
		else if (move.getType() == CARD_SUPER_TAKI)
		{
			_curr_color = move.getColor();
			_open_taki = true;
		}
		else if (move.getType() != _last_card.getType())
		{
			return GAM_ERR_ILLEGAL_CARD;
		}
	}
	else if (move.getType() == CARD_PLUS)
	{
		_plus = true;
	}
	else if (move.getType() == CARD_TAKI)
	{
		_open_taki = true;
	}
	else if (move.getType() == CARD_CHANGE_DIRECTION)
	{
		_game_dir = !_game_dir;
	}
	else if (move.getType() == CARD_STOP)
	{
		_stop = true;
	}
	_curr_player_order.push_back(move);
	_last_card = move;
	map<User *, vector<Card>>::iterator it;
	find_if(_players_decks.begin(), _players_decks.end(),
		[this](pair<User *, vector<Card>> curr_pair){ return *_players[_curr_player_index] == *(curr_pair.first); });
	vector<Card>::iterator it2 = find_if(it->second.begin(), it->second.end(),
		[move](Card curr_card) {return curr_card == move; });
	it->second.erase(it2);
	if (it->second.empty())
	{
		_in_game = false;
		_game_ended = true;
		for (winner_index = 0; winner_index < MAX_PLAYERS; ++winner_index)
		{
			if (_players[winner_index] != nullptr && *_players[winner_index] == *player)
			{
				break;
			}
		}
	}
	return GAM_SCC_TURN;
}

bool Room::draw_cards(User *player, vector<Card> &drawed_cards)
{
	if (_in_game)
	{
		if (*player == *_players[_curr_player_index])
		{
			_plus = false;
			for (; !_bank.empty() && _draw_counter > 0; --_draw_counter)
			{
				drawed_cards.push_back(_bank.front());
				_bank.erase(_bank.begin());
			}
			if (_draw_counter > 0)
			{
				_bank = _used_cards;
				_used_cards.clear();
				draw_cards(player, drawed_cards);
			}
			return true;
		}
	}
	return false;
}

int Room::is_turn_legal(vector<Card>& moves)
{
	//if (play_turn(moves))
	//{
	if (moves[moves.size() - 1].getType() != CARD_PLUS)
	{
		if (is_order_legal(moves))
		{
			//if (draw_cards(moves.size()))
			//{
			//return true;
			//}
			//else return GAM_ERROR_WRONG_DRAW;
		}
		else return GAM_ERR_ILLEGAL_ORDER;
	}
	else return GAM_ERR_LAST_CARD;
	//}
	//else return GAM_ERR_ILLEGAL_CARD;
}

void Room::shuffle_cards()
{
	for (int i = 0; i < NUM_SHUFFLES; ++i)
	{
		int loc1 = rand() % _bank.size(), loc2 = rand() % _bank.size();
		Card temp = _bank[loc1];
		_bank[loc1] = _bank[loc2];
		_bank[loc2] = temp;
	}

}

void Room::shuffle_cards_start_game()
{
	srand(time(NULL));

	init_bank();

	shuffle_cards();

	for (int i = 0; i < MAX_PLAYERS; ++i)
	{
		if (_players[i] != nullptr)
		{
			_players_decks[_players[i]] = vector<Card>();
		}
	}

	for (int i = 0; i < PLAYER_DECK_SIZE; ++i)
	{
		for (int j = 0; j < MAX_PLAYERS; ++j)
		{
		
			if (_players[j] != nullptr)
			{
				_players_decks[_players[j]].push_back(_bank.front());
				_bank.erase(_bank.begin());
			}
		}
	}
	_last_card = _bank.front();
	_bank.erase(_bank.begin());
}

bool Room::operator==(const Room &other) const
{
	return _admin == other._admin;
}

User *Room::get_admin() const
{
	return _admin;
}

string Room::get_room_name() const
{
	return _room_name;
}

int Room::get_num_players() const
{
	int num_players = 0;
	for (int i = 0; i < MAX_PLAYERS; ++i)
	{
		if (_players[i] != nullptr)
		{
			++num_players;
		}
	}
	return num_players;
}

vector<User *> Room::get_players() const
{
	vector<User *> players;
	for (int i = 0; i < MAX_PLAYERS; ++i)
	{
		if (_players[i] != nullptr)
		{
			players.push_back(_players[i]);
		}
	}
	return players;
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
				i++;
				for (j = i; j < moves.size(); j++)
				{
					if (moves[j - 1].getColor() == moves[j].getColor() || moves[j - 1].getType() == moves[j].getType())
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

Card Room::get_top_card() const
{
	return _last_card;
}

bool Room::get_player_deck(User *player, vector<Card> &player_deck)
{
	map<User *, vector<Card>>::iterator it;
	if ((it = find_if(_players_decks.begin(), _players_decks.end(),
		[player](pair<User *, vector<Card>> curr_pair){ return *player == *(curr_pair.first); }))
		!= _players_decks.end())
	{
		player_deck = it->second;
		return true;
	}
	return false;
}

int Room::end_turn(User *player)
{
	if (*_players[_curr_player_index] == *player)
	{
		if (_in_game)
		{
			if (_plus && _draw_counter == 0)
			{
				++_draw_counter;
			}
			if (_draw_counter == 0)
			{
				bool repeat = true;
				do
				{
					while (_players[_curr_player_index] == nullptr)
					{
						++_curr_player_index;
						_curr_player_index %= get_num_players();
					}
					if (!_stop)
					{
						_stop = repeat = false;
					}
				} while (repeat);
				_curr_player_order.clear();
				_open_taki = false;
				return GAM_SCC_TURN;
			}
			else
			{
				return GAM_ERR_LAST_CARD;
			}
		}
		else if (_game_ended)
		{
			return GAM_CTR_GAME_ENDED;
		}
		else
		{
			return PGM_MER_ACCESS;
		}
	}
	else
	{
		return PGM_MER_ACCESS;
	}
}

User *Room::get_winner() const
{
	return _players[winner_index];
}

User *Room::get_curr_player() const
{
	return _players[_curr_player_index];
}