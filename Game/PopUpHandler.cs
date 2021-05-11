using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{

    class PopUpHandler
    {
        Panel main;
        List<Timer> timers;

        Panel popUpBack;
        Panel background;
        PictureBox stars;
        Label title;
        Label Description;
        Label button;
        bool isEndScreen;

        AccountHandler account;
        public PopUpHandler(string titleText, string[] ButtonTexts, bool sort,Panel paste,List<Timer> playTimers, AccountHandler accountData, string Description = null)
        {
            if (sort)
            {
                account = accountData;
                timers = playTimers;
                main = paste;
                background = new Panel();
                background.Size = paste.Size;
                background.BackColor = Color.FromArgb(160, 0, 0, 0);
                main.Controls.Add(background);
                background.BringToFront();

                popUpBack = new Panel();
                popUpBack.Name = "backgroundUI";
                popUpBack.BackgroundImage = Image.FromFile("GUI/background2.png");
                popUpBack.Size = popUpBack.BackgroundImage.Size;
                popUpBack.BackgroundImageLayout = ImageLayout.Stretch;
                popUpBack.BackColor = Color.Transparent;
                popUpBack.Location = new Point(paste.Width / 2 - popUpBack.Width / 2, paste.Height / 2 - popUpBack.Height / 2);

                background.Controls.Add(popUpBack);
                popUpBack.BringToFront();

                title = new Label();
                title.Text = titleText;
                title.TextAlign = ContentAlignment.MiddleCenter;
                title.Width = 200;
                title.ForeColor = Color.White;
                title.Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                title.Height = title.Font.Height;

                title.Location = new Point(popUpBack.Width / 2 - title.Width / 2, 140);

                popUpBack.Controls.Add(title);
                title.BringToFront();

                for (int i = 0; i < ButtonTexts.Length; i++)
                {
                    button = new Label();
                    button.BackgroundImage = Image.FromFile("GUI/button.png");
                    button.Size = new Size(150, 40);
                    button.Location = new Point(popUpBack.Width / 2 - button.Width / 2, ((popUpBack.Height - 200) / ButtonTexts.Length) * i + 200);
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.BackColor = Color.Transparent;
                    button.Text = ButtonTexts[i];
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    switch (button.Text)
                    {
                        case "Menu":
                            button.Click += new EventHandler(openShop);
                            break;
                        case "Quit":
                            button.Click += new EventHandler(quitForm);
                            break;
                        case "Resume":
                            button.Click += new EventHandler(resumeGame);
                            break;
                    }

                    popUpBack.Controls.Add(button);
                    button.BringToFront();
                }
            }
            else
            {

            }
            
        }

        protected void openShop(object sender, EventArgs e)
        {
            bool opened = false;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Console.WriteLine(Application.OpenForms[i].Name);
                if (Application.OpenForms[i].Name == "MainScreen")
                {
                    Application.OpenForms[i].Show();
                    Application.OpenForms[i].BringToFront();
                    opened = true;
                }
            }
            if (!opened)
            {
                MainScreen menu = new MainScreen(account);
                menu.Show();
            }
        }
        protected void resumeGame(object sender, EventArgs e)
        {
            main.Controls.Remove(background);
           foreach(Timer timer in timers)
            {
                timer.Start();
            }
        }
        protected void quitForm(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public Panel getPopUp()
        {
            return background;
        }
    }
}
