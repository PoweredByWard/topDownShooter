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
        List<Timer> timers = new List<Timer>();

        List<BulletHandler> bullets = new List<BulletHandler>();
        List<ZombieHandler> zombies = new List<ZombieHandler>();
        List<TurretHandler> turrets = new List<TurretHandler>();
        List<Keys> inputs = new List<Keys>();

        List<RectangleF> zombiesContainers = new List<RectangleF>();
        List<RectangleF> bulletsContainers = new List<RectangleF>();
        List<RectangleF> boxesContainers = new List<RectangleF>();
        List<RectangleF> uiContainers = new List<RectangleF>();
        List<RectangleF> turretRange = new List<RectangleF>();
        RectangleF mouseContainer;
        RectangleF playerContainer;


        List<Point> spawns = new List<Point>();

        PlayerHandler player;
        InventoryHandler inventory;
        BulletHandler bulletHndlr;
        ZombieHandler zombieHndlr;
        Utils util = new Utils();
        PopUpHandler popUp;
        WaveHandler waveHndlr;
        UIHandler uiHandler;
        AccountHandler account;

        Graphics g;
        Point mouseLocation;
        Point playerMove;

        Timer drawTimer;
        Random rn = new Random();

        Panel pnlMain;
        private int score;

        DateTime lastPaused;
        DateTime lastWave;
        DateTime lastFrame;

        Panel backgroundUI;
        Form gameForm;

        Cursor cursor1;
        Cursor cursor2;


        bool paused;
        public GameHandler(Panel pnl, Timer drawTimerData,Form gameFormData,AccountHandler accountData)
        {
            player = new PlayerHandler(500, 500);

            SizeF playerSize = player.getSkin().Size;
            playerContainer = new RectangleF(500, 500, playerSize.Width, playerSize.Height);

            inventory = new InventoryHandler(player.getPosition());
            uiHandler = new UIHandler(player,this);

            pnlMain = pnl;

            waveHndlr = new WaveHandler(this);

            timers.Add(drawTimerData);
            timers.Add(waveHndlr.getTimer());


            lastPaused = DateTime.Now.AddMilliseconds(-250);
            lastWave = DateTime.Now;

            drawTimer = drawTimerData;

            createMap();

            gameForm = gameFormData;
            account = accountData;


            score = 0;

            Bitmap cursor1Btm = new Bitmap("GUI/crosshair.png");
            Bitmap cursor2Btm = new Bitmap("GUI/crosshair2.png");
            cursor1 = new Cursor(cursor1Btm.GetHicon());
            cursor2 = new Cursor(cursor2Btm.GetHicon());

        }


        public void addTurret()
        {
            TurretHandler newTurret = new TurretHandler(player.getPosition(),2000);
            turrets.Add(newTurret);
            turretRange.Add(new RectangleF(newTurret.getLocation().X - 300, newTurret.getLocation().Y - 300, 600 + newTurret.getSkin().Width, 600 + newTurret.getSkin().Height));
        }

        public void setMouseLocation(Point mouseData)
        {
            mouseLocation = mouseData;
        }

        public void addInput(KeyEventArgs e)
        {
            if(!inputs.Contains(e.KeyCode)) inputs.Add(e.KeyCode);
        }

        public void addScore(int amount)
        {
            score += amount;
        }

        public int getScore()
        {
            return score;
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
                    case Keys.Escape:
                        if (lastPaused<DateTime.Now.AddMilliseconds(-400))
                        {
                        if (pnlMain.Controls.Count > 0) {
                            pauseGame(false);
                        }
                        else
                        {
                            pauseGame(true);
                        }
                        }
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
                                boxesContainers.Add(new RectangleF(i * 50+5, j * 50+15, Image.FromFile("Textures/10.png").Width-5, Image.FromFile("Textures/10.png").Height-5));
                            }
                            else
                            {
                                background.DrawImage(Image.FromFile("Textures/0.png"), i * 50, j * 50);
                                mapPanels[i, j] = 0;
                                spawns.Add(new Point(i * 50,j*50));
                            }
                        }
                    }
                }
            }
            pnlMain.BackgroundImage = uiHandler.drawUI(res);

        }
        public void shoot(MouseEventArgs e)
        {
            for (int i = 0; i < uiContainers.Count; i++)
            {

                if (mouseContainer.IntersectsWith(uiContainers[i]))
                {
                    uiHandler.itemClicked(i);
                }
            }

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
            checkMouse();
            checkUI();
            checkBullets();
            checkPlayer();
            checkTurrets();
            checkZombies();
        }

        private void checkTurrets()
        {
            for (int i = 0; i < turretRange.Count; i++)
            {
                bool hasTarget = false;
                for (int j = 0; j < zombies.Count; j++)
                {
                    if (turretRange[i].IntersectsWith(zombiesContainers[j]) && !hasTarget)
                    {
                        hasTarget = true;
                        turrets[i].setTarget(zombies[j].getCenterPosition());
                    }
                }


                if (turrets[i].getLastShot() < DateTime.Now.AddMilliseconds(-turrets[i].getDelay()) && hasTarget)
                {
                    Point turretPos = turrets[i].getLocation();
                    double recoil = rn.Next(-17,17);
                    bulletHndlr = new BulletHandler(turretPos.X, turretPos.Y, turrets[i].getAngle()-90 + recoil, turrets[i].getTarget(), 0);
                    bullets.Add(bulletHndlr);

                    SizeF bulletSize = bulletHndlr.getBullet().Size;
                    bulletSize.Width = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;
                    bulletSize.Height = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;

                    bulletsContainers.Add(new RectangleF(bulletHndlr.getLocation(), bulletSize));

                    turrets[i].setLastShot();
                }

            }
        }

        private void checkMouse()
        {
            bool hover = false;
            mouseContainer = new RectangleF(mouseLocation, new Size(1, 1));
            for (int i = 0; i < uiContainers.Count; i++)
            {
                if (mouseContainer.IntersectsWith(uiContainers[i]))
                {
                    gameForm.Cursor = cursor1;
                    hover = true;
                }
            }
            for (int i = 0; i < zombiesContainers.Count; i++)
            {
                if (mouseContainer.IntersectsWith(zombiesContainers[i]))
                {
                    hover = true;
                }
            }
            if (!hover)
            {
                try
                {
                    gameForm.Cursor = cursor1;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    gameForm.Cursor = cursor2;
                }
                catch (Exception)
                {
                }
            }
        }

        private void checkUI()
        {
            uiContainers = uiHandler.getRectangles();
        }

        public void checkPlayer()
        {
            if (player.getPosition().Y >= pnlMain.Height && playerMove.Y > 0) playerMove.Y = 0;
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
            for (int i = 0; i < zombiesContainers.Count; i++)
            {

                if (playerContainer.IntersectsWith(zombiesContainers[i]))
                {
                    bool[] collision = util.checkCollision(playerContainer,zombiesContainers[i]);
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

            playerContainer.Location = player.getPosition();
        }

        public void checkBullets()
        {
            for (int i = 0; i < bulletsContainers.Count; i++)
            {
                bool deleted = false;
                int bulletX = bullets[i].getLocation().X;
                int bulletY = bullets[i].getLocation().Y;
                if (bulletX < 0|| bulletY < 0 || bulletX>pnlMain.Width || bulletY>pnlMain.Height)
                {
                    bullets.RemoveAt(i);
                    bulletsContainers.RemoveAt(i);
                    deleted = true;
                }
                for (int j = 0; j < boxesContainers.Count; j++)
                {
                    if (!deleted)
                    {
                        if (bulletsContainers[i].IntersectsWith(boxesContainers[j]))
                        {
                            bullets.RemoveAt(i);
                            bulletsContainers.RemoveAt(i);
                            deleted = true;
                        }
                    }
                }
                if (!deleted)
                {
                    bullets[i].move();
                    RectangleF recBullet = bulletsContainers[i];
                    recBullet.Location = bullets[i].getLocation();
                    bulletsContainers[i] = recBullet;
                }
            }
        }

        public void gameOver()
        {
            popUp = new PopUpHandler("Paused", new string[] { "Resume", "Shop", "Quit" }, false, pnlMain, timers, account);
            backgroundUI = popUp.getPopUp();
            popUp.getPopUp();
            waveHndlr.pause();
            drawTimer.Stop();
        }

        public void pauseGame(bool pause)
        {
            lastPaused = DateTime.Now;
            if (pause)
            {
                popUp = new PopUpHandler("Paused", new string[]{"Resume","Menu", "Quit"}, true, pnlMain, timers, account);
                backgroundUI = popUp.getPopUp();
                popUp.getPopUp();
                waveHndlr.pause();
                drawTimer.Stop();
            }
            else
            {
                backgroundUI.Dispose();
                waveHndlr.play();
                drawTimer.Start();
            }
        }

        public void checkZombies()
        {
            for (int i = 0; i < zombiesContainers.Count; i++)
            {
                bool deleted = false;
                for (int j = 0; j < bulletsContainers.Count; j++)
                {
                    try
                    {
                        if (zombiesContainers[i].IntersectsWith(bulletsContainers[j]))
                        {
                            bullets.RemoveAt(j);
                            bulletsContainers.RemoveAt(j);
                            deleted = zombies[i].damage(20);
                            if (deleted)
                            {
                            
                                uiHandler.addCoins(zombies[i].getMaxHp()/rn.Next(4,5));
                                zombies.RemoveAt(i);
                                zombiesContainers.RemoveAt(i);
                                waveHndlr.zombieKilled();
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!deleted)
                {
                    bool[] zombieCollisions = { false, false, false, false };
                    if (zombiesContainers[i].IntersectsWith(playerContainer))
                    {
                        if (zombies[i].attack())
                        {
                            if (player.damage(zombies[i].getDamage()))
                            {
                                gameOver();
                            }
                        }
                        bool[] collision = util.checkCollision(zombiesContainers[i], playerContainer);
                        for (int c = 0; c < collision.Length; c++)
                        {
                            if (collision[c])
                            {
                                zombieCollisions[c] = true;
                            }
                        }
                    }
                    for (int j = 0; j < zombies.Count; j++)
                    {
                        if (i != j)
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
                    for (int b = 0; b < boxesContainers.Count; b++)
                    {
                        if (zombiesContainers[i].IntersectsWith(boxesContainers[b]))
                        {
                            bool[] collision = util.checkCollision(zombiesContainers[i], boxesContainers[b]);
                            for (int c = 0; c < collision.Length; c++)
                            {
                                if (collision[c])
                                {
                                    zombieCollisions[c] = true;
                                }
                            }
                        }
                    }
                    zombies[i].setDestenation(player.getPosition());
                    zombies[i].move(zombieCollisions);

                    RectangleF recZombie = zombiesContainers[i];
                    recZombie.Location = zombies[i].getLocation();
                    zombiesContainers[i] = recZombie;
                }
            }
        }

        public void draw(Graphics gData)
        {
            g = gData;
            if ((DateTime.Now - lastFrame).Milliseconds != 0)
            {
                Console.WriteLine(DateTime.Now - lastFrame);
                g.DrawString($"{1000 / (DateTime.Now - lastFrame).Milliseconds} FPS", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), Brushes.White, 5, 5);
                lastFrame = DateTime.Now;
            }

            g.DrawString(uiHandler.getCoins().ToString(), new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold), Brushes.White, pnlMain.Width-150, 10);
            for (int i = 0; i < turrets.Count; i++)
            {
                Bitmap turretSkin = (Bitmap)turrets[i].getSkin();
                turretSkin = util.rotateImg(turretSkin, turrets[i].getAngle());
                g.DrawImage(turretSkin, turrets[i].getCenterPosition());
            }
            for (int i = 0; i < zombies.Count; i++)
            {
                Bitmap zombieSkin = (Bitmap)zombies[i].getSkin();
                zombieSkin = util.rotateImg(zombieSkin, zombies[i].getAngle());

                Bitmap zombieHpBar = util.createHealthBar(zombies[i].getMaxHp(), zombies[i].getHp(), 100, 10);
                g.DrawImage(zombieHpBar, (zombies[i].getCenterPosition().X + zombieSkin.Width / 2) - zombieHpBar.Width / 2, zombies[i].getCenterPosition().Y - 30);
                g.DrawImage(zombieSkin, zombies[i].getCenterPosition());
                
                
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

            Bitmap healthBar = util.createHealthBar(player.getMaxHealt(), player.getHealt(), 100, 10);
            g.DrawImage(healthBar, (player.getCenterPosition().X+ playerSkin.Width/2)- healthBar.Width/2, player.getPosition().Y-30);
            g.DrawImage(playerSkin, player.getCenterPosition());


            if (lastWave >= DateTime.Now.AddSeconds(-3))
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                g.DrawString($"Wave {waveHndlr.getWave()} incomming!", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Bold), Brushes.White,450,400);
            }

        }

        public Point getRandomSpawn()
        {
            int spawnNumber;
            Point spawn = new Point();
            while (true)
            {
                spawnNumber = rn.Next(0, spawns.Count);
                spawn = spawns[spawnNumber];
                int playerX = player.getPosition().X;
                int playerY = player.getPosition().Y;
                if (!Enumerable.Range(playerX - 500, playerX + 500).Contains(spawn.X) && !Enumerable.Range(playerY - 400, playerY + 400).Contains(spawn.Y))
                {
                    return spawn;
                }
            }
        }

        public void newWave()
        {
            lastWave = DateTime.Now;
        }

        public void spawnZombie(int speed, int health, int skin = 0,int damage=10) 
        {
            Point spawn = getRandomSpawn();
            zombieHndlr = new ZombieHandler(spawn.X, spawn.Y, speed, skin, health, player.getCenterPosition(), damage);
            zombies.Add(zombieHndlr);

            SizeF zombieSize = zombieHndlr.getSkin().Size;
            zombiesContainers.Add(new RectangleF(zombieHndlr.getLocation().X, zombieHndlr.getLocation().Y, zombieSize.Width, zombieSize.Height));
        }
    }
}
