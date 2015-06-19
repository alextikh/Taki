#ifndef _STATUS_CODE_H
#define _STATUS_CODE_H

#define EN_REGISTER 1
#define EN_LOGIN 2
#define EN_LOGOUT 3

#define RM_ROOM_LIST 10
#define RM_CREATE_GAME 11
#define RM_JOIN_GAME 12
#define RM_START_GAME 13
#define RM_LEAVE_GAME 14
#define RM_CLOSE_GAME 15

#define GM_PLAY 20
#define GM_DRAW 21

#define CH_SEND 30

#define PGM_SCC_LOGIN 100
#define PGM_SCC_REGISTER 101
#define PGM_SCC_GAME_CREATED 102
#define PGM_SCC_GAME_JOIN 103
#define PGM_SCC_GAME_CLOSE 104

#define PGM_CTR_NEW_USER 110
#define PGM_CTR_REMOVE_USER 111
#define PGM_CTR_GAME_STARTED 112
#define PGM_CTR_ROOM_CLOSED 113
#define PGM_CTR_ROOM_LIST 114

#define PGM_ERR_LOGIN 120
#define PGM_ERR_REGISTER_INFO 121 
#define PGM_ERR_NAME_TAKEN 122
#define PGM_ERR_ROOM_FULL 123 
#define PGM_ERR_ROOM_NOT_FOUND 124
#define PGM_ERR_TOO_FEW_USERS 125 
#define PGM_ERR_INFO_TOO_LONG 126 
#define PGM_ERR_GAME_CREATED 127

#define PGM_MER_MESSAGE 130
#define PGM_MER_ACCESS 131


#define GAM_SCC_TURN 200
#define GAM_SCC_DRAW 201

#define GAM_CTR_TURN_COMPELTE 210
#define GAM_CTR_DRAW_CARDS 211
#define GAM_CTR_GAME_ENDED 212

#define GAM_ERR_ILLEGAL_CARD 220
#define GAM_ERR_ILLEGAL_ORDER 221 
#define GAM_ERR_LAST_CARD 222
#define GAM_ERROR_WRONG_DRAW 223

#define CHA_SCC 300
#define CHA_ERR 310

#define CARD_PLUS '+'
#define CARD_STOP '!'
#define CARD_CHANGE_DIRECTION '<'
#define CARD_PLUS_2 '$'
#define CARD_CANGE_COLOR '%'
#define CARD_TAKI '^'
#define CARD_SUPER_TAKI '*'

#define NUM_OF_CARDS 8
#define NUM_CARDS_IN_BANK 110

#define NO_COLOR ' ' //for CARD_CHANGE_DIRECTION (<) and CARD_CANGE_COLOR (%)
#define COLOR_RED 'r'
#define COLOR_GREEN 'g'
#define COLOR_YELLOW 'y'
#define COLOR_BLUE 'b'

#endif