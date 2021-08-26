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
    public partial class frmCreatePL : Form
    {
        public delegate void _Load();
        public delegate void Load_pnl(string id);
        public _Load exc { get; set; }
        public Load_pnl exc_Loadpnl { get; set; }

        string ID_Login = "";
        string ID_Playlist = "";
        bool check = true;

        public frmCreatePL(string id, string id_playlist)
        {
            InitializeComponent();
            this.Opacity = 0.9;
            if(!id_playlist.Equals(""))
            {
                btnCreate.Text = "Rename";
                txbPlaylist.Text = BLL_QLSong.Instance.GetPlaylistByID(id_playlist).TenPlaylist.Trim();
                ID_Playlist = id_playlist;
                check = false;
            }
            else
            {
                btnCreate.Text = "Create playlist";
                txbPlaylist.Text = "";
                check = true;
            }
            ID_Login = id;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txbPlaylist.Text = "";
            this.Close();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string s = "";
            if (check == true) // create new
            {
                if (!txbPlaylist.Text.Equals(""))
                {
                    s = txbPlaylist.Text.Trim();
                }
                else
                {
                    int j = 1;
                    while (true)
                    {
                        s = "NewPlaylist (" + j + ")";
                        j++;
                        bool check1 = true;
                        //MessageBox.Show(s);
                        foreach (Playlist i in BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login))
                        {
                            if (s.Equals(i.TenPlaylist))
                            {
                                check1 = false;
                                break;
                            }
                        }
                        if (check1 == true) break;
                    }
                }
                Playlist p = new Playlist()
                {
                    ID_Playlist = "PL" + (BLL_QLSong.Instance.LastIndexOf("Playlist") + 1).ToString().Trim(),
                    TenPlaylist = s,
                    ID_User = ID_Login,
                    LsID_Song = null
                };
                BLL_QLSong.Instance.CreatePlaylist(p);
            }
            else if (check == false) // update
            {

                if (!txbPlaylist.Text.Equals(""))
                {
                    s = txbPlaylist.Text.Trim();
                }
                else
                {
                    int j = 1;

                    while (true)
                    {
                        s = "NewPlaylist (" + j + ")";
                        j++;
                        bool check1 = true;
                        //MessageBox.Show(s);
                        foreach (Playlist i in BLL_QLSong.Instance.GetPlaylistOfUser(ID_Login))
                        {
                            if (s.Equals(i.TenPlaylist))
                            {
                                check1 = false;
                                break;
                            }
                        }
                        if (check1 == true) break;
                    }
                }
                Playlist p = new Playlist()
                {
                    ID_Playlist = this.ID_Playlist,
                    TenPlaylist = s,
                    ID_User = this.ID_Login,
                    LsID_Song = null
                };
                BLL_QLSong.Instance.RenamePlaylist(p);
                exc_Loadpnl(ID_Playlist);
            }
            this.exc();
            this.Close();
        }
    }
}
