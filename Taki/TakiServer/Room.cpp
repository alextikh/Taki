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

bool Room::is_open() const
{
	return get_num_players() < MAX_PLAYERS && !_in_game;
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

User *Room::get_curr_player() const
{
	return _players[_curr_player_index];
}

User *Room::get_winner() const
{
	return _players[winner_index];
}

Card Room::get_top_card() const
{
	return _top_card;
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

bool Room::operator==(const Room &other) const
{
	return _admin == other._admin;
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
	_draw_made = false;
	_game_dir = DIR_NORMAL;
	shuffle_cards_start_game();
}

int Room::play_turn(User *player, const Card &move)
{
	if (!_in_game)
	{
		return PGM_MER_ACCESS;
	}
	if (!(*player == *_players[_curr_player_index]))
	{
		return PGM_MER_ACCESS;
	}
	map<User *, vector<Card>>::iterator it = find_if(_players_decks.begin(), _players_decks.end(),
		[this](pair<User *, vector<Card>> curr_pair){ return *_players[_curr_player_index] == *(curr_pair.first); });
	vector<Card>::iterator it2 = find_if(it->second.begin(), it->second.end(),
		[move](Card curr_card) {return curr_card == move; });
	if (it2 == it->second.end())
	{
		return PGM_MER_ACCESS;
	}
	if (_draw_made)
	{
		return GAM_ERR_ILLEGAL_ORDER;
	}
	if (_draw_counter > 0 && move.getType() != CARD_PLUS_2)
	{
		return GAM_ERR_ILLEGAL_ORDER;
	}
	if (_curr_player_order.size() > 0 && !_open_taki && !_plus)
	{
		return GAM_ERR_ILLEGAL_ORDER;
	}
	if (move.getColor() != _top_card.getColor())
	{
		if (move.getType() == CARD_SUPER_TAKI)
		{
			_open_taki = true;
		}
		else if (move.getType() != _top_card.getType() && move.getType() != CARD_CANGE_COLOR)
		{
			return GAM_ERR_ILLEGAL_CARD;
		}
	}
	_plus = false;
	if (move.getType() == CARD_PLUS)
	{
		_plus = true;
	}
	else if (move.getType() == CARD_TAKI)
	{
		_open_taki = true;
	}
	else if (move.getType() == CARD_CHANGE_DIRECTION)
	{
		_game_dir == DIR_NORMAL ? _game_dir = DIR_CHANGE : _game_dir = DIR_NORMAL;
	}
	else if (move.getType() == CARD_STOP)
	{
		_stop = true;
	}
	else if (move.getType() == CARD_PLUS_2)
	{
		_draw_counter += 2;
	}
	if (_curr_player_order.size() > 0)
	{
		if (move.getType() != CARD_PLUS_2)
		{
			_draw_counter = 0;
		}
		else if (move.getType() != CARD_STOP)
		{
			_stop = false;
		}
		else if (move.getType() != CARD_PLUS)
		{
			_plus = false;
		}
		else if (move.getType() != CARD_CHANGE_DIRECTION)
		{
			_game_dir = DIR_NORMAL;
		}
	}
	_curr_player_order.push_back(move);
	_top_card = move;
	it->second.erase(it2);
	_used_cards.push_back(move);
	return GAM_SCC_TURN;
}

bool Room::draw_cards(User *player, vector<Card> &drawn_cards)
{
	if (_in_game)
	{
		if (*player == *_players[_curr_player_index])
		{
			if (!_draw_made && (_curr_player_order.empty() || _plus))
			{
					if (_plus)
					{
						_plus = false;
					}
					if (_draw_counter == 0)
					{
						++_draw_counter;
					}
			}
			else
			{
				return false;
			}
			map<User *, vector<Card>>::iterator it = find_if(_players_decks.begin(), _players_decks.end(),
				[this](pair<User *, vector<Card>> curr_pair){ return *_players[_curr_player_index] == *(curr_pair.first); });
			for (; !_bank.empty() && _draw_counter > 0; --_draw_counter)
			{
				drawn_cards.push_back(_bank.front());
				it->second.push_back(_bank.front());
				_bank.erase(_bank.begin());
			}
			if (_draw_counter > 0)
			{
				_bank = _used_cards;
				_used_cards.clear();
				draw_cards(player, drawn_cards);
			}
			_draw_made = true;
			return true;
		}
	}
	return false;
}

int Room::end_turn(User *player)
{
	if (*_players[_curr_player_index] == *player)
	{
		if (_in_game)
		{
			map<User *, vector<Card>>::iterator it = find_if(_players_decks.begin(), _players_decks.end(),
				[this](pair<User *, vector<Card>> curr_pair){ return *_players[_curr_player_index] == *(curr_pair.first); });
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
				return GAM_CTR_GAME_ENDED;
			}
			if (_plus && _draw_counter == 0)
			{
				return GAM_ERR_LAST_CARD;
			}
			if (_curr_player_order.empty() && !_draw_made)
			{
				return GAM_ERR_LAST_CARD;
			}
			if (_draw_counter == 0 || _curr_player_order.back().getType() == CARD_PLUS_2)
			{
				bool repeat = true;
				do
				{
					do
					{
						if (_game_dir == DIR_NORMAL)
						{
							++_curr_player_index;
						}
						else if (--_curr_player_index < 0)
						{
							_curr_player_index *= -1;
						}
						_curr_player_index %= get_num_players();
					} while (_players[_curr_player_index] == nullptr);
					if (!_stop)
					{
						repeat = false;
					}
					if (_stop)
					{
						_stop = false;
					}
				} while (repeat);
				_curr_player_order.clear();
				_open_taki = false;
				_draw_made = false;
				return GAM_SCC_TURN;
			}
			else
			{
				return GAM_ERR_LAST_CARD;
			}
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
	_top_card = _bank.front();
	_bank.erase(_bank.begin());
	while (_top_card.getType() > '9' || _top_card.getType() < '1')
	{
		_bank.push_back(_top_card);
		_top_card = _bank.front();
		_bank.erase(_bank.begin());
	}
	_used_cards.push_back(_top_card);
}