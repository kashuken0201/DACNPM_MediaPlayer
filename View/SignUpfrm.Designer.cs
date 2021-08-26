
namespace MediaPlayer.View
{
    partial class SignUpfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUpfrm));
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxPass = new System.Windows.Forms.TextBox();
            this.tbxCheckPass = new System.Windows.Forms.TextBox();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.tbxSDT = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlSignUpMove = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblWrongName = new System.Windows.Forms.Label();
            this.lblWrongPass = new System.Windows.Forms.Label();
            this.lblWrongCheck = new System.Windows.Forms.Label();
            this.lblWrongSDT = new System.Windows.Forms.Label();
            this.lblAgree = new System.Windows.Forms.Label();
            this.btnShowPass = new System.Windows.Forms.Button();
            this.btnShowCheckPass = new System.Windows.Forms.Button();
            this.lblWrongEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxName
            // 
            this.tbxName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.tbxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxName.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxName.ForeColor = System.Drawing.Color.White;
            this.tbxName.Location = new System.Drawing.Point(100, 130);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(398, 20);
            this.tbxName.TabIndex = 1;
            this.tbxName.Text = "Tên đăng nhập";
            this.tbxName.Click += new System.EventHandler(this.tbxName_Click);
            // 
            // tbxPass
            // 
            this.tbxPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.tbxPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxPass.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPass.ForeColor = System.Drawing.Color.White;
            this.tbxPass.Location = new System.Drawing.Point(100, 200);
            this.tbxPass.Name = "tbxPass";
            this.tbxPass.Size = new System.Drawing.Size(398, 20);
            this.tbxPass.TabIndex = 2;
            this.tbxPass.Text = "Mật khẩu";
            this.tbxPass.Click += new System.EventHandler(this.tbxPass_Click);
            this.tbxPass.TextChanged += new System.EventHandler(this.tbxPass_TextChanged);
            // 
            // tbxCheckPass
            // 
            this.tbxCheckPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.tbxCheckPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxCheckPass.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCheckPass.ForeColor = System.Drawing.Color.White;
            this.tbxCheckPass.Location = new System.Drawing.Point(100, 270);
            this.tbxCheckPass.Name = "tbxCheckPass";
            this.tbxCheckPass.Size = new System.Drawing.Size(398, 20);
            this.tbxCheckPass.TabIndex = 3;
            this.tbxCheckPass.Text = "Nhập lại mật khẩu";
            this.tbxCheckPass.Click += new System.EventHandler(this.tbxCheckPass_Click);
            this.tbxCheckPass.TextChanged += new System.EventHandler(this.tbxCheckPass_TextChanged);
            // 
            // tbxEmail
            // 
            this.tbxEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.tbxEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxEmail.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEmail.ForeColor = System.Drawing.Color.White;
            this.tbxEmail.Location = new System.Drawing.Point(100, 340);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(398, 20);
            this.tbxEmail.TabIndex = 4;
            this.tbxEmail.Text = "Email";
            this.tbxEmail.Click += new System.EventHandler(this.tbxEmail_Click);
            // 
            // tbxSDT
            // 
            this.tbxSDT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.tbxSDT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxSDT.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSDT.ForeColor = System.Drawing.Color.White;
            this.tbxSDT.Location = new System.Drawing.Point(100, 410);
            this.tbxSDT.Name = "tbxSDT";
            this.tbxSDT.Size = new System.Drawing.Size(398, 20);
            this.tbxSDT.TabIndex = 5;
            this.tbxSDT.Text = "Số điện thoại";
            this.tbxSDT.Click += new System.EventHandler(this.tbxSDT_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblTitle.Location = new System.Drawing.Point(220, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 38);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Đăng ký";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Cyan;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.btnRegister.Location = new System.Drawing.Point(100, 475);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(398, 50);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Register Now";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(100, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 1);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(101, 223);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 1);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Location = new System.Drawing.Point(100, 293);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(397, 1);
            this.panel3.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Location = new System.Drawing.Point(101, 363);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(397, 1);
            this.panel4.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Location = new System.Drawing.Point(100, 432);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(397, 1);
            this.panel5.TabIndex = 10;
            // 
            // pnlSignUpMove
            // 
            this.pnlSignUpMove.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSignUpMove.Location = new System.Drawing.Point(0, 0);
            this.pnlSignUpMove.Name = "pnlSignUpMove";
            this.pnlSignUpMove.Size = new System.Drawing.Size(600, 30);
            this.pnlSignUpMove.TabIndex = 12;
            this.pnlSignUpMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlSignUpMove_MouseDown);
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::MediaPlayer.Properties.Resources.icons8_left_arrow_32;
            this.btnBack.Location = new System.Drawing.Point(560, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 40);
            this.btnBack.TabIndex = 11;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblWrongName
            // 
            this.lblWrongName.AutoSize = true;
            this.lblWrongName.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrongName.ForeColor = System.Drawing.Color.Red;
            this.lblWrongName.Location = new System.Drawing.Point(100, 160);
            this.lblWrongName.Name = "lblWrongName";
            this.lblWrongName.Size = new System.Drawing.Size(0, 16);
            this.lblWrongName.TabIndex = 13;
            // 
            // lblWrongPass
            // 
            this.lblWrongPass.AutoSize = true;
            this.lblWrongPass.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrongPass.ForeColor = System.Drawing.Color.Red;
            this.lblWrongPass.Location = new System.Drawing.Point(100, 229);
            this.lblWrongPass.Name = "lblWrongPass";
            this.lblWrongPass.Size = new System.Drawing.Size(0, 16);
            this.lblWrongPass.TabIndex = 14;
            // 
            // lblWrongCheck
            // 
            this.lblWrongCheck.AutoSize = true;
            this.lblWrongCheck.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrongCheck.ForeColor = System.Drawing.Color.Red;
            this.lblWrongCheck.Location = new System.Drawing.Point(100, 300);
            this.lblWrongCheck.Name = "lblWrongCheck";
            this.lblWrongCheck.Size = new System.Drawing.Size(0, 16);
            this.lblWrongCheck.TabIndex = 15;
            // 
            // lblWrongSDT
            // 
            this.lblWrongSDT.AutoSize = true;
            this.lblWrongSDT.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrongSDT.ForeColor = System.Drawing.Color.Red;
            this.lblWrongSDT.Location = new System.Drawing.Point(100, 440);
            this.lblWrongSDT.Name = "lblWrongSDT";
            this.lblWrongSDT.Size = new System.Drawing.Size(0, 16);
            this.lblWrongSDT.TabIndex = 16;
            // 
            // lblAgree
            // 
            this.lblAgree.AutoSize = true;
            this.lblAgree.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgree.ForeColor = System.Drawing.Color.Lime;
            this.lblAgree.Location = new System.Drawing.Point(220, 550);
            this.lblAgree.Name = "lblAgree";
            this.lblAgree.Size = new System.Drawing.Size(175, 22);
            this.lblAgree.TabIndex = 17;
            this.lblAgree.Text = "Đăng ký thành công";
            // 
            // btnShowPass
            // 
            this.btnShowPass.FlatAppearance.BorderSize = 0;
            this.btnShowPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPass.Image = global::MediaPlayer.Properties.Resources.icons8_eye_16;
            this.btnShowPass.Location = new System.Drawing.Point(474, 198);
            this.btnShowPass.Name = "btnShowPass";
            this.btnShowPass.Size = new System.Drawing.Size(24, 24);
            this.btnShowPass.TabIndex = 19;
            this.btnShowPass.UseVisualStyleBackColor = true;
            this.btnShowPass.Click += new System.EventHandler(this.btnShowPass_Click);
            // 
            // btnShowCheckPass
            // 
            this.btnShowCheckPass.FlatAppearance.BorderSize = 0;
            this.btnShowCheckPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowCheckPass.Image = global::MediaPlayer.Properties.Resources.icons8_eye_16;
            this.btnShowCheckPass.Location = new System.Drawing.Point(474, 268);
            this.btnShowCheckPass.Name = "btnShowCheckPass";
            this.btnShowCheckPass.Size = new System.Drawing.Size(24, 24);
            this.btnShowCheckPass.TabIndex = 20;
            this.btnShowCheckPass.UseVisualStyleBackColor = true;
            this.btnShowCheckPass.Click += new System.EventHandler(this.btnShowCheckPass_Click);
            // 
            // lblWrongEmail
            // 
            this.lblWrongEmail.AutoSize = true;
            this.lblWrongEmail.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrongEmail.ForeColor = System.Drawing.Color.Red;
            this.lblWrongEmail.Location = new System.Drawing.Point(100, 370);
            this.lblWrongEmail.Name = "lblWrongEmail";
            this.lblWrongEmail.Size = new System.Drawing.Size(0, 16);
            this.lblWrongEmail.TabIndex = 21;
            // 
            // SignUpfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(600, 610);
            this.Controls.Add(this.lblWrongEmail);
            this.Controls.Add(this.btnShowCheckPass);
            this.Controls.Add(this.btnShowPass);
            this.Controls.Add(this.lblAgree);
            this.Controls.Add(this.lblWrongSDT);
            this.Controls.Add(this.lblWrongCheck);
            this.Controls.Add(this.lblWrongPass);
            this.Controls.Add(this.lblWrongName);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbxSDT);
            this.Controls.Add(this.tbxEmail);
            this.Controls.Add(this.tbxCheckPass);
            this.Controls.Add(this.tbxPass);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.pnlSignUpMove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SignUpfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignUpfrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxPass;
        private System.Windows.Forms.TextBox tbxCheckPass;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.TextBox tbxSDT;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlSignUpMove;
        private System.Windows.Forms.Label lblWrongName;
        private System.Windows.Forms.Label lblWrongPass;
        private System.Windows.Forms.Label lblWrongCheck;
        private System.Windows.Forms.Label lblWrongSDT;
        private System.Windows.Forms.Label lblAgree;
        private System.Windows.Forms.Button btnShowPass;
        private System.Windows.Forms.Button btnShowCheckPass;
        private System.Windows.Forms.Label lblWrongEmail;
    }
}