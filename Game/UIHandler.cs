using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    
    class UIHandler
    {
        List<Image> abilities = new List<Image>();
        List<int> abilitiePrices = new List<int>();

        private int coins;
        Size backgroundSize;

        PlayerHandler player;
        GameHandler game;
        
        public UIHandler(PlayerHandler playerData,GameHandler gameData)
        {
            game = gameData;
            player = playerData;
            Ability item = new Ability("item1.png", 3000);
            abilities.Add(item.getAbility());
            abilitiePrices.Add(item.getPrice());

            item = new Ability("item2.png", 400);
            abilities.Add(item.getAbility());
            abilitiePrices.Add(item.getPrice());

            item = new Ability("item3.png", 400);
            abilities.Add(item.getAbility());
            abilitiePrices.Add(item.getPrice());

            coins = 1000;
        }



        public Bitmap drawUI(Bitmap ui)
        {
            backgroundSize = ui.Size;
            using (Graphics g = Graphics.FromImage(ui))
            {
                g.DrawImage(Image.FromFile("GUI/dollar.png"), new Point(backgroundSize.Width - (200), 5));
                for (int i = 0; i < abilities.Count; i++)
                {
                    g.DrawImage(abilities[i], new Point(ui.Width - (abilities[i].Width + 15), i * 50 + 60));
                }
            }
            return ui;
        }

        public int getCoins()
        {
            return coins;
        }

        public void addCoins(int amount)
        {
            coins += amount;
        }

        public List<RectangleF> getRectangles()
        {
            List<RectangleF> hitBoxes = new List<RectangleF>();

            for (int i = 0; i < abilities.Count; i++)
            {
                hitBoxes.Add(new RectangleF(new Point(backgroundSize.Width - (abilities[i].Width + 15), i * 50 + 60),abilities[i].Size));
            }

            return hitBoxes;
        }

        public void itemClicked(int item)
        {
            if (coins>=abilitiePrices[item])
            {
                coins -= abilitiePrices[item];
                switch (item)
                {
                    case 0:
                        if (player.getMaxHealt()!= player.getHealt())
                        {
                            player.setHealth(player.getMaxHealt());
                        }
                        else
                        {
                            coins += abilitiePrices[item];
                        }
                        break;
                    case 1:
                        game.addTurret(0);
                        break;
                    case 2:
                        game.addTurret(1);
                        break;
                }
            }
        }

    }
}
