using MediaPlayer.BLL;
using MediaPlayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer.View
{
    public partial class frmAddPL : Form
    {
        public delegate void CallPL();
        public CallPL cPL { get; set; }
        public delegate bool PhatNhac(string path);
        public PhatNhac Phat { get; set; }

        string ID_Login = "";
        public string ID_Add = "";
        int Status;
        List<SongModel> Now;

        public frmAddPL(string id_user, string id_add, List<SongModel> NowPlaying, int status = 0)
        {
            ID_Login = id_user;
            ID_Add = id_add;
            Now = NowPlaying;
            Status = status;
            InitializeComponent();
            Load_Form();
            this.Opacity = 0.9;
        }
        public void Load_Form()
        {
            this.Controls.Clear();
            Button btnNowPlaying = new Button()
            {
                Text = " Now playing",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.SystemColors.ButtonHighlight,
                Image = global::MediaPlayer.Properties.Resources.icons8_audio_wave_24,
                Size = new System.Drawing.Size(200, 35),
                Location = new System.Drawing.Point(10, 2),
                TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText,
                ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            btnNowPlaying.FlatAppearance.BorderSize = 0;
            btnNowPlaying.Click += new System.EventHandler(btnNowPlaying_Click);
            this.Controls.Add(btnNowPlaying);

            if (!ID_Login.Equals("Guest") && Status != 1)
            {
                Button btnCreatNewPL = new Button()
                {
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                    Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = System.Drawing.SystemColors.ButtonHighlight,
                    Image = global::MediaPlayer.Properties.Resources.icons8_plus_math_24__1_,
                    Location = new System.Drawing.Point(10, 37),
                    Name = "PL" + (BLL_QLSong.Instance.LastIndexOf("Playlist") + 1).ToString(),
                    Size = new System.Drawing.Size(200, 35),
                    Text = " New playlist",
                    TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText,
                    UseVisualStyleBackColor = true,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    ImageAlign = System.Drawing.ContentAlignment.MiddleLeft,
                };
                btnCreatNewPL.FlatAppearance.BorderSize = 0;
                btnCreatNewPL.Click += new System.EventHandler(btnCreateNewPL_Click);
                this.Controls.Add(btnCreatNewPL);
                this.Height += 37;
                int j = 2;
                foreach (Playlist i in BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login))
                {
                    Button btn = new Button()
                    {
                        Location = new System.Drawing.Point(10, 2 + (btnNowPlaying.Height + 2) * j),
                        Size = new System.Drawing.Size(200, 35),
                        Name = i.ID_Playlist.Trim(),
                        Text = " " + i.TenPlaylist.Trim(),
                        ForeColor = System.Drawing.SystemColors.ButtonHighlight,
                        Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                        FlatStyle = FlatStyle.Flat,
                        Image = global::MediaPlayer.Properties.Resources.icons8_video_playlist_24,
                        TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText,
                        TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                        ImageAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    };  
                    btn.FlatAppearance.BorderSize = 0;
                    this.Controls.Add(btn);
                    if (btn.Location.Y >= this.Height)
                    {
                        this.Height += 37;
                    }
                    btn.Click += new System.EventHandler(btnPlaylistItem_Click);
                    j++;
                }
                this.Height += 7;
            }
        }
        private void btnNowPlaying_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (Now.Count == 0) check = true;
            BLL_QLSong.Instance.AddSongToNowPlaying(ID_Add, Now);
            if (check) this.Phat(Now[0].Path);
            this.Close();
        }
        private void btnPlaylistItem_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.AddSongToPL(ID_Add.Trim(), ((Button)sender).Name.Trim());
            frmCheckDel f = new frmCheckDel(((Button)sender).Name.Trim(), ID_Add.Trim(), "Thêm");
            f.Show();
            this.Close();
        }
        private void btnCreateNewPL_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmCreatePL f = new frmCreatePL(ID_Login, "");
            f.exc += new frmCreatePL._Load(cPL);
            f.ShowDialog();
            foreach (Playlist p in BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login))
            {
                if (p.ID_Playlist.Equals(((Button)sender).Name.Trim()))
                {
                    BLL_QLSong.Instance.AddSongToPL(ID_Add.Trim(), ((Button)sender).Name.Trim());
                    break;
                }
            }
        }
    }
}
