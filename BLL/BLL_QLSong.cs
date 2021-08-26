using MediaPlayer.DAL;
using MediaPlayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MediaPlayer.BLL
{
    class BLL_QLSong
    {
        private static BLL_QLSong _Instance;
        public static BLL_QLSong Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_QLSong();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_QLSong()
        {

        }

        // Get all object
        public List<Song> GetSongTo()
        {
            return DAL_QLSong.Instance.GetAllSong();
        } // lấy danh sách bài hát trong hệ thống
        public List<Song> GetSongToAdmin()
        {
            return DAL_QLSong.Instance.GetAllSongAdmin();
        } // lấy danh sách bài hát trong hệ thống
        public List<Gerne> GetGerneTo()
        {
            return DAL_QLSong.Instance.GetAllGerne();
        } // lấy danh sách thể loại trong hệ thống
        public List<Singer> GetSingerTo()
        {
            return DAL_QLSong.Instance.GetAllSinger();
        } // lấy danh sách ca sĩ trong hệ thống
        public List<User> GetUserTo()
        {
            return DAL_QLSong.Instance.GetAllUser();
        } // lấy danh sách thể loại trong hệ thống
        public List<Author> GetAuthorTo()
        {
            return DAL_QLSong.Instance.GetAllAuthor();
        } // lấy danh sách thể loại trong hệ thống
        public List<SongInfo> GetSongInfoTo()
        {
            return DAL_QLSong.Instance.GetAllSongInfo();
        }

        // Get by ID
        public List<Song> GetSongByGerne(string type)
        {
            List<Song> ls = new List<Song>();

            if (type.Equals("TLAll"))
            {
                return GetSongTo();
            }
            foreach (Song s in GetSongTo())
            {
                if (s.ID_TheLoai.Equals(type.Trim()))
                {
                    ls.Add(s);
                }
            }
            return ls;
        }
        public Song GetSongByID(string id_song)
        {
            Song s = new Song();
            foreach (Song i in DAL_QLSong.Instance.GetAllSong())
            {
                if (i.ID_BaiHat == id_song)
                {
                    s = i;
                    break;
                }
            }
            return s;
        } // lấy bài hát theo mã
        public Song GetSongAdminByID(string id_song)
        {
            Song s = new Song();
            foreach (Song i in DAL_QLSong.Instance.GetAllSongAdmin())
            {
                if (i.ID_BaiHat == id_song)
                {
                    s = i;
                    break;
                }
            }
            return s;
        } // lấy bài hát theo mã
        public Author GetAuthorByID(string id_author)
        {
            Author a = new Author();
            foreach (Author i in DAL_QLSong.Instance.GetAllAuthor())
            {
                if (i.ID_TacGia == id_author)
                {
                    a = i;
                    break;
                }
            }
            return a;
        } // lấy tác giả theo mã
        public Singer GetSingerByID(string id_singer)
        {
            Singer s = new Singer();
            foreach (Singer i in DAL_QLSong.Instance.GetAllSinger())
            {
                if (i.ID_CaSi == id_singer)
                {
                    s = i;
                    break;
                }
            }
            return s;
        } // lấy ca sĩ theo mã
        public Playlist GetPlaylistByID(string id_playlist)
        {
            Playlist p = new Playlist();
            foreach (Playlist i in DAL_QLSong.Instance.GetAllPlaylist())
            {
                if (i.ID_Playlist == id_playlist)
                {
                    p = i;
                    break;
                }
            }
            return p;
        } // lấy playlist theo mã
        public User GetUserByID(string id_user)
        {
            User u = new User();
            foreach (User i in DAL_QLSong.Instance.GetAllUser())
            {
                if (i.ID_User == id_user)
                {
                    u = i;
                    break;
                }
            }
            return u;
        } // lấy người dùng theo mã
        public User GetUserByName(string id_name)
        {
            User u = new User();
            foreach (User i in DAL_QLSong.Instance.GetAllUser())
            {
                if (i.ID_Name == id_name)
                {
                    u = i;
                    break;
                }
            }
            return u;
        }
        public List<Playlist> GetPlaylistOfUser(string id_user)
        {
            List<Playlist> ls = new List<Playlist>();
            foreach (Playlist i in DAL_QLSong.Instance.GetAllPlaylist())
            {
                if (i.ID_User.Equals(id_user))
                {
                    i.SoLuong = CountSongPL(i.ID_Playlist);
                    ls.Add(i);
                }
            }
            return ls;
        } // lẫy danh sách playlist của 1 người dùng theo mã
        public List<Song> GetSongOfPlaylist(string id_playlist)
        {
            List<Song> ls = new List<Song>();
            List<PlaylistSong> pls = new List<PlaylistSong>();
            foreach (PlaylistSong i in DAL_QLSong.Instance.GetAllPlaylistSong())
            {
                if (i.ID_Playlist.Equals(id_playlist)) pls.Add(i);
            }
            foreach (Song i in DAL_QLSong.Instance.GetAllSong())
            {
                foreach (PlaylistSong j in pls)
                {
                    if (i.ID_BaiHat.Equals(j.ID_BaiHat)) ls.Add(i);
                }
            }
            return ls;
        } // lẫy danh sách bài hát của 1 playlist theo mã

        /* excute data */
        // Playlist
        public void CreatePlaylist(Playlist p)
        {
            DAL_QLSong.Instance.CreatePlaylist(p);
        } // tạo mới 1 playlist
        public void RenamePlaylist(Playlist p)
        {
            DAL_QLSong.Instance.UpdatePlaylist(p);
        } // đổi tên 1 playlist đã có
        public void DelPlaylist(string id_playlist)
        {
            foreach (PlaylistSong ps in DAL_QLSong.Instance.GetAllPlaylistSong())
            {
                if (ps.ID_Playlist.Equals(id_playlist)) DAL_QLSong.Instance.RemoveSongFromPL(ps.ID_BaiHat, id_playlist);
            }
            DAL_QLSong.Instance.DelPlaylist(id_playlist);
        } // xóa 1 playlist
        public void AddSongToPL(string id_add, string id_playlist)
        {
            if (id_add.Contains("PL"))
            {
                foreach (Song s1 in GetSongOfPlaylist(id_add))
                {
                    if (GetSongOfPlaylist(id_playlist).Count == 0) // playlist rong
                    {
                        DAL_QLSong.Instance.AddSongToPL(s1.ID_BaiHat, id_playlist);
                        continue;
                    }
                    else
                    {
                        bool check = true;
                        foreach (Song s2 in GetSongOfPlaylist(id_playlist))
                        {
                            if (s2.ID_BaiHat.Equals(s1.ID_BaiHat))
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check == true) DAL_QLSong.Instance.AddSongToPL(s1.ID_BaiHat, id_playlist);
                    }
                }
            }
            else if (id_add.Contains("BH"))
            {
                foreach (PlaylistSong p in DAL_QLSong.Instance.GetAllPlaylistSong())
                {
                    if (p.ID_BaiHat.Equals(id_add) && p.ID_Playlist.Equals(id_playlist)) return;
                }
                DAL_QLSong.Instance.AddSongToPL(id_add, id_playlist);
            }
        } // thêm 1 bài hát hoặc nhiều bài vào playlist
        public void RemoveSongFromPL(string id_song, string id_playlist)
        {
            foreach (PlaylistSong p in DAL_QLSong.Instance.GetAllPlaylistSong())
            {
                if (p.ID_BaiHat.Equals(id_song) && p.ID_Playlist.Equals(id_playlist))
                {
                    DAL_QLSong.Instance.RemoveSongFromPL(id_song, id_playlist);
                    break;
                }
            }
        } // xóa 1 bài hát khỏi playlist
        
        public bool CheckPath(List<SongModel> NowPlaying, string path)
        {
            foreach (SongModel i in NowPlaying)
            {
                if (i.Path == path) return true;
            }
            return false;
        }
        public void AddSongToNowPlaying(string id_add, List<SongModel> NowPlaying)
        {
            // PL10001 -> PL10007
            if (id_add.Contains("PL"))
            {
                foreach (Song s in GetSongOfPlaylist(id_add))
                {
                    bool check = File.Exists(@"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3");
                    if (check == true)
                    {
                        string path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3";
                        if (!CheckPath(NowPlaying, path)) NowPlaying.Add(new SongModel(path));
                    }
                    else
                    {
                        string path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Data\" + s.ID_BaiHat + ".mp3";
                        if (!CheckPath(NowPlaying, path)) NowPlaying.Add(new SongModel(path));
                    }
                }
            }
            else if (id_add.Contains("BH"))
            {
                Song s = BLL_QLSong.Instance.GetSongByID(id_add);
                bool check = File.Exists(@"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3");
                if (check == true)
                {
                    string path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3";
                    if (!CheckPath(NowPlaying, path)) NowPlaying.Add(new SongModel(path));
                }
                else
                {
                    string path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Data\" + s.ID_BaiHat + ".mp3";
                    if (!CheckPath(NowPlaying, path)) NowPlaying.Add(new SongModel(path));
                }
            }
            else
            {
                if (!CheckPath(NowPlaying, id_add)) NowPlaying.Add(new SongModel(id_add));
            }
        } // thêm 1 bài hát hoặc nhiều bài vào playlist
        
        // Song
        public void AddSong(string name, string cs, string tg, string lk, string tl)
        {
            Song s = new Song
            {
                ID_BaiHat = "BH" + (BLL_QLSong.Instance.LastIndexOf("BaiHat") + 1).ToString(),
                ID_CaSi = cs,
                ID_TacGia = tg,
                Link = lk,
                ID_TheLoai = tl,
                TenBaiHat = name,
            };
            DAL_QLSong.Instance.AddSong(s);
        } // thêm 1 bài hát vào hệ thống
        public void UpdateView(string id)
        {
            Song s = BLL_QLSong.Instance.GetSongByID(id);
            s.LuotXem++;
            DAL_QLSong.Instance.UpdateSong(s);
        }
        public void UpdateStatus(string id)
        {
            Song s = GetSongAdminByID(id);
            if (s.Status == 1)
            {
                s.Status = 0;
            }
            else if (s.Status == 0)
            {
                s.Status = 1;
            }
            DAL_QLSong.Instance.UpdateSong(s);
        }
        public void UpdateSong(string id, string name, string cs, string tg, string lk, string tl)
        {
            Song s = new Song
            {
                ID_BaiHat = id,
                TenBaiHat = name,
                ID_CaSi = cs,
                ID_TacGia = tg,
                Link = lk,
                ID_TheLoai = tl,
                LuotXem = BLL_QLSong.Instance.GetSongByID(id).LuotXem,
                Status = BLL_QLSong.Instance.GetSongByID(id).Status
            };
            DAL_QLSong.Instance.UpdateSong(s);
        } // cập nhật thông tin bài hát
        public void DeleteSong(string id)
        {
            foreach (PlaylistSong p in DAL_QLSong.Instance.GetAllPlaylistSong())
            {
                DAL_QLSong.Instance.RemoveSongFromPL(id, p.ID_Playlist);
            }
            DAL_QLSong.Instance.DeleteSong(id);
        } // xóa 1 bài hát khỏi hệ thống

        // Author
        public void AddAuthor(string Name)
        {
            Author a = new Author
            {
                ID_TacGia = "TG" + (LastIndexOf("TacGia") + 1).ToString(),
                TenTacGia = Name
            };
            DAL_QLSong.Instance.AddAuthor(a);
        } // thêm 1 tác giả
        public void UpdateAuthor(string ID, string Name)
        {
            Author a = new Author
            {
                ID_TacGia = ID,
                TenTacGia = Name
            };
            DAL_QLSong.Instance.UpdateAuthor(a);
        } // cập nhật 1 tác giả
        public void DeleteAuthor(string ID)
        {
            DAL_QLSong.Instance.DeleteAuthor(ID);
        } // xóa 1 tác giả

        // Gerne
        public void AddGerne(string Name)
        {
            Gerne g = new Gerne
            {
                ID_TheLoai = "TL" + (LastIndexOf("TheLoai") + 1).ToString(),
                TenTheLoai = Name
            };
            DAL_QLSong.Instance.AddGerne(g);
        } // thêm 1 thể loại
        public void UpdateGerne(string ID, string Name)
        {
            Gerne g = new Gerne
            {
                ID_TheLoai = ID,
                TenTheLoai = Name
            };
            DAL_QLSong.Instance.UpdateGerne(g);
        } // cập nhật 1 thể loại
        public void DeleteGerne(string ID)
        {
            DAL_QLSong.Instance.DeleteGerne(ID);
        } // xóa 1 thể loại

        // Singer
        public void AddSinger(string Name)
        {
            Singer s = new Singer
            {
                ID_CaSi = "CS" + (LastIndexOf("CaSi") + 1).ToString(),
                TenCaSi = Name
            };
            DAL_QLSong.Instance.AddSinger(s);
        } // thêm 1 ca sĩ
        public void UpdateSinger(string ID, string Name)
        {
            Singer s = new Singer
            {
                ID_CaSi = ID,
                TenCaSi = Name
            };
            DAL_QLSong.Instance.UpdateSinger(s);
        } // cập nhật 1 ca sĩ
        public void DeleteSinger(string ID)
        {
            DAL_QLSong.Instance.DeleteSinger(ID);
        } // xóa 1 ca sĩ

        // User
        public void AddUser(User u)
        {
            DAL_QLSong.Instance.AddUser(u);
        } // thêm 1 người dùng
        public void EditUser(User u)
        {
            DAL_QLSong.Instance.EditUser(u);
        } // cập nhật 1 người dùng
        public void EditDecentralization(User u)
        {
            if (u.Role.Equals("User"))
            {
                u.Role = "Admin";
            }
            else if (u.Role.Equals("Admin"))
            {
                u.Role = "User";
            }
            else if (u.Role.Equals("Admin1"))
            {
                MessageBox.Show("Không thể thu hồi quyền hiện tại");
            }
            BLL_QLSong.Instance.EditUser(u);
        }

        // Search
        public List<User> SearchUser(string txt)
        {
            List<User> ls = new List<User>();
            foreach (User i in GetUserTo())
            {
                string s = i.ID_Name + " " + i.SDT + " " + i.Email;
                s = convertToUnSign(s).ToLower();
                txt = convertToUnSign(txt).ToLower();
                if (s.Contains(txt))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }
        public List<Gerne> SearchGerne(string txt)
        {
            List<Gerne> ls = new List<Gerne>();
            foreach (Gerne i in GetGerneTo())
            {
                string s = i.TenTheLoai;
                s = convertToUnSign(s).ToLower();
                txt = convertToUnSign(txt).ToLower();
                if (s.Contains(txt))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }
        public List<Singer> SearchSinger(string txt)
        {
            List<Singer> ls = new List<Singer>();
            foreach (Singer i in GetSingerTo())
            {
                string s = i.TenCaSi;
                s = convertToUnSign(s).ToLower();
                txt = convertToUnSign(txt).ToLower();
                if (s.Contains(txt))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }
        public List<Author> SearchAuthor(string txt)
        {
            List<Author> ls = new List<Author>();
            foreach (Author i in GetAuthorTo())
            {
                string s = i.TenTacGia;
                s = convertToUnSign(s).ToLower();
                txt = convertToUnSign(txt).ToLower();
                if (s.Contains(txt))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }
        public List<SongInfo> SearchSong(string txt, string HeadText)
        {
            List<SongInfo> ls = new List<SongInfo>();
            foreach (SongInfo i in GetSongInfoTo())
            {
                string s = "";
                switch (HeadText)
                {
                    case "Ca sĩ":
                        s = i.CaSi;
                        break;
                    case "Tác giả":
                        s = i.TacGia;
                        break;
                    case "Thể loại":
                        s = i.TheLoai;
                        break;
                    default:
                        s = i.TenBaiHat + " " + i.TacGia + " " + i.CaSi + " " + i.TheLoai;
                        break;
                }
                s = convertToUnSign(s).ToLower();
                txt = convertToUnSign(txt).ToLower();
                if (s.Contains(txt))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }
        public void Search(object ls, string txb)
        {
            if (ls.GetType().Equals(new List<Song>().GetType()))
            {
                List<Song> lsNew = new List<Song>((List<Song>)ls);
                ((List<Song>)ls).Clear();
                foreach (Song s in lsNew)
                {
                    string txt = s.TenBaiHat + " " + GetSingerByID(s.ID_CaSi).TenCaSi + " " + GetAuthorByID(s.ID_TacGia).TenTacGia;
                    txt = convertToUnSign(txt).ToLower();
                    txb = convertToUnSign(txb).ToLower();
                    if (txt.Contains(txb.Trim()))
                    {
                        ((List<Song>)ls).Add(s);
                    }
                }
            }
            else if (ls.GetType().Equals(new List<SongModel>().GetType()))
            {
                List<SongModel> lsName = (List<SongModel>)ls;
                txb = convertToUnSign(txb.ToLower());
                for (int i = 0; i < lsName.Count; i++)
                {
                    if (!convertToUnSign(lsName[i].Name.ToLower()).Contains(txb))
                    {
                        lsName.RemoveAt(i);
                    }
                }
            }
        }

        // Sort
        public List<Singer> SortSinger(List<Singer> ls, string type, bool isReverse)
        {
            ls.Sort(new CompareGAS(type));
            if (isReverse == true)
            {
                ls.Reverse();
            }
            return ls;
        }
        public List<Author> SortAuthor(List<Author> ls, string type, bool isReverse)
        {
            ls.Sort(new CompareGAS(type));
            if (isReverse == true)
            {
                ls.Reverse();
            }
            return ls;
        }
        public List<Gerne> SortGerne(List<Gerne> ls, string type, bool isReverse)
        {
            ls.Sort(new CompareGAS(type));
            if (isReverse == true)
            {
                ls.Reverse();
            }
            return ls;
        }
        public List<SongInfo> SortSongInfo(List<SongInfo> ls, string type, bool isReverse)
        {
            ls.Sort(new CompareSongInfo(type));
            if (isReverse == true)
            {
                ls.Reverse();
            }
            return ls;
        }
        public List<Song> SortViewSong(List<Song> ls)
        {
            ls.Sort(new Song());
            ls.Reverse();
            return ls;
        }
        public List<User> SortUser(List<User> ls, string type, bool isReverse)
        {
            ls.Sort(new CompareGAS(type));
            if (isReverse == true)
            {
                ls.Reverse();
            }
            return ls;
        }
        public List<SongModel> SortMyMusic(List<SongModel> ls, string type)
        {
            ls.Sort(new CompareGAS("0"));
            if (type.Equals("1")) ls.Reverse();
            return ls;
        }
        
        // Others
        public int LastIndexOf(string type)
        {
            string s = "";
            if (type.Equals("BaiHat"))
            {
                List<Song> ls = DAL_QLSong.Instance.GetAllSong();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_BaiHat;
                }
                else s = "BH10000";
            }
            else if (type.Equals("TacGia"))
            {
                List<Author> ls = DAL_QLSong.Instance.GetAllAuthor();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_TacGia;
                }
                else s = "TG100";
            }
            else if (type.Equals("CaSi"))
            {
                List<Singer> ls = DAL_QLSong.Instance.GetAllSinger();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_CaSi;
                }
                else s = "CS100";
            }
            else if (type.Equals("TheLoai"))
            {
                List<Gerne> ls = DAL_QLSong.Instance.GetAllGerne();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_TheLoai;
                }
                else s = "TL100";
            }
            else if (type.Equals("Playlist"))
            {
                List<Playlist> ls = DAL_QLSong.Instance.GetAllPlaylist();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_Playlist;
                }
                else s = "PL10000";
            }
            else if (type.Equals("User"))
            {
                List<User> ls = DAL_QLSong.Instance.GetAllUser();
                if (ls.Count > 0)
                {
                    s = ls[ls.Count - 1].ID_User;
                }
                else s = "US10000";
            }
            return Convert.ToInt32(s.Substring(2, s.Length - 2));
        } // lấy chỉ số cuối của mỗi danh sách theo từng loại
        public int CountSongPL(string id_playlist)
        {
            return GetSongOfPlaylist(id_playlist).Count;
        } // đếm số lượng bài hát của 1 playlist
        
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public string Encode(string s)
        {
            byte[] Pass = ASCIIEncoding.ASCII.GetBytes(s);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(Pass);
            string haspass = "";
            foreach (byte i in hasData)
            {
                haspass += i;
            }
            return haspass;
        }
        public bool isUnicode(string txt)
        {
            return Encoding.ASCII.GetByteCount(txt) != Encoding.UTF8.GetByteCount(txt);
        }
        public bool CheckID_Name(string id)
        {
            foreach (User i in BLL_QLSong.Instance.GetUserTo())
            {
                if (i.ID_Name.Equals(id.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckEmail(string email)
        {
            if (email.Contains("@gmail.com"))
            {
                return true;
            }
            return false;
        }
        public bool CheckPass(string pass)
        {
            if (isUnicode(pass) || pass.Length < 8)
            {
                return false;
            }
            return true;
        }
        public bool CheckSDT(string sdt)
        {
            if (sdt.Trim().All(char.IsDigit) || sdt.Length == 10)
            {

                return true;
            }
            return false;
        }
        public void DownloadSong(string link, string path)
        {
            if (!File.Exists(path))
                using (var client = new WebClient())
                {
                    client.DownloadFile(link, path);
                }
        }
        public void DeleteSong()
        {
            foreach (string files in Directory.GetFiles(@"Data\").ToList())
            {
                File.Delete(files);
            }
        }
    }
}