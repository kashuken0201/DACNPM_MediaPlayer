using MediaPlayer.BLL;
using MediaPlayer.DTO;
using MediaPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
        private string ID_Login = "";
        private string Role = "";

        public delegate void SetInfoUser(string id_user);
        public SetInfoUser set { get; set; }


        public Form1(string role, string id) // "US1001" - "Guest"
        {
            InitializeComponent();
            Role = role.Trim();
            ID_Login = id.Trim();
            lblNameLogin.Text = "Admin";
            btnAdmin.Visible = true;
            btnDecentralize.Visible = true;
            if (role == "User")
            {
                btnAdmin.Visible = false;
                btnDecentralize.Visible = false;
                lblNameLogin.Text = BLL_QLSong.Instance.GetUserByID(ID_Login).Email;
            }
            else if (role == "Admin")
            {
                btnDecentralize.Visible = false;
            }
            if(ID_Login.Equals("Guest"))
            {
                btnAdmin.Visible = false;
                lblNameLogin.Text = "Guest";
            }
            SetGUI();  
        }
        

        // Set GUI
        private void SetGUI()
        {
            SetVisiblePanel(false, pnlHome);
            SetFill(pnlHome);
            pnlAdmin.Visible = false;
            lblID_User.Visible = false;
            btnVolume.Visible = true;
            btnMute.Visible = false;
            trbVolume.Value = 80;
            lblVolume.Text = trbVolume.Value.ToString() + "%";
            btnShuffle.Visible = true;
            btnShuffleTrue.Visible = false;
            btnRepeat.Visible = true;
            btnRepeatAll.Visible = false;
            btnRepeatOne.Visible = false;
            Status = 0;
            SetCBBHomePage();
            SetCBBMusic();
        }
        public void SetCBBHomePage()
        {
            cbbHomePage.Items.Clear();
            cbbHomePage.Items.Add(new CBBItem
            {
                Value = "TLAll",
                Text = "All"
            });
            foreach (Gerne i in BLL_QLSong.Instance.SortGerne(BLL_QLSong.Instance.GetGerneTo(), "Tên", true))
            {
                cbbHomePage.Items.Add(new CBBItem
                {
                    Value = i.ID_TheLoai,
                    Text = i.TenTheLoai
                });
            }
            cbbHomePage.SelectedIndex = 0;
        }
        private void SetCBBMusic()
        {
            cbbSortSong.Items.AddRange(new CBBItem[]
            {
                new CBBItem { Value = "0", Text = "A to Z"},
                new CBBItem { Value = "1", Text = "Z to A"},
            });
            cbbSortSong.SelectedIndex = 0;
        }
        private void SetCBBDynamic()
        {
            // set Singer of Info Song
            cbbNameSinger.Items.Clear();
            foreach (Singer i in BLL_QLSong.Instance.SortSinger(BLL_QLSong.Instance.GetSingerTo(), "Tên", true))
            {
                cbbNameSinger.Items.Add(new CBBItem
                {
                    Value = i.ID_CaSi,
                    Text = i.TenCaSi
                });

            }
            cbbNameSinger.SelectedIndex = 0;
            // set Author of Info Song
            cbbNameAuthor.Items.Clear();
            List<Author> lsAuthor = new List<Author>();
            lsAuthor = BLL_QLSong.Instance.SortAuthor(BLL_QLSong.Instance.GetAuthorTo(), "Tên", true);
            foreach (Author i in lsAuthor)
            {
                cbbNameAuthor.Items.Add(new CBBItem
                {
                    Value = i.ID_TacGia,
                    Text = i.TenTacGia
                });

            }
            cbbNameAuthor.SelectedIndex = 0;
            // set Gerne of Info Song
            cbbGerne.Items.Clear();
            List<Gerne> lsGerne = new List<Gerne>();
            lsGerne = BLL_QLSong.Instance.SortGerne(BLL_QLSong.Instance.GetGerneTo(), "Tên", true);
            foreach (Gerne i in lsGerne)
            {
                cbbGerne.Items.Add(new CBBItem
                {
                    Value = i.ID_TheLoai,
                    Text = i.TenTheLoai
                });
            }
            cbbGerne.SelectedIndex = 0;
        }
        static void Swap<T>(List<T> ls, int indexA, int indexB)
        {
            T tmp = ls[indexA];
            ls[indexA] = ls[indexB];
            ls[indexB] = tmp;
        }
        private void SetBackColor(Button btn)
        {
            btnAdmin.BackColor = Color.FromArgb(0, 0, 51);
            btnHomePage.BackColor = Color.FromArgb(0, 0, 51);
            btnMusic.BackColor = Color.FromArgb(0, 0, 51);
            btnPlaylist.BackColor = Color.FromArgb(0, 0, 51);
            btnNowPlaying.BackColor = Color.FromArgb(0, 0, 51);
            btn.BackColor = Color.FromArgb(0, 0, 102);
        }
        private void SetFill(Panel pnl)
        {
            pnlHome.Dock = DockStyle.None;
            pnlMusic.Dock = DockStyle.None; // Now
            pnlShowPL.Dock = DockStyle.None;
            pnlSongAdmin.Dock = DockStyle.None;
            pnlAddSong.Dock = DockStyle.None;
            pnlUser.Dock = DockStyle.None;
            pnlInfoUser.Dock = DockStyle.None;
            pnlGe_Au_Si.Dock = DockStyle.None;
            //grbAddSong.Dock = DockStyle.None;
            pnl.Dock = DockStyle.Fill;
        }
        private void SetVisiblePanel(bool check, Panel pnl)
        {
            pnlHome.Visible = check;
            pnlMusic.Visible = check; // Now
            pnlShowPL.Visible = check;
            pnlSongAdmin.Visible = check;
            pnlAddSong.Visible = check;
            pnlUser.Visible = check;
            pnlInfoUser.Visible = check;
            pnlGe_Au_Si.Visible = check;
            pnl.Visible = !check;
        }

        // Control Bar
        private void btnExit_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.DeleteSong();
            Application.Exit();
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pnlName_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public void Close_App()
        {
            Player.close();
        }

        private Panel pnl = new Panel();
        private void btnInfo_Click(object sender, EventArgs e)
        {
            Status = 5;
            txbMK.Enabled = true;
            txbEmail.Enabled = true;
            txbSDT.Enabled = true;
            btnCancel_User.Visible = true;
            pnlInfoUser.Dock = DockStyle.Fill;
            Form f = Application.OpenForms["frmControlLogin"];
            frmControlLogin ff = new frmControlLogin(ID_Login, Role);
            if (f == null)
            {
                this.Controls.Add(pnl);
                pnl.BringToFront();
                pnl.Size = new Size(225, 244);
                pnl.Location = new Point(270, 40);
                pnl.Visible = true;
                f = ff;
                ff.set = new frmControlLogin.SetInfo(Load_pnlInfoUser);
                ff.close = new frmControlLogin.SignOff(Close_App);
                ff.TopLevel = false;
                pnl.Controls.Add(ff);
                ff.Show();
            }
            else
            {
                f.Close();
                pnl.SendToBack();
            }
        }

        // Biến toàn cục
        // CheckRepeat = 0 thi khong, = 1 thi repeat hết, = 2 thì repeat 1 bài
        int CheckRepeat = 0;
        //0 : Homepage
        //1 : MyMusic
        //2 : NowPlaying
        //3 : PlayList
        //4 : Admin
        //5 : Info
        int Status = 0;
        int IndexOfMyMusic = 0; // vị trí bài hát hiện tại trong My Music
        List<SongModel> NowPlaying = new List<SongModel>(); // csdl + owner (path)
        List<SongModel> ListView = new List<SongModel>(); // lưu trữ đường dẫn các file nhạc

        // Homepage
        private void btnHomePage_Click(object sender, EventArgs e)
        {
            pnlAdmin.Visible = false;
            SetBackColor(btnHomePage);
            SetVisiblePanel(false, pnlHome);
            SetFill(pnlHome);
            Status = 0;
            tbxSearch.Text = "Search";
        }
        public void Load_HomePage(string type)
        {
            if (InternetConnection.Instance.isConnected())
            {
                pnlHomeSong.Controls.Clear();
                List<Song> L = new List<Song>();
                L = BLL_QLSong.Instance.GetSongByGerne(type);
                L = BLL_QLSong.Instance.SortViewSong(L);
                if (!tbxSearch.Text.Trim().Equals("Search"))
                {
                    BLL_QLSong.Instance.Search(L, tbxSearch.Text.Trim());
                }
                if (L.Count > 0)
                {
                    int iy = 0;
                    foreach (Song i in L)
                    {
                        SongModel s = new SongModel(i);
                        s.ShowHomePage(i, pnlHomeSong, iy, btnSongHome_Click, btnMusicPlay_Click, btnAddSongToPL_Click, btnDownload_Click, Status);
                        iy++;
                    }
                }
            }
        }
        private void btnSongHome_Click(object sender, EventArgs e)
        {
            if (Status != 2)
            {
                NowPlaying = new List<SongModel>();
                foreach (SongModel i in ListView)
                {
                    NowPlaying.Add(i);
                }
                //NowPlaying = new List<string>(lsPathPL);
            }
            Song s = BLL_QLSong.Instance.GetSongByID(((Button)sender).Name.Trim());
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Data\" + s.ID_BaiHat + ".mp3";
            if (Status == 0)
            {
                NowPlaying.Clear();
                NowPlaying.Add(new SongModel(path));
            }
            BLL_QLSong.Instance.DownloadSong(s.Link.Trim(), path.Trim());
            if (Status == 2)
            {
                int index = -1, pre = -1;
                for (int i = 0; i < NowPlaying.Count; i++)
                {
                    if (NowPlaying[i].Path == Player.currentMedia.sourceURL)
                    {
                        pre = i;
                        NowPlaying[pre].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                    if (NowPlaying[i].Path == path)
                    {
                        index = i;
                        NowPlaying[index].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                    }
                    if (index != -1 && pre != -1) break;
                }
            }
            Player.URL = path;
            BLL_QLSong.Instance.UpdateView(s.ID_BaiHat);
            Player.controls.play();
            btnPause.Visible = true;
            btnPlay.Visible = false;
        }
        public void btnAddSongToPL_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["frmAddPL"];
            if (f == null)
            {
                frmAddPL ff = new frmAddPL(ID_Login, ((Button)sender).Name, NowPlaying, Status);
                f = ff;
                ff.cPL += new frmAddPL.CallPL(Load_Playlists);
                ff.Phat += new frmAddPL.PhatNhac(PhatNhac);
                ff.TopLevel = false;
                if (Status == 0)
                {
                    pnlHomeSong.Controls.Add(ff);
                    ff.BringToFront();
                    Point p = new Point(0, ((Button)sender).Height);
                    p = ((Button)sender).PointToScreen(p);
                    if ((p.Y - 240 + ff.Height) > pnlHomeSong.Height)
                    {
                        ff.Location = new System.Drawing.Point(p.X - 600, pnlHomeSong.Height - (ff.Height));
                    }
                    else
                    {
                        ff.Location = new System.Drawing.Point(p.X - 600, p.Y - 240);
                    }
                    ff.Show();
                }
                if (Status == 1)
                {
                    pnlMusic.Controls.Add(ff);
                    ff.BringToFront();
                    Point p = new Point(0, ((Button)sender).Height);
                    p = ((Button)sender).PointToScreen(p);
                    if ((p.Y - 240 + ff.Height) > pnlMusic.Height)
                    {
                        ff.Location = new System.Drawing.Point(p.X - 550, pnlMusic.Height - (ff.Height));
                    }
                    else
                    {
                        ff.Location = new System.Drawing.Point(p.X - 550, p.Y - 120);
                    }
                    ff.Show();
                }
            }
            else
            {
                f.Close();
                if (((Button)sender).Name != ((frmAddPL)f).ID_Add) btnAddSongToPL_Click(sender, e);
            }
        }
        public void btnDownload_Click(object sender, EventArgs e)
        {
            Song s = BLL_QLSong.Instance.GetSongByID(((Button)sender).Name);
            string path = @"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3";
            BLL_QLSong.Instance.DownloadSong(s.Link.Trim(), path);
            BLL_QLSong.Instance.UpdateView(s.ID_BaiHat);
            ((Button)sender).Visible = false;
        }
        private void cbbHomePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_HomePage(((CBBItem)cbbHomePage.SelectedItem).Value.ToString().Trim());
        }

        // My music
        private void btnMusic_Click(object sender, EventArgs e)
        {
            Status = 1;
            tbxSearch.Text = "Search";
            pnlAdmin.Visible = false;
            Load_ControlMusic(true);
            SetBackColor(btnMusic);
            SetFill(pnlMusic);
            SetVisiblePanel(false, pnlMusic);
            pnlTitleNowPlaying.Visible = false;
            lblmyMusic.Text = "My Music";
            LoadMyMusic();
        }
        public void LoadMyMusic()
        {
            //Mỗi lần load lại reset lại các biến
            ListView.Clear();
            StreamReader read = new StreamReader("MyMusic.txt");
            string line = read.ReadLine();
            foreach (string f in Directory.GetFiles(@"Downloads\").ToList())
            {
                ListView.Add(new SongModel(System.IO.Directory.GetCurrentDirectory() + @"\" + f));
            }
            while (line != null)
            {
                //Lưu các paths vào danh sách phát
                ListView.Add(new SongModel(line));
                line = read.ReadLine();
            }
            cbbSortSong_SelectedIndexChanged(new object(), new EventArgs());
            read.Close();
        }
        public static bool CheckPath(string path)
        {
            StreamReader read = new StreamReader("MyMusic.txt");
            string line = read.ReadLine();
            while (line != null)
            {
                if (line == path)
                {
                    read.Close();
                    return true;
                }
                line = read.ReadLine();
            }
            read.Close();
            return false;
        }
        private void Load_ControlMusic(bool check)
        {
            btnSongOfMe.Visible = check;
            lblSortSong.Visible = check;
            cbbSortSong.Visible = check;
        }
        public void Load_MusicOfPL()
        {
            IndexOfMyMusic = 0;
            pnlConSong.Controls.Clear();
            if (!tbxSearch.Text.Trim().Equals("Search"))
            {
                BLL_QLSong.Instance.Search(ListView, tbxSearch.Text.Trim());
            }
            foreach (SongModel f in ListView)
            {
                // thiết kế giao diện
                f.ShowMyMusic(pnlConSong, IndexOfMyMusic, btnMusicPlay_Click, btnRemovePlay_Click, btnAddSongToPL_Click);
                IndexOfMyMusic++;
            }
        }
        public void Load_MusicOfMe(List<SongModel> SongOfMe)
        {
            //Nếu MyMusic.txt rỗng thì return
            if (SongOfMe == null) return;
            if (ListView == null) ListView = new List<SongModel>();
            for (int i = 0; i < SongOfMe.Count; i++)
            {
                // thiết kế
                ListView.Add(SongOfMe[i]);
                SongOfMe[i].ShowMyMusic(pnlConSong, IndexOfMyMusic, btnMusicPlay_Click, btnRemovePlay_Click, btnAddSongToPL_Click);
                IndexOfMyMusic++;
            }
        }
        public void btnRemovePlay_Click(object sender, EventArgs e) 
        {
            string p = ((Button)sender).Name;
            string name = (p.Substring(p.LastIndexOf("\\") + 1));
            name = name.Substring(0, name.LastIndexOf("."));
            for (int i = 0; i < NowPlaying.Count; i++)
            {
                if (p == NowPlaying[i].Path)
                {
                    NowPlaying.RemoveAt(i);
                    break;
                }
            }
            if ((System.IO.Directory.GetCurrentDirectory() + @"\" + "Downloads") == p.Substring(0, p.LastIndexOf("\\")))
            {
                File.Delete(p);
            }
            else
            {
                var tempFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines("MyMusic.txt").Where(l => l != p);
                File.WriteAllLines(tempFile, linesToKeep);
                File.Delete("MyMusic.txt");
                File.Move(tempFile, "MyMusic.txt");
            }
            LoadMyMusic();
        }
        public void btnSongOfMe_Click(object sender, EventArgs e)
        {
            // nhạc của chính người dùng
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "file (*.mp3)|*.mp3|All files (*.*)|*.*";
            // tạo file ghi nhạc
            List<SongModel> SongOfMe = new List<SongModel>();
            List<string> paths = new List<string>();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                paths.AddRange(openFileDialog.FileNames);
                foreach (string path in paths)
                {
                    SongOfMe.Add(new SongModel(path));
                }
                for (int i = 0; i < SongOfMe.Count; i++)
                {
                    if (CheckPath(SongOfMe[i].Path))
                    {
                        SongOfMe.RemoveAt(i);
                        i--;
                    }
                }
                Load_MusicOfMe(SongOfMe);
            }
            StreamWriter write = new StreamWriter("MyMusic.txt", true);
            foreach (SongModel p in SongOfMe)
            {
                write.WriteLine(p.Path);
            }
            write.Close();
        }
        public void btnMusicPlay_Click(object sender, EventArgs e)
        {
            string path = ((Button)sender).Name;
            if (Status != 2)
            {
                NowPlaying = new List<SongModel>();
                foreach (SongModel i in ListView)
                {
                    NowPlaying.Add(i);
                }
            }
            if (Status == 0)
            {
                NowPlaying.Clear();
                NowPlaying.Add(new SongModel(path));
            }
            if (Status == 2)
            {
                int index = -1, pre = -1;
                for (int i = 0; i < NowPlaying.Count; i++)
                {
                    if (NowPlaying[i].Path == Player.currentMedia.sourceURL)
                    {
                        pre = i;
                        NowPlaying[pre].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                    if (NowPlaying[i].Path == path)
                    {
                        index = i;
                        NowPlaying[index].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                    }
                    if (index != -1 && pre != -1) break;
                }
            }
            Player.URL = path;
            Player.controls.play();
            btnPause.Visible = true;
            btnPlay.Visible = false;
        }

        // Move panel
        Point first;
        const int SizeY = 42;
        private bool enableMoving = false;
        private Point initialClickedPoint;
        private void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                enableMoving = true;
                first = new Point(((Panel)sender).Location.X, ((Panel)sender).Location.Y);
                initialClickedPoint = e.Location;
            }
        }
        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {
            int index = 0;
            int X = ((Panel)sender).Location.X;
            int Y = e.Y + ((Panel)sender).Top - initialClickedPoint.Y;
            if (Y < 0) Y = 0;
            for (int i = 0; i < NowPlaying.Count; i++)
            {
                if (((Panel)sender).Name == NowPlaying[i].pnl.Name)
                {
                    index = i;
                    break;
                }
            }
            int GocToaDo;
            if (index != 0) GocToaDo = NowPlaying[0].pnl.Location.Y;
            else GocToaDo = first.Y;
            ((Panel)sender).Location = new Point(X, index * ((Panel)sender).Height + GocToaDo);
            enableMoving = false;
        }
        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (enableMoving)
            {

                int index = 0;
                int X = ((Panel)sender).Location.X;
                int Y = e.Y + ((Panel)sender).Top - initialClickedPoint.Y;
                int max = SizeY * (NowPlaying.Count - 1);
                if (Y > pnlConSong.Height - 50 && pnlConSong.VerticalScroll.Value <= max) pnlConSong.VerticalScroll.Value++;
                ((Panel)sender).Location = new Point(X, Y);
                if (Y < 0) Y = 0;
                for (int i = 0; i < NowPlaying.Count; i++)
                {
                    if (((Panel)sender).Name == NowPlaying[i].pnl.Name)
                    {
                        index = i;
                        break;
                    }
                }
                if (index - 1 >= 0 && Y <= NowPlaying[index - 1].pnl.Location.Y)
                {
                    int GocToaDo;
                    if (index != 0) GocToaDo = NowPlaying[0].pnl.Location.Y;
                    else GocToaDo = first.Y;
                    NowPlaying[index - 1].pnl.Location = new Point(X, SizeY * (index) + GocToaDo);
                    ((Panel)sender).Location = new Point(X, SizeY * (index - 1) + GocToaDo);
                    first.Y = ((Panel)sender).Location.Y;
                    Swap<SongModel>(NowPlaying, index - 1, index);

                }
                if (index + 1 < NowPlaying.Count && Y >= NowPlaying[index + 1].pnl.Location.Y)
                {
                    int GocToaDo;
                    if (index != 0) GocToaDo = NowPlaying[0].pnl.Location.Y;
                    else GocToaDo = first.Y;
                    NowPlaying[index + 1].pnl.Location = new Point(X, SizeY * (index) + GocToaDo);
                    ((Panel)sender).Location = new Point(X, SizeY * (index + 1) + GocToaDo);
                    first.Y = ((Panel)sender).Location.Y;
                    Swap<SongModel>(NowPlaying, index + 1, index);
                }

            }
        }

        // Now Playing
        private void btnNowPlaying_Click(object sender, EventArgs e)
        {
            Load_NowPlaying();
        }
        public void Load_NowPlaying()
        {
            Status = 2;
            Load_ControlMusic(false);
            SetFill(pnlMusic);
            SetVisiblePanel(false, pnlMusic);
            SetBackColor(btnNowPlaying);
            pnlMusic.Dock = DockStyle.None;
            pnlTitleNowPlaying.Visible = true;
            pnlMusic.Location = new Point(0, 0);
            pnlMusic.Size = new Size(1280, 720 - pnlPlay.Height);
            pnlName.Visible = false;
            lblmyMusic.Text = "     Now Playing";
            pnlConSong.Controls.Clear();
            for (int i = 0; i < NowPlaying.Count; i++)
            {
                NowPlaying[i].ShowNowPlaying(pnlConSong, i, btnSongHome_Click, btnMusicPlay_Click, btnRemoveSongFromNowPlaying_Click);
                if (NowPlaying[i].Path.Equals(Player.currentMedia.sourceURL)) NowPlaying[i].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                else NowPlaying[i].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                NowPlaying[i].pnl.MouseUp += new System.Windows.Forms.MouseEventHandler(pnl_MouseUp);
                NowPlaying[i].pnl.MouseMove += new System.Windows.Forms.MouseEventHandler(pnl_MouseMove);
                NowPlaying[i].pnl.MouseDown += new System.Windows.Forms.MouseEventHandler(pnl_MouseDown);
            }
        }
        private void btnRemoveSongFromNowPlaying_Click(object sender, EventArgs e)
        {
            if (NowPlaying.Count == 1)
            {
                Player.URL = null;
                lblEndTime.Text = "00:00";
                lblStartTime.Text = "00:00";
                trbTime.Value = 0;
                btnPlay.Visible = true;
                btnPause.Visible = false;
            }
            else if (Player.currentMedia.sourceURL == ((Button)sender).Name)
            {
                btnNext_Click(sender, e);
            }
            for (int i = 0; i < NowPlaying.Count; i++)
            {
                if (NowPlaying[i].Path == ((Button)sender).Name)
                {
                    NowPlaying.RemoveAt(i);
                    break;
                }
            }
            Load_NowPlaying();
        }
        private void btnCloseMM_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMinimizeMM_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnBackNowPlaying_Click(object sender, EventArgs e)
        {
            Status = 0;
            SetBackColor(btnHomePage);
            SetVisiblePanel(false, pnlHome);
            SetFill(pnlHome);
            pnlName.Visible = true;
        }

        // Playlists
        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            tbxSearch.Text = "Search";
            if (BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login).Count > 0)
                Load_Playlists(); // check
        }
        private void btnCreatePL_Click(object sender, EventArgs e)
        {
            if (!ID_Login.Equals("Guest"))
            {
                pnlAdmin.Visible = false;
                frmCreatePL f = new frmCreatePL(ID_Login, "");
                f.exc += new frmCreatePL._Load(Load_Playlists);
                f.ShowDialog();
            }
        }
        public void Load_Playlists()
        {
            if (ID_Login != "Guest")
            {
                pnlAdmin.Visible = false;
                SetFill(pnlShowPL);
                SetVisiblePanel(false, pnlShowPL);
                SetBackColor(btnPlaylist);
                int j = 0;
                pnlItemPL.Controls.Clear();
                pnlItemPL.Height = 32;
                foreach (Playlist i in BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login))
                {
                    Panel pnl = new Panel();
                    pnlItemPL.Controls.Add(pnl);
                    pnl.Width = pnlItemPL.Width;
                    pnl.Height = 32;
                    pnl.Location = new System.Drawing.Point(0, pnl.Height * j);

                    Button btn = new Button();
                    pnl.Controls.Add(btn);
                    btn.Location = new System.Drawing.Point(0, 1);
                    btn.Size = new System.Drawing.Size(220, 30);
                    btn.Name = i.ID_Playlist.Trim();
                    btn.Text = " " + i.TenPlaylist.Trim();
                    btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    btn.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Image = global::MediaPlayer.Properties.Resources.icons8_video_playlist_24__1_;
                    btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(51))))); ;
                    if (pnl.Location.Y >= pnlItemPL.Height)
                    {
                        pnlItemPL.Height += 32;
                    }
                    j++;
                    btn.Click += new System.EventHandler(btnPlaylistItem_Click);
                }
                Load_pnlSongOfPL(BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login)[0].ID_Playlist);
            }
        }
        private void btnPlaylistItem_Click(object sender, EventArgs e)
        {
            Status = 3;
            tbxSearch.Text = "Search";
            Load_pnlSongOfPL(((Button)sender).Name);
        }
        public void Load_pnlSongOfPL(string id_playlist)
        {
            //reset lại danh sách phát
            SetVisiblePanel(false, pnlShowPL);
            SetFill(pnlShowPL);
            pnlAdmin.Visible = false;
            Status = 3;
            List<Song> L = new List<Song>();
            if (id_playlist != null)
            {
                L = BLL_QLSong.Instance.GetSongOfPlaylist(id_playlist);
                if (!tbxSearch.Text.Equals("Search"))
                {
                    BLL_QLSong.Instance.Search(L, tbxSearch.Text.Trim());
                }
                ListView.Clear();
                pnlSongOfPL.Controls.Clear();
                int iy = 0;
                ptbSongOfPL.Name = id_playlist;
                lblTitlePL.Text = BLL_QLSong.Instance.GetPlaylistByID(id_playlist).TenPlaylist.Trim();
                lblCountSongPL.Text = BLL_QLSong.Instance.CountSongPL(id_playlist).ToString() + " songs";
                if (L.Count == 0)
                {
                    btnPlayAllPL.Enabled = false;
                    btnAddOtherPL.Enabled = false;
                }
                else
                {
                    btnPlayAllPL.Enabled = true;
                    btnAddOtherPL.Enabled = true;
                }
                foreach (Song i in L)
                {
                    // thiết kế
                    SongModel s = new SongModel(i);
                    ListView.Add(s);
                    s.ShowHomePage(i, pnlSongOfPL, iy, btnSongHome_Click, btnMusicPlay_Click, btnRemoveSongFromPL_Click, btnDownload_Click, Status);
                    iy++;
                }
            }
            else
            {

            }
        }
        private void btnRemoveSongFromPL_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.RemoveSongFromPL(((Button)sender).Name.Trim(), ptbSongOfPL.Name.Trim());
            Load_pnlSongOfPL(ptbSongOfPL.Name.Trim());
        }
        private void btnPlayAllPL_Click(object sender, EventArgs e)
        {
            NowPlaying = new List<SongModel>();
            foreach (SongModel i in ListView)
            {
                NowPlaying.Add(i);
            }
            PhatNhac(NowPlaying[0].Path);
            btnPlay_Click(sender, e);
        }
        private void btnAddOtherPL_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["frmAddPL"];
            if (f == null)
            {
                frmAddPL ff = new frmAddPL(ID_Login, ptbSongOfPL.Name.Trim(), NowPlaying);
                f = ff;
                ff.Location = new System.Drawing.Point(1050, 200);
                ff.cPL += new frmAddPL.CallPL(Load_Playlists);
                ff.Phat += new frmAddPL.PhatNhac(PhatNhac);
                ff.Show();
            }
            else
            {
                f.Close();
            }
        }
        private void btnRenamePL_Click(object sender, EventArgs e)
        {
            frmCreatePL f = new frmCreatePL(ID_Login, ptbSongOfPL.Name.Trim());
            f.exc += new frmCreatePL._Load(Load_Playlists);
            f.exc_Loadpnl += new frmCreatePL.Load_pnl(Load_pnlSongOfPL);
            f.ShowDialog();
        }
        private void btnDelPL_Click(object sender, EventArgs e)
        {
            frmCheckDel f = new frmCheckDel(ptbSongOfPL.Name.Trim(), "", "Xóa");
            f.exc += new frmCheckDel.Load_pnlPlaylistItem(Load_Playlists);
            f.ShowDialog();
        }

        // Admin 
        bool isReverseSort = false;
        bool isAdd = true;
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Status = 4;
            pnlAdmin.Visible = true;
            SetBackColor(btnAdmin);
            SetCBBDynamic();
            btnQLUser.BackColor = Color.FromArgb(0, 0, 102);
            btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 102);
            btnQLSong.BackColor = Color.FromArgb(0, 0, 102);
            if (pnlSongAdmin.Visible == true)
            {
                btnQLSong.BackColor = Color.FromArgb(0, 0, 204);
                btnQLUser.BackColor = Color.FromArgb(0, 0, 102);
                btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 102);
            }
            if (pnlUser.Visible == true)
            {
                btnQLUser.BackColor = Color.FromArgb(0, 0, 204);
                btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 102);
                btnQLSong.BackColor = Color.FromArgb(0, 0, 102);
            }
            if (pnlGe_Au_Si.Visible == true)
            {
                btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 204);
                btnQLSong.BackColor = Color.FromArgb(0, 0, 102);
                btnQLUser.BackColor = Color.FromArgb(0, 0, 102);
            }
        }

        // Quản lý bài hát
        private void btnQLSong_Click(object sender, EventArgs e)
        {
            isReverseSort = false;
            SetVisiblePanel(false, pnlSongAdmin);
            SetFill(pnlSongAdmin);
            btnQLUser.BackColor = Color.FromArgb(0, 0, 102);
            btnQLSong.BackColor = Color.FromArgb(0, 0, 204);
            btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 102);
            Load_DGV();
        }
        public void Load_DGV()
        {
            dgvQLBH.DataSource = BLL_QLSong.Instance.GetSongInfoTo();

            dgvQLBH.Columns[0].Visible = false;
            dgvQLBH.Columns[1].HeaderText = "Tên bài hát";
            dgvQLBH.Columns[2].HeaderText = "Ca sĩ";
            dgvQLBH.Columns[3].HeaderText = "Tác giả";
            dgvQLBH.Columns[4].HeaderText = "Thể loại";
            dgvQLBH.Columns[6].HeaderText = "Lượt xem";
            dgvQLBH.Columns[7].HeaderText = "Tình trạng";
        }
        private void btnAddSong_Click(object sender, EventArgs e)
        {
            isAdd = true;
            SetCBBDynamic();
            SetVisiblePanel(false, pnlAddSong);
            SetFill(pnlAddSong);
            cbbNameSinger.SelectedIndex = 0;
            cbbNameAuthor.SelectedIndex = 0;
            cbbGerne.SelectedIndex = 0;
            btnOK.Text = "OK";
            tbxNameSong.Text = "";
            tbxLink.Text = "";
            lblID.Text = "BH" + BLL_QLSong.Instance.LastIndexOf("BaiHat").ToString();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            isAdd = false;
            SetCBBDynamic();
            SetVisiblePanel(false, pnlAddSong);
            SetFill(pnlAddSong);
            btnOK.Text = "OK";
            Song s = BLL_QLSong.Instance.GetSongAdminByID(dgvQLBH.SelectedRows[0].Cells["ID_BaiHat"].Value.ToString());
            tbxNameSong.Text = s.TenBaiHat;
            foreach (var item in cbbNameSinger.Items)
            {
                if (s.ID_CaSi.Equals(((CBBItem)item).Value.ToString()))
                {
                    cbbNameSinger.SelectedItem = item;
                    break;
                }
            }
            foreach (var item in cbbNameAuthor.Items)
            {
                if (s.ID_TacGia.Equals(((CBBItem)item).Value.ToString()))
                {
                    cbbNameAuthor.SelectedItem = item;
                    break;
                }
            }
            foreach (var item in cbbGerne.Items)
            {
                if (s.ID_TheLoai.Equals(((CBBItem)item).Value.ToString()))
                {
                    cbbGerne.SelectedItem = item;
                    break;
                }
            }
            tbxLink.Text = s.Link;
            lblID.Text = s.ID_BaiHat;
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.DeleteSong(dgvQLBH.SelectedRows[0].Cells["ID_BaiHat"].Value.ToString());
            Load_DGV();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbxNameSong.Text = "";
            tbxLink.Text = "";
            tbxSearchSongAdmin.Text = "";
            SetVisiblePanel(false, pnlSongAdmin);
            SetFill(pnlSongAdmin);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbxNameSong.Text.Equals("") || tbxLink.Text.Equals(""))
            {
                MessageBox.Show("Điền thông tin cần thiết");
                return;
            }
            if (isAdd)
            {
                // goi ham them
                BLL_QLSong.Instance.AddSong(tbxNameSong.Text, ((CBBItem)cbbNameSinger.SelectedItem).Value.ToString(), ((CBBItem)cbbNameAuthor.SelectedItem).Value.ToString(), tbxLink.Text, ((CBBItem)cbbGerne.SelectedItem).Value.ToString());
            }
            else
            {
                // goi ham sua
                string id = dgvQLBH.SelectedRows[0].Cells["ID_BaiHat"].Value.ToString();
                BLL_QLSong.Instance.UpdateSong(id, tbxNameSong.Text, ((CBBItem)cbbNameSinger.SelectedItem).Value.ToString(), ((CBBItem)cbbNameAuthor.SelectedItem).Value.ToString(), tbxLink.Text, ((CBBItem)cbbGerne.SelectedItem).Value.ToString());
            }
            Load_DGV();
            SetVisiblePanel(false, pnlSongAdmin);
            SetFill(pnlSongAdmin);
        }
        private void btnBlock_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.UpdateStatus(BLL_QLSong.Instance.GetSongAdminByID(dgvQLBH.SelectedRows[0].Cells["ID_BaiHat"].Value.ToString()).ID_BaiHat);
            Load_DGV();
        }
        private void dgvQLBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4) && e.RowIndex != -1)
            {
                string txt = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string HeaderText = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
                dgvQLBH.DataSource = BLL_QLSong.Instance.SearchSong(txt, HeaderText);
            }
        }
        private void btnShowAdmin_Click(object sender, EventArgs e)
        {
            Load_DGV();
        }
        private void tbxSearchSongAdmin_TextChanged(object sender, EventArgs e)
        {
            dgvQLBH.DataSource = BLL_QLSong.Instance.SearchSong(tbxSearchSongAdmin.Text.Trim(), "");
        }
        private void dgvQLBH_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvQLBH.DataSource != null)
            {
                if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderText != "Link")
                {
                    isReverseSort = !isReverseSort;
                    List<SongInfo> ls = new List<SongInfo>((List<SongInfo>)dgvQLBH.DataSource);
                    dgvQLBH.DataSource = BLL_QLSong.Instance.SortSongInfo(ls, ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText, isReverseSort);
                }
            }
        }

        // Quản lý thông tin người dùng
        public void Load_pnlInfoUser(string id_user)
        {
            SetVisiblePanel(false, pnlInfoUser);
            SetFill(pnlInfoUser);
            User u = BLL_QLSong.Instance.GetUserByID(id_user);
            txbMK.Text = u.Password;
            txbEmail.Text = u.Email;
            txbSDT.Text = u.SDT;
            pnl.Visible = false;
            lblID_Name.Text = u.ID_Name;
        }
        private void btnQLUser_Click(object sender, EventArgs e)
        {
            isReverseSort = false;
            SetVisiblePanel(false, pnlUser);
            SetFill(pnlUser);
            btnQLUser.BackColor = Color.FromArgb(0, 0, 204);
            btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 102);
            btnQLSong.BackColor = Color.FromArgb(0, 0, 102);

            dgvUser.DataSource = BLL_QLSong.Instance.GetUserTo();
            dgvUser.Columns[0].Visible = false;
            dgvUser.Columns[1].HeaderText = "Tên đăng nhập";
            dgvUser.Columns[2].Visible = false;
            dgvUser.Columns[3].HeaderText = "Vai trò";
            dgvUser.Columns[4].HeaderText = "Email";
            dgvUser.Columns[5].HeaderText = "Số điện thoại";
        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if(Role.Equals("Admin") && dgvUser.SelectedRows[0].Cells[3].Value.ToString().Equals("Admin1"))
            {
                MessageBox.Show("Bạn không thể sửa thông tin người này");
                return;
            }
            if(dgvUser.SelectedRows.Count == 1)
                Load_pnlInfoUser(dgvUser.SelectedRows[0].Cells[0].Value.ToString());
        }
        private void btnOK_User_Click(object sender, EventArgs e)
        {
            // cập nhật email, sđt, mật khẩu
            // kiểm tra
            User s = BLL_QLSong.Instance.GetUserByName(lblID_Name.Text);
            if(BLL_QLSong.Instance.CheckPass(txbMK.Text.Trim()))
            {
                s.Password = BLL_QLSong.Instance.Encode(txbMK.Text.Trim());
            }
            else
            {
                MessageBox.Show("Thông tin sai!");
                return;
            }
            if (BLL_QLSong.Instance.CheckEmail(txbEmail.Text.Trim()))
            {
                s.Email = txbEmail.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thông tin sai!");
                return;
            }
            if (BLL_QLSong.Instance.CheckSDT(txbSDT.Text.Trim()))
            {
                s.SDT = txbSDT.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thông tin sai!");
                return;
            }
            BLL_QLSong.Instance.EditUser(s);
            Load_pnlInfoUser(s.ID_User);
            MessageBox.Show("Sửa thành công!");
        }
        private void btnCancel_User_Click(object sender, EventArgs e)
        {
            if (Role.Equals("User"))
            {
                SetVisiblePanel(false, pnlHome);
                SetFill(pnlHome);
            }
            else if (Role.Equals("Admin") || Role.Equals("Admin1"))
            {
                if (Status == 4)
                {
                    SetVisiblePanel(false, pnlUser);
                    SetFill(pnlUser);
                }
                else if (Status == 5)
                {
                    SetVisiblePanel(false, pnlHome);
                    SetFill(pnlHome);
                }
            }
        }
        private void btnDecentralize_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.EditDecentralization(BLL_QLSong.Instance.GetUserByID(dgvUser.SelectedRows[0].Cells["ID_User"].Value.ToString()));
            btnQLUser_Click(sender, e);
        }
        private void dgvUser_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvUser.DataSource != null)
            {
                isReverseSort = !isReverseSort;
                List<User> ls = new List<User>((List<User>)dgvUser.DataSource);
                dgvUser.DataSource = BLL_QLSong.Instance.SortUser(ls, ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText, isReverseSort);
            }
        }
        private void tbxSearchUserAdmin_TextChanged(object sender, EventArgs e)
        {
            dgvUser.DataSource = BLL_QLSong.Instance.SearchUser(tbxSearchUserAdmin.Text.Trim());
        }

        // Quản lý thông tin của tác giả, thể loại, ca sĩ
        private void SetDGV_GAS_Song()
        {
            dgvGe_Au_Si.Columns[0].Visible = false;
            dgvGe_Au_Si.Columns[1].HeaderText = "Tên";

            dgvGAS_Song.DataSource = BLL_QLSong.Instance.GetSongInfoTo();
            dgvGAS_Song.Columns[0].Visible = false;
            dgvGAS_Song.Columns[1].HeaderText = "Tên bài hát";
            dgvGAS_Song.Columns[2].Visible = false;
            dgvGAS_Song.Columns[3].Visible = false;
            dgvGAS_Song.Columns[4].Visible = false;
            dgvGAS_Song.Columns[5].Visible = false;
            dgvGAS_Song.Columns[6].HeaderText = "Lượt xem";
            dgvGAS_Song.Columns[7].HeaderText = "Tình trạng";
        }
        private void btnQLInfoSong_Click(object sender, EventArgs e)
        {
            isReverseSort = false;
            SetVisiblePanel(false, pnlGe_Au_Si);
            SetFill(pnlGe_Au_Si);
            txbGe_Au_Si.Clear();
            btnQLUser.BackColor = Color.FromArgb(0, 0, 102);
            btnQLSong.BackColor = Color.FromArgb(0, 0, 102);
            btnQLInfoSong.BackColor = Color.FromArgb(0, 0, 204);
            lblGe_Au_Si.Text = "Ca sĩ";
            txbGe_Au_Si.Clear();
            pnlUnderGAS.Location = new System.Drawing.Point(btnShowSi.Location.X, 0);

            dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetSingerTo();
            SetDGV_GAS_Song();
        }
        private void btnShowSi_Click(object sender, EventArgs e)
        {
            lblGe_Au_Si.Text = "Ca sĩ";
            txbGe_Au_Si.Clear();
            pnlUnderGAS.Location = new System.Drawing.Point(btnShowSi.Location.X, 0);
            dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetSingerTo();
            SetDGV_GAS_Song();
        }
        private void btnShowAu_Click(object sender, EventArgs e)
        {
            lblGe_Au_Si.Text = "Tác giả";
            txbGe_Au_Si.Clear();
            pnlUnderGAS.Location = new System.Drawing.Point(btnShowAu.Location.X, 0);
            dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetAuthorTo();
            SetDGV_GAS_Song();
        }
        private void btnShowGe_Click(object sender, EventArgs e)
        {
            lblGe_Au_Si.Text = "Thể loại";
            txbGe_Au_Si.Clear();
            pnlUnderGAS.Location = new System.Drawing.Point(btnShowGe.Location.X, 0);
            dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetGerneTo();
            SetDGV_GAS_Song();
        }
        private void dgvGe_Au_Si_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string txt = "";
            if (lblGe_Au_Si.Text.Equals("Ca sĩ") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                txt = dgvGe_Au_Si.SelectedRows[0].Cells[1].Value.ToString();
                dgvGAS_Song.DataSource = BLL_QLSong.Instance.SearchSong(txt, "Ca sĩ");
            }
            else if (lblGe_Au_Si.Text.Equals("Tác giả") && dgvGe_Au_Si.SelectedRows.Count == 1)
            { 
                txt = dgvGe_Au_Si.SelectedRows[0].Cells[1].Value.ToString();
                dgvGAS_Song.DataSource = BLL_QLSong.Instance.SearchSong(txt, "Tác giả");
            }
            else if (lblGe_Au_Si.Text.Equals("Thể loại") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                txt = dgvGe_Au_Si.SelectedRows[0].Cells[1].Value.ToString();
                dgvGAS_Song.DataSource = BLL_QLSong.Instance.SearchSong(txt, "Thể loại");
            }
            tbxChangeGAS.Text = txt;
        }
        private void btnShowGAS_Click(object sender, EventArgs e)
        {
            SetDGV_GAS_Song();
        }
        private void btnAddGAS_Click(object sender, EventArgs e)
        {
            if (lblGe_Au_Si.Text.Equals("Ca sĩ") && tbxChangeGAS.Text.Length > 0)
            {
                BLL_QLSong.Instance.AddSinger(tbxChangeGAS.Text.ToString().Trim());
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetSingerTo();
            }
            else if (lblGe_Au_Si.Text.Equals("Tác giả") && tbxChangeGAS.Text.Length > 0)
            {
                BLL_QLSong.Instance.AddAuthor(tbxChangeGAS.Text.ToString().Trim());
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetAuthorTo();
            }
            else if (lblGe_Au_Si.Text.Equals("Thể loại") && tbxChangeGAS.Text.Length > 0)
            {
                BLL_QLSong.Instance.AddGerne(tbxChangeGAS.Text.ToString().Trim());
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetGerneTo();
            }
        }
        private void btnEditGAS_Click(object sender, EventArgs e)
        {
            if(dgvGe_Au_Si.CurrentCell == null)
            {
                return;
            }
            if (lblGe_Au_Si.Text.Equals("Ca sĩ") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("CS100"))
                {
                    BLL_QLSong.Instance.UpdateSinger(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString(), tbxChangeGAS.Text);
                }
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetSingerTo();
                tbxChangeGAS.Clear();
            }
            else if (lblGe_Au_Si.Text.Equals("Tác giả") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("TG100"))
                {
                    BLL_QLSong.Instance.UpdateAuthor(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString(), tbxChangeGAS.Text);
                }
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetAuthorTo();
                tbxChangeGAS.Clear();
            }
            else if (lblGe_Au_Si.Text.Equals("Thể loại") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("TL100"))
                {
                    BLL_QLSong.Instance.UpdateGerne(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString(), tbxChangeGAS.Text);
                }
                dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetGerneTo();
                tbxChangeGAS.Clear();
            }
        }
        private void btnDelGAS_Click(object sender, EventArgs e)
        {
            if (dgvGe_Au_Si.CurrentCell == null)
            {
                return;
            }
            if (lblGe_Au_Si.Text.Equals("Ca sĩ") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("CS100"))
                {
                    BLL_QLSong.Instance.DeleteSinger(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString());
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetSingerTo();
                }
                else
                {
                    MessageBox.Show("Khong the xoa");
                }
                txbGe_Au_Si.Clear();
            }
            else if (lblGe_Au_Si.Text.Equals("Tác giả") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("TG100"))
                {
                    BLL_QLSong.Instance.DeleteAuthor(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString());
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetAuthorTo();
                }
                else
                {
                    MessageBox.Show("Khong the xoa");
                }
                txbGe_Au_Si.Clear();
            }
            else if (lblGe_Au_Si.Text.Equals("Thể loại") && dgvGe_Au_Si.SelectedRows.Count == 1)
            {
                int n = dgvGe_Au_Si.CurrentCell.RowIndex;
                if (!dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString().Equals("TL100"))
                {
                    BLL_QLSong.Instance.DeleteGerne(dgvGe_Au_Si.Rows[n].Cells[0].Value.ToString());
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.GetGerneTo();
                }
                else
                {
                    MessageBox.Show("Khong the xoa");
                }
                txbGe_Au_Si.Clear();
            }
        }
        private void dgvGe_Au_Si_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvGe_Au_Si.DataSource != null)
            {
                isReverseSort = !isReverseSort;
                switch (lblGe_Au_Si.Text.Trim())
                {
                    case "Ca sĩ":
                        List<Singer> lsinger = new List<Singer>((List<Singer>)dgvGe_Au_Si.DataSource);
                        dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SortSinger(lsinger, ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText, isReverseSort);
                        break;
                    case "Tác giả":
                        List<Author> lauthor = new List<Author>((List<Author>)dgvGe_Au_Si.DataSource);
                        dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SortAuthor(lauthor, ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText, isReverseSort);
                        break;
                    case "Thể loại":
                        List<Gerne> lgerne = new List<Gerne>((List<Gerne>)dgvGe_Au_Si.DataSource);
                        dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SortGerne(lgerne, ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText, isReverseSort);
                        break;
                    default:
                        break;
                }
            }
        }
        private void txbGe_Au_Si_TextChanged(object sender, EventArgs e)
        {
            txbGe_Au_Si.Focus();
            int type;
            if (lblGe_Au_Si.Text.Equals("Ca sĩ")) type = 0;
            else if (lblGe_Au_Si.Text.Equals("Tác giả")) type = 1;
            else type = 2;
            switch (type)
            {
                case 0:
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SearchSinger(txbGe_Au_Si.Text.Trim());
                    break;
                case 1:
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SearchAuthor(txbGe_Au_Si.Text.Trim());
                    break;
                case 2:
                    dgvGe_Au_Si.DataSource = BLL_QLSong.Instance.SearchGerne(txbGe_Au_Si.Text.Trim());
                    break;
                default:
                    break;
            }
        }

        // phát nhạc
        List<SongModel> PreNow;
        public bool PhatNhac(string path)
        {
            if (File.Exists(path))
            {
                Player.URL = path;
                Player.controls.play();
                btnPlay_Click(new object(), new EventArgs());
                // 
                if ((System.IO.Directory.GetCurrentDirectory() + @"\" + "Data") == path.Substring(0, path.LastIndexOf("\\")))
                {
                    string id = path.Substring(path.LastIndexOf("\\") + 1);
                    id = id.Substring(0, id.Length - 4);
                    BLL_QLSong.Instance.UpdateView(id);
                }
                return true;
            }
            else if ((System.IO.Directory.GetCurrentDirectory() + @"\" + "Data") == path.Substring(0, path.LastIndexOf("\\")))
            {
                string id = path.Substring(path.LastIndexOf("\\") + 1);
                id = id.Substring(0, id.Length - 4);
                Song s = BLL_QLSong.Instance.GetSongByID(id);
                string p = @"Data\" + s.ID_BaiHat.ToString() + ".mp3";
                BLL_QLSong.Instance.DownloadSong(s.Link.Trim(), p.Trim());
                Player.URL = p;
                Player.controls.play();
                btnPlay_Click(new object(), new EventArgs());
                BLL_QLSong.Instance.UpdateView(id);
                btnPause.Visible = true;
                btnPlay.Visible = false;
                return true;
            }
            return false;
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPause.Visible = true;
            btnPlay.Visible = false;
            if (Player.URL.Equals(""))
            {
                btnPlay.Visible = true;
                btnPause.Visible = false;
            }
            else Player.controls.play();
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            btnPause.Visible = false;
            btnPlay.Visible = true;
            Player.controls.pause();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (NowPlaying.Count > 0)
            {
                string s = Player.currentMedia.sourceURL.ToString();
                int index = 0;
                for (int i = 0; i < NowPlaying.Count; i++)
                {
                    if (s.Equals(NowPlaying[i]))
                    {
                        index = i;
                        break;
                    }
                }
                if (index - 1 >= 0)
                {
                    //Chỉnh sửa
                    if (PhatNhac(NowPlaying[index - 1].Path) == true && Status == 2)
                    {
                        NowPlaying[index - 1].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                        NowPlaying[index].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                }
            }
        }
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            if(NowPlaying.Count == 0)
            {
                return;
            }
            btnShuffleTrue.Visible = true;
            btnShuffle.Visible = false;
            PreNow = new List<SongModel>(NowPlaying);
            string s = Player.currentMedia.sourceURL.ToString();
            int index = 0;
            for (int i = 0; i < NowPlaying.Count; i++)
            {
                if (s == NowPlaying[i].Path)
                {
                    index = i;
                    break;
                }
            }
            Swap<SongModel>(NowPlaying, 0, index);
            List<SongModel> temp = new List<SongModel>(NowPlaying);
            temp.RemoveAt(0);
            for (int i = 1; i < NowPlaying.Count; i++)
            {
                Random rand = new Random();
                int x = rand.Next(temp.Count - 1);
                NowPlaying[i] = temp[x];
                temp.RemoveAt(x);
            }
            if (Status == 2) Load_NowPlaying();
        }
        private void btnShuffleTrue_Click(object sender, EventArgs e)
        {
            btnShuffleTrue.Visible = false;
            btnShuffle.Visible = true;
            NowPlaying = PreNow;
            if (Status == 2) Load_NowPlaying();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (NowPlaying.Count > 0)
            {
                string s = Player.currentMedia.sourceURL.ToString();
                int index = 0;
                for (int i = 0; i < NowPlaying.Count; i++)
                {
                    if (s == NowPlaying[i].Path)
                    {
                        index = i;
                        break;
                    }
                }
                if (index < NowPlaying.Count - 1)
                {
                    if (PhatNhac(NowPlaying[index + 1].Path) == true && Status == 2)
                    {
                        NowPlaying[index + 1].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                        NowPlaying[index].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                }
                //Đang ở bài cuối cùng khi nhấn vào btnNext thì phát lại bài đầu tiên	
                else
                {
                    if (PhatNhac(NowPlaying[0].Path) == true && Status == 2)
                    {
                        NowPlaying[0].BtnPlay.ForeColor = System.Drawing.Color.Aqua;
                        NowPlaying[NowPlaying.Count - 1].BtnPlay.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                }
            }
        }
        private void btnRepeatAll_Click(object sender, EventArgs e)
        {
            btnRepeat.Visible = false;
            btnRepeatOne.Visible = true;
            btnRepeatAll.Visible = false;
            CheckRepeat = 2;
        }
        private void btnRepeatOne_Click(object sender, EventArgs e)
        {
            btnRepeat.Visible = true;
            btnRepeatOne.Visible = false;
            btnRepeatAll.Visible = false;
            CheckRepeat = 0;
        }
        private void btnRepeat_Click(object sender, EventArgs e)
        {
            btnRepeat.Visible = false;
            btnRepeatOne.Visible = false;
            btnRepeatAll.Visible = true;
            CheckRepeat = 1;
        }
        private void trbVolume_Scroll(object sender, EventArgs e)
        {
            Player.settings.volume = trbVolume.Value;
            lblVolume.Text = trbVolume.Value.ToString() + "%";
        }
        private void trbTime_Scroll(object sender, EventArgs e)
        {
            if(NowPlaying.Count > 0)
            {
                Player.controls.currentPosition = trbTime.Value;
                lblStartTime.Text = Player.controls.currentItem.durationString.ToString();
            }    
        }
        private void trbTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (NowPlaying.Count > 0)
            {
                Player.controls.currentPosition = Player.currentMedia.duration * e.X / trbTime.Width;
            }
        }
        private void timPlay_Tick(object sender, EventArgs e)
        {
            try
            {
                //Player.currentMedia.sourceURL!=null Kiểm tra có đang phát bài hát nào không
                if (Player.playState == WMPLib.WMPPlayState.wmppsPlaying && Player.currentMedia.sourceURL != null)
                {
                    trbTime.Maximum = (int)Player.controls.currentItem.duration;
                    trbTime.Value = (int)Player.controls.currentPosition;
                    //Kiểm tra đã phát hết bài chưa
                    if (trbTime.Value < trbTime.Maximum)
                    {
                        lblStartTime.Text = Player.controls.currentPositionString.ToString();
                        lblEndTime.Text = Player.controls.currentItem.durationString.ToString();
                    }
                    else
                    {
                        //reset lại lbl với trbTime
                        trbTime.Value = 0;
                        lblStartTime.Text = "00:00";
                        lblEndTime.Text = "00:00";
                        switch (CheckRepeat)
                        {
                            //Chỉnh sửa
                            case 0:
                                if (Player.currentMedia.sourceURL != NowPlaying[NowPlaying.Count - 1].Path) btnNext_Click(sender, e);
                                break;
                            case 1:
                                btnNext_Click(sender, e);
                                break;
                            case 2:
                                PhatNhac(Player.currentMedia.sourceURL);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void btnMute_Click(object sender, EventArgs e)
        {
            btnMute.Visible = false;
            btnVolume.Visible = true;
            Player.settings.volume = trbVolume.Value = 80;
            lblVolume.Text = trbVolume.Value.ToString() + "%";
        }
        private void btnVolume_Click(object sender, EventArgs e)
        {
            btnMute.Visible = true;
            btnVolume.Visible = false;
            Player.settings.volume = trbVolume.Value = 0;
            lblVolume.Text = trbVolume.Value.ToString() + "%";
        }

        // Search - Search
        private void tbxSearch_Click(object sender, EventArgs e)
        {
            tbxSearch.Clear();
        }
        private void cbbSortSong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView != null)
            {
                ListView = BLL_QLSong.Instance.SortMyMusic(ListView, ((CBBItem)cbbSortSong.SelectedItem).Value.Trim());
                Load_MusicOfPL();
            }
        }
        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            tbxSearch.Focus();
            e.Handled = true;
            if (e.KeyCode == Keys.Enter)
            {
                if (Status == 0) Load_HomePage(((CBBItem)cbbHomePage.SelectedItem).Value.ToString().Trim());
                if (Status == 1) LoadMyMusic();
                if (Status == 3) Load_pnlSongOfPL(ptbSongOfPL.Name); 
            }
        }
        
    }
}
