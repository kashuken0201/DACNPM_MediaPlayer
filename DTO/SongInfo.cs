using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.DTO
{
    class SongInfo
    {
        public string ID_BaiHat { get; set; }
        public string TenBaiHat { get; set; }
        public string CaSi { get; set; }
        public string TacGia { get; set; }
        public string TheLoai { get; set; }
        public string Link { get; set; }
        public int LuotXem { get; set; }
        public string Status { get; set; }
    }
}
