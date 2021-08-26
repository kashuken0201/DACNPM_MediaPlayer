using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer
{
    class Playlist
    {
        public string ID_Playlist { get; set; }
        public string ID_User { get; set; }
        public string TenPlaylist { get; set; }
        public List<string> LsID_Song { get; set; }
        public int SoLuong { get; set; }
    }
}
