using MediaPlayer;
using MediaPlayer.BLL;
using MediaPlayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer.View
{
    public partial class Login : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        public Login()
        {
            InitializeComponent();
            tbxUsername.Text = "Tên đăng nhập";
            tbxPassword.Text = "Mật khẩu";
            tbxPassword.UseSystemPasswordChar = false;
            this.Opacity = 0.98; 
        }
        
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (!InternetConnection.Instance.isConnected())
            {
                lblWrongInfo.Text = "* Connection error";
                return;
            }
            
            if(tbxUsername.Text.Trim().Equals("") || tbxPassword.Text.Trim().Equals(""))
            {
                lblWrongInfo.Text = "* Please enter Username or Password.";
                return;
            }
            foreach (var i in BLL_QLSong.Instance.GetUserTo())
            {
                if (i.ID_Name.Equals(tbxUsername.Text.Trim()))
                {
                    if (i.Password.Equals(BLL_QLSong.Instance.Encode(tbxPassword.Text.Trim())))
                    {
                        if (i.Role.Equals("User"))
                        {
                            Form1 f = new Form1("User", i.ID_User);
                            this.Hide();
                            f.ShowDialog();
                        }
                        else if (i.Role == "Admin")
                        {
                            Form1 f = new Form1("Admin", i.ID_User);
                            this.Hide();
                            f.ShowDialog();
                        }
                        else if(i.Role == "Admin1")
                        {
                            Form1 f = new Form1("Admin1", i.ID_User);
                            this.Hide();
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        lblWrongInfo.Text = "* Wrong Password. Try again";
                        tbxPassword.Clear();
                        return;
                    }
                }
            }
            lblWrongInfo.Text = "* Invalid Username";
            tbxPassword.Clear();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!InternetConnection.Instance.isConnected())
            {
                lblWrongInfo.Text = "* Connection error";
                return;
            }
            SignUpfrm f = new SignUpfrm();
            f.Show();
            this.Hide();
        }
        private void btnGuest_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1("User", "Guest");
            this.Hide();
            f.ShowDialog();
        }
        private void tbxUsername_Click(object sender, EventArgs e)
        {
            tbxUsername.Clear();
        }
        private void tbxPassword_Click(object sender, EventArgs e)
        {
            tbxPassword.Clear();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pnlLoginMove_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if(tbxPassword.UseSystemPasswordChar == true)
            {
                tbxPassword.UseSystemPasswordChar = false;
                return;
            }
            tbxPassword.UseSystemPasswordChar = true;
        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = true;
        }
    }
}
