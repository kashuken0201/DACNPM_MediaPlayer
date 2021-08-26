using MediaPlayer.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MediaPlayer.DAL
{
    class DAL_QLSong
    {
        private static DAL_QLSong _Instance;
        public static DAL_QLSong Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_QLSong();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_QLSong()
        {

        }

        /* Get records */
        // Song
        public List<Song> GetAllSong() // homepage
        {
            List<Song> dtSong = new List<Song>();
            foreach(DataRow i in DBHelper.Instance.GetRecords("select * from BaiHat where Status = 1").Rows)
            {
                dtSong.Add(GetSong(i));
            }
            return dtSong;
        }
        public List<Song> GetAllSongAdmin()
        {
            List<Song> dtSong = new List<Song>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from BaiHat").Rows)
            {
                dtSong.Add(GetSong(i));
            }
            return dtSong;
        }
        public Song GetSong(DataRow i)
        {
            return new Song
            {
                ID_BaiHat = i["ID_Baihat"].ToString().Trim(),
                TenBaiHat = i["TenBaiHat"].ToString().Trim(),
                ID_TacGia = i["ID_TacGia"].ToString().Trim(),
                ID_CaSi = i["ID_CaSi"].ToString().Trim(),
                ID_TheLoai = i["ID_TheLoai"].ToString().Trim(),
                Link = i["Link"].ToString().Trim(),
                LuotXem = Convert.ToInt32(i["LuotXem"].ToString().Trim()),
                Status = Convert.ToInt32(i["Status"].ToString().Trim())
            };
        }

        // User
        public List<User> GetAllUser()
        {
            List<User> dtUser = new List<User>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from Users").Rows)
            {
                dtUser.Add(GetUser(i));
            }
            return dtUser;
        }
        public User GetUser(DataRow i)
        {
            return new User
            {
                ID_User = i["ID_User"].ToString().Trim(),
                ID_Name = i["ID_Name"].ToString().Trim(),
                Password = i["Password"].ToString().Trim(),
                Role = i["Roll"].ToString().Trim(),
                Email = i["Email"].ToString().Trim(),
                SDT = i["SDT"].ToString().Trim(),
                lsPlaylist = null
            };
        }

        // Gerne
        public List<Gerne> GetAllGerne()
        {
            List<Gerne> dtGerne = new List<Gerne>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from TheLoai").Rows)
            {
                dtGerne.Add(GetGerne(i));
            }
            return dtGerne;
        }
        public Gerne GetGerne(DataRow i)
        {
            return new Gerne
            {
                ID_TheLoai = i["ID_TheLoai"].ToString().Trim(),
                TenTheLoai = i["TenTheLoai"].ToString().Trim()
            };
        }

        // Singer
        public List<Singer> GetAllSinger()
        {
            List<Singer> dtSinger = new List<Singer>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from CaSi").Rows)
            {
                dtSinger.Add(GetSinger(i));
            }
            return dtSinger;
        }
        public Singer GetSinger(DataRow i)
        {
            return new Singer
            {
                ID_CaSi = i["ID_CaSi"].ToString().Trim(),
                TenCaSi = i["TenCaSi"].ToString().Trim()
            };
        }

        // Author
        public List<Author> GetAllAuthor()
        {
            List<Author> dtAuthor = new List<Author>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from TacGia").Rows)
            {
                dtAuthor.Add(GetAuthor(i));
            }
            return dtAuthor;
        }
        public Author GetAuthor(DataRow i)
        {
            return new Author
            {
                ID_TacGia = i["ID_TacGia"].ToString().Trim(),
                TenTacGia = i["TenTacGia"].ToString().Trim()
            };
        }

        // PlaylistSong
        public List<PlaylistSong> GetAllPlaylistSong()
        {
            List<PlaylistSong> dtPlaylistSong = new List<PlaylistSong>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select * from PlaylistSong").Rows)
            {
                dtPlaylistSong.Add(GetPlaylistSong(i));
            }
            return dtPlaylistSong;
        }
        public PlaylistSong GetPlaylistSong(DataRow i)
        {
            return new PlaylistSong
            {
                ID_Playlist = i["ID_Playlist"].ToString().Trim(),
                ID_BaiHat = i["ID_BaiHat"].ToString().Trim(),
            };
        }

        // Playlist
        public List<Playlist> GetAllPlaylist()
        {
            List<Playlist> dtPlaylist = new List<Playlist>();
            foreach (DataRow i in DBHelper.Instance.GetRecords("select Playlist.ID_User, Playlist.ID_Playlist, Playlist.TenPlaylist from Playlist").Rows)
            {
                dtPlaylist.Add(GetPlaylist(i));
            }
            return dtPlaylist;
        }
        public Playlist GetPlaylist(DataRow i)
        {
            return new Playlist
            {
                ID_User = i["ID_User"].ToString().Trim(),
                ID_Playlist = i["ID_Playlist"].ToString().Trim(),
                TenPlaylist = i["TenPlaylist"].ToString().Trim(),
                LsID_Song = null
            };
        }

        // Info of Song
        public List<SongInfo> GetAllSongInfo()
        {
            List<SongInfo> dtSongInfo = new List<SongInfo>();
            foreach(DataRow i in DBHelper.Instance.GetRecords("select distinct BaiHat.ID_BaiHat, BaiHat.TenBaiHat, CaSi.TenCaSi, TacGia.TenTacGia, TheLoai.TenTheLoai, BaiHat.Link, BaiHat.LuotXem, BaiHat.Status from BaiHat, CaSi, TheLoai, TacGia where BaiHat.ID_CaSi = CaSi.ID_CaSi and BaiHat.ID_TacGia = TacGia.ID_TacGia and BaiHat.ID_TheLoai = TheLoai.ID_TheLoai").Rows)
            {
                dtSongInfo.Add(GetSongInfo(i));
            }
            return dtSongInfo;
        }
        public SongInfo GetSongInfo(DataRow i)
        {
            return new SongInfo
            {
                ID_BaiHat = i["ID_BaiHat"].ToString().Trim(),
                TenBaiHat = i["TenBaiHat"].ToString().Trim(),
                CaSi = i["TenCaSi"].ToString().Trim(),
                TacGia = i["TenTacGia"].ToString().Trim(),
                TheLoai = i["TenTheLoai"].ToString().Trim(),
                Link = i["Link"].ToString().Trim(),
                LuotXem = Convert.ToInt32(i["LuotXem"].ToString().Trim()),
                Status = (Convert.ToInt32(i["Status"].ToString().Trim()) == 1) ? "Đang mở" : "Bị khóa"
            };
        }

        /* Execute data */
        // Song
        public void AddSong(Song s)
        {
            DBHelper.Instance.ExecuteDB("insert into BaiHat values ('" + s.ID_BaiHat + "',N'" + s.TenBaiHat + "','" + s.ID_TacGia + "','" + s.ID_CaSi + "','" + s.ID_TheLoai + "','" + s.Link + "', 0, 1)");
        }
        public void UpdateSong(Song s)
        {
            DBHelper.Instance.ExecuteDB("update BaiHat set TenBaiHat = N'" + s.TenBaiHat + "',ID_CaSi = '" + s.ID_CaSi + "' , ID_TacGia = '" + s.ID_TacGia + "',ID_TheLoai = '" + s.ID_TheLoai + "',Link = '" + s.Link + "', LuotXem = " + s.LuotXem + ", Status = " + s.Status + " where ID_BaiHat = '" + s.ID_BaiHat + "'");
        }
        public void DeleteSong(string id)
        {
            DBHelper.Instance.ExecuteDB("delete from BaiHat where ID_BaiHat = '" + id + "'");
        }

        // Playlist
        public void CreatePlaylist(Playlist p)
        {
            DBHelper.Instance.ExecuteDB("insert into Playlist values ('" + p.ID_User + "','" + p.ID_Playlist + "',N'" + p.TenPlaylist + "')");
        }
        public void UpdatePlaylist(Playlist p)
        {
            DBHelper.Instance.ExecuteDB("update Playlist set ID_User = '" + p.ID_User + "',ID_Playlist = '" + p.ID_Playlist + "',TenPlaylist = N'" + p.TenPlaylist + "' where ID_Playlist = '" + p.ID_Playlist + "'");
        }
        public void DelPlaylist(string id_playlist)
        {
            DBHelper.Instance.ExecuteDB("delete from Playlist where ID_Playlist = '" + id_playlist + "'");
        }
        public void AddSongToPL(string id_song, string id_playlist)
        {
            DBHelper.Instance.ExecuteDB("insert into PlaylistSong values ('" + id_playlist + "','" + id_song + "')");
        }
        public void RemoveSongFromPL(string id_song, string id_playlist)
        {
            DBHelper.Instance.ExecuteDB("delete from PlaylistSong where ID_Playlist = '" + id_playlist + "' and ID_BaiHat = '" + id_song + "'");
        }

        // Singer
        public void AddSinger(Singer s)
        {
            DBHelper.Instance.ExecuteDB("insert into CaSi values ('" + s.ID_CaSi + "',N'" + s.TenCaSi + "')");
        }
        public void UpdateSinger(Singer s)
        {
            DBHelper.Instance.ExecuteDB("update CaSi set TenCaSi = N'" + s.TenCaSi + "' where ID_CaSi='" + s.ID_CaSi + "'");
        }
        public void DeleteSinger(string ID)
        {
            DBHelper.Instance.ExecuteDB("update BaiHat set ID_CaSi = 'CS100' where ID_CaSi = '" + ID + "' ");
            DBHelper.Instance.ExecuteDB("delete from CaSi where ID_CaSi = '" + ID + "'");
        }

        // Author
        public void AddAuthor(Author a)
        {
            DBHelper.Instance.ExecuteDB("insert into TacGia values ('" + a.ID_TacGia + "',N'" + a.TenTacGia + "')");
        }
        public void UpdateAuthor(Author s)
        {
            DBHelper.Instance.ExecuteDB("update TacGia set TenTacGia = N'" + s.TenTacGia + "' where ID_TacGia='" + s.ID_TacGia + "'");
        }
        public void DeleteAuthor(string ID)
        {
            DBHelper.Instance.ExecuteDB("update BaiHat set ID_TacGia = 'TG100' where ID_TacGia = '" + ID + "' ");
            DBHelper.Instance.ExecuteDB("delete from TacGia where ID_TacGia = '" + ID + "'");
        }

        // Gerne
        public void AddGerne(Gerne g)
        {
            DBHelper.Instance.ExecuteDB("insert into TheLoai values ('" + g.ID_TheLoai + "',N'" + g.TenTheLoai + "')");
        }
        public void UpdateGerne(Gerne s)
        {
            DBHelper.Instance.ExecuteDB("update TheLoai set TenTheLoai = N'" + s.TenTheLoai + "' where ID_TheLoai='" + s.ID_TheLoai + "'");
        }
        public void DeleteGerne(string ID)
        {
            DBHelper.Instance.ExecuteDB("update BaiHat set ID_TacGia = 'TL100' where ID_TheLoai = '" + ID + "' ");
            DBHelper.Instance.ExecuteDB("delete from TheLoai where ID_TheLoai = '" + ID + "'");
        }

        // User
        public void AddUser(User u)
        {
            DBHelper.Instance.ExecuteDB("insert into Users values ('" + u.ID_User + "','" + u.ID_Name + "','" + u.Password + "','" + u.Role + "','" + u.Email + "','" + u.SDT + "')");
        }
        public void EditUser(User u)
        {
            DBHelper.Instance.ExecuteDB("update Users set Password = '" + u.Password + "', Roll = '" + u.Role + "', Email = '" + u.Email + "', SDT = '" + u.SDT + "' where ID_User = '" + u.ID_User + "'");
        }
    }
}
