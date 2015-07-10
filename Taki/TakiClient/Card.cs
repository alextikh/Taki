using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newGUI_Taki
{
    class Card
    {
        private char _type;
        private char _color;
        string to_string()
        {
            char[] a = {_type,_color};
	        return a.ToString();
        }

        char getColor()
        {
            return _color;
        }

        char getType()
        {
            return _type;
        }

        void setType(char type)
        {
            _type = type;
        }

        void setColor(char color)
        {
            _color = color;
        }

    }
}
