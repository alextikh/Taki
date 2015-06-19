#ifndef _ROOM_H
#define _ROOM_H

#include <vector>
#include <string>
#include <time.h>
#include <iostream>
#include "Card.h"
#include "User.h"
#include "status_code.h"

using std::vector;
using std::string;
class User;

class Room
{
public:
	Room();
	~Room();
	bool add_user(User &user);
	void delete_user(User &user);
	bool is_open() const;
	void close();
	bool is_in_room(const User &user) const;
	bool start_game();
	Card Room::get_random_card();
	bool play_turn(vector<Card>& moves);
	bool is_order_legal(vector<Card>& moves);
	bool draw_cards(int card_number);
	int  is_turn_legal(vector<Card>& moves);
	bool is_draw_legal(int num_of_cards);
	vector<Card> shuffle_cards(int num_of_cards);
	vector<vector<Card>> shuffle_cards_start_game(int num_of_players);
	void Room::init_bank();
	bool operator==(const Room &other);

private: 
	vector<Card> bank;
	string _room_name;
	User* _admin;
	User* _players[4];
	bool _in_game;
	int _current_player;
	int _turn_modifier;
	int _draw_counter;
	Card _last_card;
};

#endif