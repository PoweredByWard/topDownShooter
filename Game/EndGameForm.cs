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
    public partial class EndGameForm : Form
    {
        int wave;
        int turrets;
        int damageDealt;
        int kills;
        int coins;
        TimeSpan duration;

        const int ROW_HEIGHT = 24;
        public EndGameForm(int waveData,int turretsData,int damageDealtData,int killsData, TimeSpan durationData)
        {
            wave = waveData;
            turrets = turretsData;
            damageDealt = damageDealtData;
            kills = killsData;
            duration = durationData;
            
            coins = Utils.calculateScore(kills, duration.TotalMinutes); 
            

            InitializeComponent();
            createBoard();
        }

        private void createBoard()
        {
            Console.WriteLine(kills);
            
            lblCoin.Text = coins.ToString();

            tlpStats.RowCount = 4;
            tlpStats.RowStyles.RemoveAt(0);
            Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            ContentAlignment align = ContentAlignment.TopLeft;
            AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Wave:", Font = lblFont, ForeColor = Color.White, Anchor = anchor },0,0);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = wave.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 0);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Kills:", Font = lblFont, ForeColor = Color.White, Anchor = anchor },0,1);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = kills.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 1);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Damage dealt:", Font = lblFont, ForeColor = Color.White, Anchor = anchor },0,2);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = damageDealt.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 2);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Turrets placed:", Font = lblFont, ForeColor = Color.White, Anchor = anchor },0,3);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = turrets.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 3);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Game duration:", Font = lblFont, ForeColor = Color.White, Anchor = anchor },0,4);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text =  $"{Math.Floor(duration.TotalMinutes)}:{duration.Seconds}", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 4);



        }

        private void openMenu()
        {
            bool opened = false;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Console.WriteLine(Application.OpenForms[i].Name);
                if (Application.OpenForms[i].Name == "MainScreen")
                {
                    Application.OpenForms[i].Show();
                    Application.OpenForms[i].BringToFront();
                    Application.OpenForms[i].Focus();
                    opened = true;
                }
                if (Application.OpenForms[i].Name == "GameForm")
                {
                    Application.OpenForms[i].Dispose();
                }
            }
            if (!opened)
            {
                MainScreen menu = new MainScreen();
                menu.Show();
                menu.Focus();
            }
            this.Dispose();
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            DataHandler.saveGame(wave,kills,turrets,damageDealt,duration);
            DataHandler.setCoins(DataHandler.getCoins()+coins);
            openMenu();
        }

        private void pbCancel_Click(object sender, EventArgs e)
        {
            openMenu();
        }
    }
}
