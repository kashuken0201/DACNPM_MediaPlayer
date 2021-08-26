using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.DTO
{
    class CBBItem
    {
        public string Value { get; set; } // keys
        public string Text { get; set; } // values
        public override string ToString()
        {
            return this.Text;
        }
    }
}
