﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class StartForm : Form
    {
        string username;
        string password;
        public StartForm()
        {
            InitializeComponent();
        }


        private void lblToRegister_Click(object sender, EventArgs e)
        {
            pnlRegister.Visible = true;
            pnlLogin.Visible = false;
        }

        private void hover(object sender, EventArgs e)
        {
            Label senderlbl = (Label)sender;
            senderlbl.Font = new Font(senderlbl.Font.Name, senderlbl.Font.SizeInPoints, FontStyle.Underline);
        }

        private void outHover(object sender, EventArgs e)
        {
            Label senderlbl = (Label)sender;
            senderlbl.Font = new Font(senderlbl.Font.Name, senderlbl.Font.SizeInPoints, FontStyle.Regular);
        }

        private void lblToLogin_Click(object sender, EventArgs e)
        {
            pnlRegister.Visible = false;
            pnlLogin.Visible = true;
        }


        private bool checkInputs(string type)
        {
            if (type=="Login")
            {
                username = txtLoginUsername.Text;
                password = txtLoginPassword.Text;
                if (txtLoginUsername.Text.Length <= 3) {
                    MessageBox.Show("Please use a valid username!");
                    return false;
                }else if (txtLoginPassword.Text.Length <= 5)
                {
                    MessageBox.Show("Please use a valid Password!");
                    return false;
                }
            }
            else
            {
                username = txtRegisterUsername.Text;
                password = txtRegisterPassword.Text;

                if (password.Length <= 3)
                {
                    MessageBox.Show("Your username has to be longer then 3 karaketers.");
                    return false;
                }
                else if (DataHandler.isTaken(txtRegisterUsername.Text))
                {
                    MessageBox.Show("This username is already in use.");
                    return false;
                }

                return Utils.checkPassword(txtRegisterPassword.Text,txtRegisterRepeatPassword.Text);
            }
            return true;
        }

        private void pbLogin_Click(object sender, EventArgs e)
        {

            bool valid = checkInputs("Login");
            if (valid)
            {
                if (DataHandler.loginUser(username, password))
                {
                    AccountHandler.setUsername(username);
                    ResourceHandler.checkResources();
                    MainScreen mainScreen = new MainScreen();
                    mainScreen.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("wrong credentials, Invalid username or password!");
                }
            }
        }

        private void pbRegister_Click(object sender, EventArgs e)
        {
            bool valid = checkInputs("Register");
            if (valid)
            {
                AccountHandler.setUsername(username);
                if (DataHandler.createUser(username, password))
                {
                    ResourceHandler.checkResources();
                    MainScreen mainScreen = new MainScreen();
                    mainScreen.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Whoops something went wrong.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) => Utils.quit();
    }
}
