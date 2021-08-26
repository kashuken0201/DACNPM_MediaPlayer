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
    public partial class SignUpfrm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        public SignUpfrm()
        {
            InitializeComponent();
            lblAgree.Visible = false;
            this.Opacity = 0.97;
        }
        private void pnlSignUpMove_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void tbxName_Click(object sender, EventArgs e)
        {
            tbxName.Clear();
        }
        private void tbxPass_Click(object sender, EventArgs e)
        {
            tbxPass.Clear();
        }
        private void tbxCheckPass_Click(object sender, EventArgs e)
        {
            tbxCheckPass.Clear();
        }
        private void tbxEmail_Click(object sender, EventArgs e)
        {
            tbxEmail.Clear();
        }
        private void tbxSDT_Click(object sender, EventArgs e)
        {
            tbxSDT.Clear();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Close();
        }
        private void tbxCheckPass_TextChanged(object sender, EventArgs e)
        {
            tbxCheckPass.UseSystemPasswordChar = true;
        }
        private void tbxPass_TextChanged(object sender, EventArgs e)
        {
            tbxPass.UseSystemPasswordChar = true;
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (tbxPass.UseSystemPasswordChar == true)
            {
                tbxPass.UseSystemPasswordChar = false;
                return;
            }
            tbxPass.UseSystemPasswordChar = true;
        }

        private void btnShowCheckPass_Click(object sender, EventArgs e)
        {
            if (tbxCheckPass.UseSystemPasswordChar == true)
            {
                tbxCheckPass.UseSystemPasswordChar = false;
                return;
            }
            tbxCheckPass.UseSystemPasswordChar = true;
        }
        private void Register()
        {
            if (!InternetConnection.Instance.isConnected())
            {
                lblAgree.Text = "* Connection error";
                return;
            }

            // Kiểm tra tài khoản khách và unicode
            if (tbxName.Text.Equals("Guest") || BLL_QLSong.Instance.isUnicode(tbxName.Text.Trim()) || tbxName.Text.Trim().Equals(""))
            {
                lblWrongName.Text = "* Tài khoản không hợp lệ";
                lblWrongName.Visible = true;
                return;
            }
            else
            {
                lblWrongName.Visible = false;
            }

            // Kiểm tra tài khoản trùng
            if (!BLL_QLSong.Instance.CheckID_Name(tbxName.Text.Trim()))
            {
                lblWrongName.Text = "* Tài khoản đã tồn tại";
                lblWrongName.Visible = true;
                return;
            }
            else
            {
                lblWrongName.Visible = false;
            }

            // kiểm tra yêu cầu mật khẩu ko hợp lệ
            if (tbxPass.Text.Trim().Length < 8)
            {
                lblWrongPass.Text = "* Mật khẩu tối thiểu 8 kí tự";
                lblWrongPass.ForeColor = System.Drawing.Color.Red;
                lblWrongPass.Visible = true;
                return;
            }
            else
            {
                lblWrongPass.Visible = false;
            }
            if (BLL_QLSong.Instance.isUnicode(tbxPass.Text.Trim()) || tbxPass.Text.Trim().Equals(""))
            {
                lblWrongPass.Text = "* Mật khẩu không hợp lệ";
                lblWrongPass.Visible = true;
                lblWrongPass.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblWrongPass.Visible = false;
            }

            if (!tbxCheckPass.Text.Trim().Equals(tbxPass.Text.Trim()) || tbxCheckPass.Text.Trim().Equals(""))
            {
                lblWrongCheck.Text = "* Mật khẩu xác nhận sai";
                lblWrongCheck.ForeColor = System.Drawing.Color.Red;
                lblWrongCheck.Visible = true;
                return;
            }
            else
            {
                lblWrongCheck.Visible = false;
            }

            // Kiểm tra email
            if (!tbxEmail.Text.Contains("@gmail.com"))
            {
                lblWrongEmail.Text = "* Email không hợp lệ";
                lblWrongEmail.Visible = true;
                return;
            }
            else
            {
                lblWrongEmail.Visible = false;
            }
            // Kiểm tra số điện thoại
            if (!tbxSDT.Text.Trim().All(Char.IsDigit) || tbxSDT.Text.Trim().Length != 10)
            {
                lblWrongSDT.Text = "* Số điện thoại không hợp lệ";
                lblWrongSDT.Visible = true;
                return;
            }
            else
            {
                lblWrongSDT.Visible = false;
            }

            User u = new User()
            {
                ID_User = "US" + (BLL_QLSong.Instance.LastIndexOf("User") + 1).ToString(),
                ID_Name = tbxName.Text.Trim(),
                Password = BLL_QLSong.Instance.Encode(tbxPass.Text.Trim()),
                Email = tbxEmail.Text.Trim(),
                SDT = tbxSDT.Text.Trim(),
                Role = "User"
            };
            BLL_QLSong.Instance.AddUser(u);
            lblAgree.Visible = true;
        }
    }
}
