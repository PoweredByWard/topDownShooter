namespace Game
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.pbRegister = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRegisterRepeatPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegisterPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegisterUsername = new System.Windows.Forms.TextBox();
            this.lblToLogin = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pbLogin = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoginUsername = new System.Windows.Forms.TextBox();
            this.lblToRegister = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRegister)).BeginInit();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegister
            // 
            this.pnlRegister.BackColor = System.Drawing.Color.Transparent;
            this.pnlRegister.Controls.Add(this.pbRegister);
            this.pnlRegister.Controls.Add(this.label6);
            this.pnlRegister.Controls.Add(this.txtRegisterRepeatPassword);
            this.pnlRegister.Controls.Add(this.label3);
            this.pnlRegister.Controls.Add(this.txtRegisterPassword);
            this.pnlRegister.Controls.Add(this.label4);
            this.pnlRegister.Controls.Add(this.txtRegisterUsername);
            this.pnlRegister.Controls.Add(this.lblToLogin);
            this.pnlRegister.Location = new System.Drawing.Point(34, 18);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(303, 223);
            this.pnlRegister.TabIndex = 0;
            this.pnlRegister.Visible = false;
            // 
            // pbRegister
            // 
            this.pbRegister.BackColor = System.Drawing.Color.Transparent;
            this.pbRegister.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRegister.BackgroundImage")));
            this.pbRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRegister.Location = new System.Drawing.Point(73, 97);
            this.pbRegister.Name = "pbRegister";
            this.pbRegister.Size = new System.Drawing.Size(182, 50);
            this.pbRegister.TabIndex = 6;
            this.pbRegister.TabStop = false;
            this.pbRegister.Click += new System.EventHandler(this.pbRegister_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 65);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(120, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Repeat assword:";
            // 
            // txtRegisterRepeatPassword
            // 
            this.txtRegisterRepeatPassword.BackColor = System.Drawing.Color.White;
            this.txtRegisterRepeatPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegisterRepeatPassword.Location = new System.Drawing.Point(131, 67);
            this.txtRegisterRepeatPassword.Name = "txtRegisterRepeatPassword";
            this.txtRegisterRepeatPassword.PasswordChar = '*';
            this.txtRegisterRepeatPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRegisterRepeatPassword.Size = new System.Drawing.Size(161, 20);
            this.txtRegisterRepeatPassword.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 40);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password:";
            // 
            // txtRegisterPassword
            // 
            this.txtRegisterPassword.BackColor = System.Drawing.Color.White;
            this.txtRegisterPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegisterPassword.Location = new System.Drawing.Point(131, 40);
            this.txtRegisterPassword.Name = "txtRegisterPassword";
            this.txtRegisterPassword.PasswordChar = '*';
            this.txtRegisterPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRegisterPassword.Size = new System.Drawing.Size(161, 20);
            this.txtRegisterPassword.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 14);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(81, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Username:";
            // 
            // txtRegisterUsername
            // 
            this.txtRegisterUsername.BackColor = System.Drawing.Color.White;
            this.txtRegisterUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegisterUsername.Location = new System.Drawing.Point(131, 14);
            this.txtRegisterUsername.Name = "txtRegisterUsername";
            this.txtRegisterUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRegisterUsername.Size = new System.Drawing.Size(161, 20);
            this.txtRegisterUsername.TabIndex = 7;
            // 
            // lblToLogin
            // 
            this.lblToLogin.AutoSize = true;
            this.lblToLogin.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblToLogin.Location = new System.Drawing.Point(233, 206);
            this.lblToLogin.Name = "lblToLogin";
            this.lblToLogin.Size = new System.Drawing.Size(70, 13);
            this.lblToLogin.TabIndex = 6;
            this.lblToLogin.Text = "Login instead";
            this.lblToLogin.Click += new System.EventHandler(this.lblToLogin_Click);
            this.lblToLogin.MouseEnter += new System.EventHandler(this.hover);
            this.lblToLogin.MouseLeave += new System.EventHandler(this.outHover);
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.pbLogin);
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.txtLoginPassword);
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Controls.Add(this.txtLoginUsername);
            this.pnlLogin.Controls.Add(this.lblToRegister);
            this.pnlLogin.Location = new System.Drawing.Point(75, 42);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlLogin.Size = new System.Drawing.Size(262, 199);
            this.pnlLogin.TabIndex = 1;
            // 
            // pbLogin
            // 
            this.pbLogin.BackColor = System.Drawing.Color.Transparent;
            this.pbLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbLogin.BackgroundImage")));
            this.pbLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogin.Location = new System.Drawing.Point(32, 72);
            this.pbLogin.Name = "pbLogin";
            this.pbLogin.Size = new System.Drawing.Size(182, 48);
            this.pbLogin.TabIndex = 14;
            this.pbLogin.TabStop = false;
            this.pbLogin.Click += new System.EventHandler(this.pbLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.BackColor = System.Drawing.Color.White;
            this.txtLoginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginPassword.Location = new System.Drawing.Point(90, 43);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(161, 20);
            this.txtLoginPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // txtLoginUsername
            // 
            this.txtLoginUsername.BackColor = System.Drawing.Color.White;
            this.txtLoginUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginUsername.Location = new System.Drawing.Point(90, 17);
            this.txtLoginUsername.Name = "txtLoginUsername";
            this.txtLoginUsername.Size = new System.Drawing.Size(161, 20);
            this.txtLoginUsername.TabIndex = 1;
            // 
            // lblToRegister
            // 
            this.lblToRegister.AutoSize = true;
            this.lblToRegister.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblToRegister.Location = new System.Drawing.Point(184, 182);
            this.lblToRegister.Name = "lblToRegister";
            this.lblToRegister.Size = new System.Drawing.Size(78, 13);
            this.lblToRegister.TabIndex = 0;
            this.lblToRegister.Text = "Regiter instead";
            this.lblToRegister.Click += new System.EventHandler(this.lblToRegister_Click);
            this.lblToRegister.MouseEnter += new System.EventHandler(this.hover);
            this.lblToRegister.MouseLeave += new System.EventHandler(this.outHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(107, 169);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 48);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(406, 257);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlRegister);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "start";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRegister)).EndInit();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoginUsername;
        private System.Windows.Forms.Label lblToRegister;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRegisterRepeatPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegisterPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRegisterUsername;
        private System.Windows.Forms.Label lblToLogin;
        private System.Windows.Forms.PictureBox pbRegister;
        private System.Windows.Forms.PictureBox pbLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}