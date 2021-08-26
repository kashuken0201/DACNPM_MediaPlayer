using MediaPlayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer
{
    public class Song: IComparer<Song>
    {
        public string ID_BaiHat { get; set; }
        public string TenBaiHat { get; set; }
        public string ID_TacGia { get; set; }
        public string ID_CaSi { get; set; }
        public string ID_TheLoai { get; set; }
        public string Link { get; set; }
        public int LuotXem { get; set; }
        public int Status { get; set; }

        public int Compare(Song x, Song y)
        {
            return x.LuotXem.CompareTo(y.LuotXem);
        }
    }
}
