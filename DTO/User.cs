using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer
{
    class User
    {
        public string ID_User { get; set; }
        public string ID_Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public List<Playlist> lsPlaylist { get; set; }
    }
}
