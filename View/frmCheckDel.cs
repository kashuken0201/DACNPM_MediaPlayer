using MediaPlayer.BLL;
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
    public partial class frmCheckDel : Form
    {
        public delegate void Load_pnlPlaylistItem();
        public Load_pnlPlaylistItem exc { get; set; }
        string ID_Playlist = "";
        //string Type = "";
        public frmCheckDel(string id_newplaylist, string id_oldplaylist, string type)
        {
            InitializeComponent();
            this.Opacity = 0.9;
            ID_Playlist = id_newplaylist;
            //Type = type;
            if(type.Equals("Thêm"))
            {
                Timer timer = new Timer();
                timer.Enabled = true;
                timer.Interval = 1000;
                timer.Start();
                btnOK.Visible = false;
                btnOK.Visible = false;
                lblNamePL.Visible = true;
                this.Size = new Size(240, 30);
                this.Location = new Point(1150, 100);
                int n = BLL_QLSong.Instance.CountSongPL(id_oldplaylist);
                if(id_oldplaylist.Contains("PL"))
                {
                    lblQues.Text = "Thêm thành công " + n + " bài hát đến playlist '" + BLL_QLSong.Instance.GetPlaylistByID(id_newplaylist).TenPlaylist + "'";
                }
                else
                {
                    lblQues.Text = "Thêm thành công bài hát " + BLL_QLSong.Instance.GetSongByID(id_oldplaylist).TenBaiHat + " vào playlist '" + BLL_QLSong.Instance.GetPlaylistByID(id_newplaylist).TenPlaylist + "'";
                }
                lblQues.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblQues.Dock = DockStyle.Fill;
                this.BackColor = Color.Black;
                timer.Tick += new EventHandler(timer_Tick);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
                lblNamePL.Text = BLL_QLSong.Instance.GetPlaylistByID(id_newplaylist).TenPlaylist;
            }
            
        }
        int i = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            i++;
            if (i == 2) this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            BLL_QLSong.Instance.DelPlaylist(ID_Playlist);
            this.Close();
            this.exc();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
