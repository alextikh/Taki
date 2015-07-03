#ifndef _ROOM_H
#define _ROOM_H

#include <vector>
#include <string>
#include <map>
#include <time.h>
#include <iostream>
#include "Card.h"
#include "User.h"
#include "status_code.h"

using std::vector;
using std::string;
using std::map;
using std::pair;
class User;

#define MAX_PLAYERS 4
#define NUM_SHUFFLES 100
#define PLAYER_DECK_SIZE 8

class Room
{
public:
	Room(const string &room_name, User &admin);
	~Room();
	vector<User> get_players() const;
	User *get_admin() const;
	string get_room_name() const;
	int get_num_players() const;
	bool add_user(User &user);
	void delete_user(User &user);
	bool is_open() const;
	void close();
	bool is_in_room(const User &user) const;
	map<User, vector<Card>> start_game();
	Card get_random_card();
	bool play_turn(vector<Card>& moves);
	bool is_order_legal(vector<Card>& moves);
	bool draw_cards(int card_number);
	int  is_turn_legal(vector<Card>& moves);
	bool is_draw_legal(int num_of_cards);
	vector<Card> shuffle_cards(int num_of_cards);
	Card get_top_card() const;
	bool operator==(const Room &other);

private: 
	void init_bank();
	map<User, vector<Card>> shuffle_cards_start_game();

	vector<Card> bank;
	string _room_name;
	User* _admin;
	User* _players[MAX_PLAYERS];
	bool _in_game;
	int _current_player;
	int _turn_modifier;
	int _draw_counter;
	Card _last_card;
};

#endif