using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GameHandler
    {
        List<BulletHandler> bullets = new List<BulletHandler>();
        List<ZombieHandler> zombies = new List<ZombieHandler>();
        List<Keys> inputs = new List<Keys>();

        List<RectangleF> zombiesContainers = new List<RectangleF>();
        List<RectangleF> bulletsContainers = new List<RectangleF>();
        List<RectangleF> boxesContainers = new List<RectangleF>();
        RectangleF playerContainer;

        PlayerHandler player;
        InventoryHandler inventory;
        BulletHandler bulletHndlr;
        ZombieHandler zombieHndlr;
        Utils util = new Utils();

        Graphics g;
        Point mouseLocation;
        Point playerMove;

        Timer spawnsTimer = new Timer();
        Random rn;

        Panel pnlMain;
        public GameHandler(Panel pnl)
        {
            player = new PlayerHandler(500, 500);
            SizeF playerSize = player.getSkin().Size;
            playerSize.Width = playerSize.Width > playerSize.Height ? playerSize.Width : playerSize.Height;
            playerSize.Height = playerSize.Width > playerSize.Height ? playerSize.Width : playerSize.Height;
            playerContainer = new RectangleF(player.getPosition(),playerSize);

            inventory = new InventoryHandler(player.getPosition());

            pnlMain = pnl;

            spawnsTimer.Interval = 5000;
            spawnsTimer.Tick += new EventHandler(spawnTimer);
            spawnsTimer.Start();

            rn = new Random();

        }

        public void setMouseLocation(Point mouseData)
        {
            mouseLocation = mouseData;
        }

        public void addInput(KeyEventArgs e)
        {
            if(!inputs.Contains(e.KeyCode)) inputs.Add(e.KeyCode);
        }

        public void removeInput(KeyEventArgs e)
        {
            inputs.Remove(e.KeyCode);
        }

        public void checkInputs()
        {
            int moveX = 0;
            int moveY = 0;
            foreach (Keys key in inputs)
            {
                switch (key)
                {
                    case Keys.Up:
                        moveY--;
                        break;
                    case Keys.Down:
                        moveY++;
                        break;
                    case Keys.Right:
                        moveX++;
                        break;
                    case Keys.Left:
                        moveX--;
                        break;
                    case Keys.A:
                        inventory.setActiveWeapon(1);
                        break;
                }
            }
            playerMove = new Point(moveX, moveY);
        }

        public void createMap()
        {
            int xRoad = rn.Next(1, 29);
            int yRoad = rn.Next(1, 19);
            int[,] mapPanels = new int[30, 20];
            Bitmap res = new Bitmap(pnlMain.BackgroundImage,pnlMain.Width, pnlMain.Height);
            using (Graphics background = Graphics.FromImage(res))
            {
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (i == xRoad && j == yRoad)
                        {
                            background.DrawImage(Image.FromFile("Textures/3.png"), i * 50, j * 50);
                            mapPanels[i, j] = 3;
                        }
                        else if (i == xRoad)
                        {
                            background.DrawImage(Image.FromFile("Textures/2.png"), i * 50, j * 50);
                            mapPanels[i, j] = 1;
                        }
                        else if (j == yRoad)
                        {
                            background.DrawImage(Image.FromFile("Textures/1.png"), i * 50, j * 50);
                            mapPanels[i, j] = 2;
                        }
                        else
                        {
                            bool boxNear = mapPanels[i != 0 ? i - 1 : i, j] != 10 && mapPanels[i, j != 0 ? j - 1 : j] != 10 && mapPanels[i != 0 ? i - 1 : i, j != 0 ? j - 1 : j] != 10 && mapPanels[i != 29 ? i + 1 : i, j != 19 ? j + 1 : j] != 10 && mapPanels[i != 29 ? i + 1 : i, j] != 10 && mapPanels[i, j != 19 ? j + 1 : j] != 10 && mapPanels[i != 0 ? i - 1 : i, j != 19 ? j + 1 : j] != 10 && mapPanels[i != 29 ? i + 1 : i, j != 0 ? j - 1 : j] != 10 ? false : true;
                            bool roadNear = i == xRoad + 1 || i == xRoad - 1 || j == yRoad + 1 || j == yRoad - 1 ? true : false;
                            bool isbox = rn.Next(0, 20) == 1 ? true : false;
                            if(isbox && !boxNear && !roadNear)
                            {
                                background.DrawImage(Image.FromFile("Textures/10.png"), i * 50, j * 50);
                                mapPanels[i, j] = 10;
                                boxesContainers.Add(new RectangleF(i * 50+10, j * 50 + 10, 50, 50));
                            }
                            else
                            {
                                background.DrawImage(Image.FromFile("Textures/0.png"), i * 50, j * 50);
                                mapPanels[i, j] = 0;
                            }
                        }
                    }
                }
            }
            pnlMain.BackgroundImage = res;

        }
        public void shoot(MouseEventArgs e)
        {
            if (player.getLastShot() < DateTime.Now.AddMilliseconds(-player.getShootDelay(inventory.getActiveWeaponNumber())))
            {
                Point playerCenterPosition = player.getPosition();
                int activeWeapon = inventory.getActiveWeaponNumber();
                double recoil = rn.Next(-(player.getWeaponRecoil(activeWeapon) / 2), (player.getWeaponRecoil(activeWeapon) / 2));
                bulletHndlr = new BulletHandler(playerCenterPosition.X, playerCenterPosition.Y, player.getAngle() + recoil, e.Location, activeWeapon);
                bullets.Add(bulletHndlr);

                SizeF bulletSize = bulletHndlr.getBullet().Size;
                bulletSize.Width = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;
                bulletSize.Height = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;

                bulletsContainers.Add(new RectangleF(bulletHndlr.getLocation(), bulletSize));

                player.setLastShot();
            }
        }

        public void move()
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                bool deleted = false;
                for (int j = 0; j < bulletsContainers.Count; j++)
                {
                    if (zombiesContainers[i].IntersectsWith(bulletsContainers[j]))
                    {
                        bullets.RemoveAt(j);
                        bulletsContainers.RemoveAt(j);
                        deleted = zombies[i].damage(20);
                        if (deleted)
                        {
                            zombies.RemoveAt(i);
                            zombiesContainers.RemoveAt(i);
                        }
                    }
                }
                if (!deleted)
                {
                    bool[] zombieCollisions = {false,false,false,false};
                    if (zombiesContainers[i].IntersectsWith(playerContainer))
                    {
                        if (zombies[i].attack())
                        {
                            if (player.damage(zombies[i].getDamage()))
                            {
                                Console.WriteLine("game over");
                            }
                        }
                    }
                    for (int j = 0; j < zombies.Count; j++)
                    {
                        if(i!= j)
                        {
                            if (zombiesContainers[i].IntersectsWith(zombiesContainers[j]))
                            {
                                bool[] collision = util.checkCollision(zombiesContainers[i], zombiesContainers[j]);
                                for (int c = 0; c < collision.Length; c++)
                                {
                                    if (collision[c])
                                    {
                                        zombieCollisions[c] = true;
                                    }
                                }
                            }
                        }
                    }
                    zombies[i].setDestenation(player.getCenterPosition());
                    zombies[i].move(zombieCollisions);

                    RectangleF recZombie = zombiesContainers[i];
                    recZombie.Location = zombies[i].getLocation();
                    zombiesContainers[i] = recZombie;
                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].move();
                RectangleF recBullet = bulletsContainers[i];
                recBullet.Location = bullets[i].getLocation();
                bulletsContainers[i] = recBullet;
            }
            if (player.getPosition().Y >= pnlMain.Height && playerMove.Y>0) playerMove.Y = 0;
            if (player.getPosition().X >= pnlMain.Width && playerMove.X > 0) playerMove.X = 0;
            if (player.getPosition().Y <= 0 && playerMove.Y < 0) playerMove.Y = 0;
            if (player.getPosition().X <= 0 && playerMove.X < 0) playerMove.X = 0;
            bool[] collisions = { false, false, false, false };
            for (int i = 0; i < boxesContainers.Count; i++)
            {
                if (playerContainer.IntersectsWith(boxesContainers[i]))
                {
                    bool[] collision = util.checkCollision(playerContainer, boxesContainers[i]);
                    for (int c = 0; c < collision.Length; c++)
                    {
                        if (collision[c])
                        {
                            collisions[c] = true;
                        }
                    }
                }
            }
            player.move(playerMove.X, playerMove.Y, mouseLocation, collisions);
            playerContainer.X = player.getPosition().X;
            playerContainer.Y = player.getPosition().Y;
        }

        public void draw(Graphics gData)
        {
            g = gData;
            for (int i = 0; i < zombies.Count; i++)
            {
                Bitmap zombieSkin = (Bitmap)zombies[i].getSkin();
                zombieSkin = util.rotateImg(zombieSkin, zombies[i].getAngle());

                g.FillRectangle(Brushes.Red, zombies[i].getLocation().X + zombies[i].getHpPercent(), zombies[i].getLocation().Y - 40, 100 - zombies[i].getHpPercent(), 10);
                g.FillRectangle(Brushes.Green, zombies[i].getLocation().X, zombies[i].getLocation().Y-40, zombies[i].getHpPercent(), 10);
                g.DrawImage(zombieSkin, zombies[i].getLocation());
            }
            foreach (BulletHandler bullet in bullets)
            {
                Bitmap bulletSkin = (Bitmap)bullet.getBullet();
                bulletSkin = util.rotateImg(bulletSkin, bullet.getAngle());
                g.DrawImage(bulletSkin, bullet.getLocation());
            }

            Bitmap weaponSkin = (Bitmap)inventory.getActiveWeapon();
            inventory.setAngle(player.getAngle());
            inventory.setLocation(player.getPosition());
            weaponSkin = util.rotateImg(weaponSkin, player.getAngle());
            g.DrawImage(weaponSkin, inventory.getLocation());

            Bitmap playerSkin = (Bitmap)player.getSkin();
            playerSkin = util.rotateImg(playerSkin, player.getAngle());
            g.DrawImage(playerSkin, player.getCenterPosition());
            Bitmap healthBar = util.createHealthBar(player.getMaxHealt(), player.getHealt(), 120, 10);
            g.DrawImage(healthBar, player.getPosition());

        }

        void spawnTimer(Object sender, EventArgs e)
        {
            zombieHndlr = new ZombieHandler(0, 0, 2, 0, 100, player.getCenterPosition());
            zombies.Add(zombieHndlr);

            SizeF zombieSize = zombieHndlr.getSkin().Size;
            zombieSize.Width = zombieSize.Width > zombieSize.Height ? zombieSize.Width : zombieSize.Height;
            zombieSize.Height = zombieSize.Width > zombieSize.Height ? zombieSize.Width : zombieSize.Height;

            zombiesContainers.Add(new RectangleF(zombieHndlr.getLocation(), zombieSize));
        }
    }
}
