#ifndef _ROOM_H
#define _ROOM_H

#include <vector>
#include <string>
#include <algorithm>
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
using std::find_if;
class User;

#define MAX_PLAYERS 4
#define NUM_SHUFFLES 100
#define PLAYER_DECK_SIZE 8
#define DIR_NORMAL 1
#define DIR_CHANGE 0

class Room
{
public:
	Room(const string &room_name, User &admin);
	~Room();

	vector<User *> get_players() const;
	vector<string> get_players_participated() const;
	User *get_admin() const;
	string get_room_name() const;
	int get_num_players() const;
	bool is_open() const;
	bool in_game() const;
	long long int get_start_time() const;
	long long int get_end_time() const;
	int get_turns() const;

	bool get_player_deck(User *player, vector<Card> &player_deck);
	User *get_curr_player() const;
	User *get_winner() const;
	Card get_top_card() const;
	
	bool add_user(User &user);
	void delete_user(User &user);
	bool operator==(const Room &other) const;

	void start_game();
	void end_game();
	int play_turn(User *player, const Card &move);
	bool draw_cards(User *player, vector<Card> &drawed_cards);
	int end_turn(User *player);
	
private: 
	void init_bank();
	void shuffle_cards_start_game();
	void shuffle_cards();;

	vector<Card> _bank;
	vector<Card> _used_cards;
	vector<Card> _curr_player_order;
	int _curr_player_index;
	int winner_index;
	map<User *, vector<Card>> _players_decks;
	string _room_name;
	User* _admin;
	User* _players[MAX_PLAYERS];
	bool _in_game;
	int _draw_counter;
	bool _open_taki;
	bool _plus;
	bool _stop;
	int _game_dir;
	bool _game_ended;
	bool _draw_made;
	Card _top_card;
	long long int _start_time;
	long long int _end_time;
	int _turns;
	vector<string> _players_participated;
};

#endif