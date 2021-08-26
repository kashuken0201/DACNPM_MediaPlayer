using MediaPlayer.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer.DTO
{
    public class SongModel
    {
        public Panel pnl { get; set; }
        public Button BtnPlay { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        const int SizeY = 42;
        public delegate void Event(object sender, EventArgs e);
        public SongModel()
        {

        }
        public SongModel(string Path)
        {
            this.Path = Path;
            Name = ("           " + Path.Substring(Path.LastIndexOf("\\") + 1));
            Name = Name.Substring(0, Name.Length - 4);
        }
        public SongModel(Song s)
        {
            bool check = File.Exists(@"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3");
            string path;
            if (check == true)
            {
                path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3";
            }
            else
            {
                path = System.IO.Directory.GetCurrentDirectory() + @"\" + @"Data\" + s.ID_BaiHat + ".mp3";
            }
            this.Path = path;
            Name = ("           " + path.Substring(path.LastIndexOf("\\") + 1));
            Name = Name.Substring(0, Name.Length - 4);
        }
        public void ShowMyMusic(Panel pnlConSong, int index, Event Play, Event Remove, Event AddSongTo)
        {
            pnl = new Panel()
            {
                Name = Path,
                Width = pnlConSong.Width - SystemInformation.VerticalScrollBarWidth - 40,
                Height = SizeY,
            };
            pnl.Location = new System.Drawing.Point(20, pnl.Height * index);
            pnlConSong.Controls.Add(pnl);
            BtnPlay = new Button()
            {
                Name = Path,
                Text = Name,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(0, 0),
                Height = SizeY,
                Width = pnlConSong.Width - SystemInformation.VerticalScrollBarWidth - 130,
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            };
            BtnPlay.FlatAppearance.BorderSize = 0;
            BtnPlay.Click += new System.EventHandler(Play);
            pnl.Controls.Add(BtnPlay);

            Button btnRemove = new Button()
            {
                Name = Path,
                Location = new System.Drawing.Point(BtnPlay.Width, 0),
                Height = SizeY,
                Width = SizeY,
                FlatStyle = FlatStyle.Flat,
                Image = global::MediaPlayer.Properties.Resources.icons8_minus_24,
            };
            pnl.Controls.Add(btnRemove);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += new System.EventHandler(Remove);

            Button btnAddToNowPlaying = new Button()
            {
                Name = Path,
                Location = new System.Drawing.Point(BtnPlay.Width + btnRemove.Width, 0),
                Height = SizeY,
                Width = SizeY,
                FlatStyle = FlatStyle.Flat,
                Image = global::MediaPlayer.Properties.Resources.icons8_plus_math_24,
            };
            pnl.Controls.Add(btnAddToNowPlaying);
            btnAddToNowPlaying.FlatAppearance.BorderSize = 0;
            btnAddToNowPlaying.Click += new System.EventHandler(AddSongTo);
        }
        public void ShowNowPlaying(Panel pnlConSong, int index, Event PlayOnl, Event PlayOff, Event Remove)
        {
            pnl = new Panel()
            {
                Name = Path,
                Width = pnlConSong.Width - SystemInformation.VerticalScrollBarWidth - 30,
                Height = SizeY,  
            };
            pnl.Location = new System.Drawing.Point(30, pnl.Height * index);
            pnlConSong.Controls.Add(pnl);
            BtnPlay = new Button()
            {
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(0, 0),
                Height = SizeY - 1,
                Width = pnl.Width - 100,
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            };
            pnl.Controls.Add(BtnPlay);
            BtnPlay.FlatAppearance.BorderSize = 0;
            if ((System.IO.Directory.GetCurrentDirectory() + @"\" + "Data") == Path.Substring(0, Path.LastIndexOf("\\")))
            {
                Song s = BLL_QLSong.Instance.GetSongByID(Name.Trim());
                BtnPlay.Text = "           " + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi;
                BtnPlay.Name = s.ID_BaiHat;
                BtnPlay.Click += new System.EventHandler(PlayOnl);
            }
            else
            {
                BtnPlay.Name = Path;
                BtnPlay.Text = Name;
                BtnPlay.Click += new System.EventHandler(PlayOff);
            }

            Panel pnlUnderNow = new Panel()
            {
                Location = new Point(0, 41),
                Size = new Size(pnl.Width - 42, 1),
                BackColor = SystemColors.ControlDark
            };
            pnl.Controls.Add(pnlUnderNow);

            Button btnRemove = new Button()
            {
                Name = Path,
                Dock = DockStyle.Right,
                Height = SizeY,
                Width = SizeY,
                FlatStyle = FlatStyle.Flat,
                Image = global::MediaPlayer.Properties.Resources.icons8_minus_24,
            };
            pnl.Controls.Add(btnRemove);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += new System.EventHandler(Remove);
        }
        public void ShowHomePage(Song s, Panel pnlHomeSong, int index, Event PlayOnl, Event PlayOff, Event PL, Event Download, int status)
        {
            pnl = new Panel()
            {
                Width = pnlHomeSong.Width - SystemInformation.VerticalScrollBarWidth - 20,
                Height = SizeY,
            };
            pnl.Location = new System.Drawing.Point(20, pnl.Height * index);
            pnlHomeSong.Controls.Add(pnl);

            BtnPlay = new Button()
            {
                Text = "           " + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi,
                ForeColor = System.Drawing.SystemColors.ButtonHighlight,
                Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(0, 0),
                Width = pnlHomeSong.Width - SystemInformation.VerticalScrollBarWidth - 120,
                FlatStyle = FlatStyle.Flat,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Height = SizeY,
            };
            pnl.Controls.Add(BtnPlay);
            bool check = File.Exists(@"Downloads\" + s.TenBaiHat + "  -  " + BLL_QLSong.Instance.GetSingerByID(s.ID_CaSi).TenCaSi + ".mp3");
            if (check == false)
            {
                BtnPlay.Name = s.ID_BaiHat;
                BtnPlay.Click += new System.EventHandler(PlayOnl);
            }
            else
            {
                BtnPlay.Name = Path;
                BtnPlay.Click += new System.EventHandler(PlayOff);

            }
            BtnPlay.FlatAppearance.BorderSize = 0;
            if (index % 2 == 0)
            {
                BtnPlay.BackColor = System.Drawing.Color.FromArgb(57, 62, 60);
            }
            else
            {
                BtnPlay.BackColor = System.Drawing.Color.FromArgb(29, 27, 27);
            }

            Button btnPL = new Button()
            {
                Name = s.ID_BaiHat.Trim(),
                Location = new System.Drawing.Point(BtnPlay.Size.Width, 0),
                Height = SizeY,
                Width = SizeY,
                FlatStyle = FlatStyle.Flat
            };
            pnl.Controls.Add(btnPL);
            btnPL.FlatAppearance.BorderSize = 0;
            if (status == 0)
            {
                btnPL.Image = global::MediaPlayer.Properties.Resources.icons8_plus_math_24;
            }
            else
            {
                btnPL.Image = global::MediaPlayer.Properties.Resources.icons8_minus_24;
            }
            if (index % 2 == 0)
            {
                btnPL.BackColor = System.Drawing.Color.FromArgb(29, 27, 27);
            }
            else
            {
                btnPL.BackColor = System.Drawing.Color.FromArgb(57, 62, 60);
            }
            btnPL.Click += new System.EventHandler(PL);

            if (check == false)
            {
                Button btnDown = new Button()
                {
                    Name = s.ID_BaiHat.Trim(),
                    Location = new System.Drawing.Point(btnPL.Size.Width + BtnPlay.Size.Width, 0),
                    Height = SizeY,
                    Width = SizeY,
                    FlatStyle = FlatStyle.Flat,
                    Image = global::MediaPlayer.Properties.Resources.icons8_download_24
                };
                pnl.Controls.Add(btnDown);
                btnDown.FlatAppearance.BorderSize = 0;
                if (index % 2 == 0)
                {
                    btnDown.BackColor = System.Drawing.Color.FromArgb(57, 62, 60);
                }
                else
                {
                    btnDown.BackColor = System.Drawing.Color.FromArgb(29, 27, 27);
                }
                btnDown.Click += new System.EventHandler(Download);
            }
        }
    }
}
