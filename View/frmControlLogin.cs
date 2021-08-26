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
    public partial class frmControlLogin : Form
    {
        public delegate void SetInfo(string id_user);
        public SetInfo set { get; set; }
        public delegate void SignOff();
        public SignOff close { get; set; }
        string ID_Login = "";
        string Role = "";

        public frmControlLogin(string id_user, string role) // guest or user/admin
        {
            InitializeComponent();
            this.Opacity = 0.98;
            btnSignOff.Text = "Đăng xuất";
            btnSignOff.Image = global::MediaPlayer.Properties.Resources.icons8_exit_32;
            ID_Login = id_user;
            Role = role;
            if (!ID_Login.Equals("Guest"))
            {
                lblEmail.Text = BLL_QLSong.Instance.GetUserByID(id_user).Email.ToString();
            }       
            else
            {
                lblEmail.Text = "Guest";
                btnSettings.Visible = false;
                btnSignOff.Text = "Đăng nhập";
                btnSignOff.Image = global::MediaPlayer.Properties.Resources.icons8_enter_32;
            }
        }
        private void btnSignOff_Click(object sender, EventArgs e)
        {
            this.close();
            Form ff = Application.OpenForms["Form1"];
            if (ff != null)
            {
                ff.Dispose();
            }
            Login f = new Login();
            f.Show();
            this.Close();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            //
            Form f = Application.OpenForms["Form1"];
            if(f != null)
            {
                this.Close();
                Form1 ff = new Form1(Role, ID_Login);
                f = ff;
                ff.set += new Form1.SetInfoUser(set);
                this.set(ID_Login);
            }
            else
            {
                this.Close();
                f.Close();
            }
        }
    }
}
