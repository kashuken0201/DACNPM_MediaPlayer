using MediaPlayer.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.DTO
{
    class CompareGAS: IComparer<object>
    {
        string CompareType = "";
        public CompareGAS(string type)
        {
            CompareType = type;
        }
        public int Compare(object x, object y)
        {
            if (x.GetType().Equals(new Gerne().GetType()))
            {
                if (CompareType.Equals("Mã"))
                {
                    return ((Gerne)x).ID_TheLoai.CompareTo(((Gerne)y).ID_TheLoai);
                }
                if (CompareType.Equals("Tên"))
                {
                    return ((Gerne)x).TenTheLoai.CompareTo(((Gerne)y).TenTheLoai);
                }
                return 0;
            }
            else if(x.GetType().Equals(new Singer().GetType()))
            {
                if (CompareType.Equals("Mã"))
                {
                    return ((Singer)x).ID_CaSi.CompareTo(((Singer)y).ID_CaSi);
                }
                if (CompareType.Equals("Tên"))
                {
                    return ((Singer)x).TenCaSi.CompareTo(((Singer)y).TenCaSi);
                }
                return 0;
            }
            else if (x.GetType().Equals(new Author().GetType()))
            {
                if (CompareType.Equals("Mã"))
                {
                    return ((Author)x).ID_TacGia.CompareTo(((Author)y).ID_TacGia);
                }
                if (CompareType.Equals("Tên"))
                {
                    return ((Author)x).TenTacGia.CompareTo(((Author)y).TenTacGia);
                }
                return 0;
            }
            else if (x.GetType().Equals(new User().GetType()))
            {
                if (CompareType.Equals("Tên đăng nhập"))
                {
                    return ((User)x).ID_Name.CompareTo(((User)y).ID_Name);
                }
                if (CompareType.Equals("Email"))
                {
                    return ((User)x).Email.CompareTo(((User)y).Email);
                }
                if (CompareType.Equals("Số điện thoại"))
                {
                    return ((User)x).SDT.CompareTo(((User)y).SDT);
                }
                if (CompareType.Equals("Vai trò"))
                {
                    return ((User)x).Role.CompareTo(((User)y).Role);
                }
                return 0;
            }
            else if (x.GetType().Equals(new SongModel().GetType()))
            {
                if (CompareType.Equals("0"))
                {
                    string Namex=((SongModel)x).Name;
                    string Namey=((SongModel) y).Name;
                    BLL_QLSong.convertToUnSign(Namex).ToLower();
                    BLL_QLSong.convertToUnSign(Namey).ToLower();
                    return ((string)Namex).CompareTo(((string)Namey));
                }
                return 0;
            }
            return 0;
        }
    }
}
