﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class GameForm : Form
    {
        GameHandler game;

        public GameForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlMain, new object[] { true });
            if (DataHandler.isFirstTime())
            {
                Utils.showInfoMessage();
                game = new GameHandler(pnlMain, drawTimer, this);
                DataHandler.didFirstTime();
            }
            else
            {
                game = new GameHandler(pnlMain, drawTimer, this);
            }
            Bitmap cursor = new Bitmap("GUI/crosshair.png");
            this.Cursor = new Cursor(cursor.GetHicon());
            drawTimer.Start();
        }

        private void paintEvent(object sender, PaintEventArgs e)
        {
            game.move();
            game.draw(e.Graphics);
            game.checkInputs();
        }



        private void timerDrawTick(object sender, EventArgs e)
        {
            pnlMain.Refresh();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.addInput(e);
            game.checkInputs();
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.removeInput(e);
            game.checkInputs();
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            game.setMouseLocation(new Point(e.X, e.Y));
        }

        private void pnlMain_MouseClick(object sender, MouseEventArgs e)
        {
            game.shoot(e);
        }
    }
}
