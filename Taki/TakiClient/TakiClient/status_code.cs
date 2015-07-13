using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newGUI_Taki
{
    class status_code
    {

        public const string EN_REGISTER = "1";
        public const string EN_LOGIN = "2";
        public const string EN_LOGOUT = "3";

        public const string RM_ROOM_LIST = "10";
        public const string RM_CREATE_GAME = "11";
        public const string RM_JOIN_GAME = "12";
        public const string RM_START_GAME = "13";
        public const string RM_LEAVE_GAME = "14";
        public const string RM_CLOSE_GAME = "15";

        public const string GM_PLAY = "20";
        public const string GM_DRAW = "21";

        public const string CH_SEND = "30";

        public const string PGM_SCC_LOGIN = "100";
        public const string PGM_SCC_REGISTER = "101";
        public const string PGM_SCC_GAME_CREATED = "102";
        public const string PGM_SCC_GAME_JOIN = "103";
        public const string PGM_SCC_GAME_CLOSE = "104";

        public const string PGM_CTR_NEW_USER = "110";
        public const string PGM_CTR_REMOVE_USER = "111";
        public const string PGM_CTR_GAME_STARTED = "112";
        public const string PGM_CTR_ROOM_CLOSED = "113";
        public const string PGM_CTR_ROOM_LIST = "114";

        public const string PGM_ERR_LOGIN = "120";
        public const string PGM_ERR_REGISTER_INFO = "121";
        public const string PGM_ERR_NAME_TAKEN = "122";
        public const string PGM_ERR_ROOM_FULL = "123";
        public const string PGM_ERR_ROOM_NOT_FOUND = "124";
        public const string PGM_ERR_TOO_FEW_USERS = "125";
        public const string PGM_ERR_INFO_TOO_LONG = "126";
        public const string PGM_ERR_GAME_CREATED = "127";

        public const string PGM_MER_MESSAGE = "130";
        public const string PGM_MER_ACCESS = "131";

        public const string GAM_SCC_TURN = "200";
        public const string GAM_SCC_DRAW = "201";

        public const string GAM_CTR_TURN_COMPLETE = "210";
        public const string GAM_CTR_DRAW_CARDS = "211";
        public const string GAM_CTR_GAME_ENDED = "212";
        public const string GAM_CTR_TURN_ENDED = "213";

        public const string GAM_ERR_ILLEGAL_CARD = "220";
        public const string GAM_ERR_ILLEGAL_ORDER = "221";
        public const string GAM_ERR_LAST_CARD = "222";
        public const string GAM_ERROR_WRONG_DRAW = "223";

        public const string CHA_SCC = "300";
        public const string CHA_ERR = "310";

        public const string CARD_PLUS = "+";
        public const string CARD_STOP = "!";
        public const string CARD_CHANGE_DIRECTION = "<";
        public const string CARD_PLUS_2 = "$";
        public const string CARD_CHANGE_COLOR = "%";
        public const string CARD_TAKI = "^";
        public const string CARD_SUPER_TAKI = "*";

        public const string COLOR_RED = "r";
        public const string COLOR_GREEN = "g";
        public const string COLOR_YELLOW = "y";
        public const string COLOR_BLUE = "b";

        public const string ROOM_OPEN = "1";
        public const string ROOM_CLOSED = "0";

        public const int MSG_LEN = 1024;

        public const int NUM_OF_CARDS = 8;
        public const int MIN_PLAYERS_FOR_GAME = 2;
    }
}
