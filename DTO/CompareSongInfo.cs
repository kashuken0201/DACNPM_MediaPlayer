using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.DTO
{
    class CompareSongInfo: IComparer<SongInfo>
    {
        string CompareType = "";
        public CompareSongInfo(string s)
        {
            CompareType = s;
        }

        public int Compare(SongInfo x, SongInfo y)
        {
            if (CompareType.Equals("Tên bài hát"))
            {
                return x.TenBaiHat.CompareTo(y.TenBaiHat);
            }
            if (CompareType.Equals("Tác giả"))
            {
                return x.TacGia.CompareTo(y.TacGia);
            }
            if (CompareType.Equals("Ca sĩ"))
            {
                return x.CaSi.CompareTo(y.CaSi);
            }
            if (CompareType.Equals("Thể loại"))
            {
                return x.TheLoai.CompareTo(y.TheLoai);
            }
            if (CompareType.Equals("Lượt xem"))
            {
                return x.LuotXem.CompareTo(y.LuotXem);
            }
            return 0;
        }
    }
}
