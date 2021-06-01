using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GameHandler
    {
        //start teken attributen
        Graphics g;
        Timer drawTimer;
        Panel pnlMain;
        //einde teken attributen

        //start timers
        List<Timer> timers = new List<Timer>();
        //einde timers

        //start entiteiten
        List<BulletHandler> bullets = new List<BulletHandler>();
        List<ZombieHandler> zombies = new List<ZombieHandler>();
        List<TurretHandler> turrets = new List<TurretHandler>();
        //einde entiteiten

        //start inputs
        List<int> inputs = new List<int>();
        int[] gameControls;
        //einde inputs

        //start containers
        List<RectangleF> zombiesContainers = new List<RectangleF>();
        List<RectangleF> bulletsContainers = new List<RectangleF>();
        List<RectangleF> boxesContainers = new List<RectangleF>();
        List<RectangleF> uiContainers = new List<RectangleF>();
        List<RectangleF> turretRange = new List<RectangleF>();
        RectangleF mouseContainer;
        RectangleF playerContainer;
        //einde containers

        //start spawns
        List<Point> spawns = new List<Point>();
        //einde spawns

        //start handlers
        PlayerHandler player;
        InventoryHandler inventory;
        BulletHandler bulletHndlr;
        ZombieHandler zombieHndlr;
        PopUpHandler popUp;
        WaveHandler waveHndlr;
        UIHandler uiHandler;
        //einde handlers

        //start locaties
        Point mouseLocation;
        Point playerMove;
        //einde locaties

        //start tijden
        DateTime lastPaused;
        DateTime lastWave;
        DateTime lastFrame;
        //einde tijden

        //start delta
        double deltaTime;
        double timedif;
        const int gameSpeed = 20;
        //einde delta

        //start main controls
        Panel backgroundUI;
        Form gameForm;
        //einde main controls

        //start cursors
        Cursor cursor1;
        Cursor cursor2;
        //einde cursors

        //start stats
        private int kills;
        private int turretsPlaced;
        private int damageDealt;
        private DateTime gameStart;
        private int score;
        //einde stats

        //start andere variablen
        Random rn = new Random();
        bool paused;
        //einde andere variablen

        public GameHandler(Panel pnl, Timer drawTimerData,Form gameFormData)
        {
            //start UI elements
            inventory = new InventoryHandler(player.getPosition());
            uiHandler = new UIHandler(player,this);
            //einde UI elements

            //start init variabelen
            pnlMain = pnl;
            drawTimer = drawTimerData;
            gameForm = gameFormData;
            deltaTime = 0;
            timedif = 0;
            score = 0;
            //einde init variabelen

            //start waves
            waveHndlr = new WaveHandler(this);
            timers.Add(waveHndlr.getTimer());
            //einde waves

            //start delta
            timers.Add(drawTimerData);
            //einde delta

            //start tijden
            lastPaused = DateTime.Now.AddMilliseconds(-250);
            lastWave = DateTime.Now;
            //einde tijden

            //start map
            createMap();
            //einde map

            //start player
            Point playerSpawn = getRandomSpawn(false);
            player = new PlayerHandler(playerSpawn.X, playerSpawn.Y);
            SizeF playerSize = player.getSkin().Size;
            playerContainer = new RectangleF(playerSpawn.X, playerSpawn.Y, playerSize.Width, playerSize.Height);
            //einde player

            //start inputs
            DataTable keys = DataHandler.getUserControls();
            gameControls = new int[keys.Rows.Count];
            for (int i = 0; i < keys.Rows.Count; i++)
            {
                gameControls[i] = int.Parse(keys.Rows[i][0].ToString());
                Console.WriteLine(gameControls[i]);
            }
            //einde inputs

            //start cursors
            Bitmap cursor1Btm = new Bitmap("GUI/crosshair.png");
            Bitmap cursor2Btm = new Bitmap("GUI/crosshair2.png");
            cursor1 = new Cursor(cursor1Btm.GetHicon());
            cursor2 = new Cursor(cursor2Btm.GetHicon());
            //einde cursors

            //start stats
            kills = 0;
            turretsPlaced = 0;
            damageDealt = 0;
            gameStart = DateTime.Now;
            //einde stats
        }

        //start inputs
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

        public void setMouseLocation(Point mouseData)
        {
            mouseLocation = mouseData;
        }

        public void addInput(KeyEventArgs e)
        {
            if (!inputs.Contains((int)e.KeyCode)) inputs.Add((int)e.KeyCode);
        }

        public void removeInput(KeyEventArgs e)
        {
            inputs.Remove((int)e.KeyCode);
        }

        public void checkInputs()
        {


            int moveX = 0;
            int moveY = 0;
            Console.WriteLine();
            foreach (int key in inputs)
            {
                if (key == gameControls[0])
                {
                    moveY--;
                }
                else if (key == gameControls[1])
                {
                    moveX--;
                }
                else if (key == gameControls[2])
                {
                    moveX++;
                }
                else if (key == gameControls[3])
                {
                    moveY++;
                }
                else if (key == gameControls[4])
                {
                    SoundHandler.togleMute();
                }
                else if (key == (int)Keys.Escape)
                {
                    if (lastPaused < DateTime.Now.AddMilliseconds(-400))
                    {
                        if (pnlMain.Controls.Count > 0)
                        {
                            pauseGame(false);
                        }
                        else
                        {
                            pauseGame(true);
                        }
                    }
                }
            }
            playerMove = new Point(moveX, moveY);
        }

        //einde inputs

        //start score settings
        public void addScore(int amount)
        {
            score += amount;
        }

        public int getScore()
        {
            return score;
        }

        //einde score settings

        //start map
        public Point getRandomSpawn(bool notNearPlayer = true)
        {
            int spawnNumber;
            Point spawn = new Point();
            while (true)
            {
                spawnNumber = rn.Next(0, spawns.Count);
                spawn = spawns[spawnNumber];
                if (notNearPlayer)
                {
                    int playerX = player.getPosition().X;
                    int playerY = player.getPosition().Y;
                    if (!Enumerable.Range(playerX - 400, playerX + 400).Contains(spawn.X) && !Enumerable.Range(playerY - 300, playerY + 300).Contains(spawn.Y))
                    {
                        return spawn;
                    }
                }
                else
                {
                    return spawn;
                }
            }
        }

        public void createMap()
        {
            int xRoad = rn.Next(1, 29);
            int yRoad = rn.Next(1, 19);
            int[,] mapPanels = new int[30, 20];
            Bitmap res = new Bitmap(pnlMain.BackgroundImage, pnlMain.Width, pnlMain.Height);
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
                            if (isbox && !boxNear && !roadNear)
                            {
                                background.DrawImage(Image.FromFile("Textures/10.png"), i * 50, j * 50);
                                mapPanels[i, j] = 10;
                                boxesContainers.Add(new RectangleF(i * 50 + 5, j * 50 + 15, Image.FromFile("Textures/10.png").Width - 5, Image.FromFile("Textures/10.png").Height - 5));
                            }
                            else
                            {
                                background.DrawImage(Image.FromFile("Textures/0.png"), i * 50, j * 50);
                                mapPanels[i, j] = 0;
                                spawns.Add(new Point(i * 50, j * 50));
                            }
                        }
                    }
                }
            }
            pnlMain.BackgroundImage = uiHandler.drawUI(res);

        }

        //einde map

        //start turrets
        public void addTurret(int type)
        {
            TurretHandler newTurret = new TurretHandler(player.getPosition(),type);
            turretsPlaced++;
            turrets.Add(newTurret);
            turretRange.Add(new RectangleF(newTurret.getLocation().X - 300, newTurret.getLocation().Y - 300, 600 + newTurret.getSkin().Width, 600 + newTurret.getSkin().Height));
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
                    double recoil = rn.Next(-17, 17);
                    bulletHndlr = new BulletHandler(turretPos.X, turretPos.Y, turrets[i].getAngle() - 90 + recoil, turrets[i].getTarget(), turrets[i].getType());
                    bullets.Add(bulletHndlr);
                    SoundHandler.playTurret();

                    SizeF bulletSize = bulletHndlr.getBullet().Size;
                    bulletSize.Width = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;
                    bulletSize.Height = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;

                    bulletsContainers.Add(new RectangleF(bulletHndlr.getLocation(), bulletSize));

                    turrets[i].setLastShot();
                }

            }
        }

        //einde turrets

        //start player
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
                    bool[] collision = Utils.checkCollision(playerContainer, boxesContainers[i]);
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
                    bool[] collision = Utils.checkCollision(playerContainer, zombiesContainers[i]);
                    for (int c = 0; c < collision.Length; c++)
                    {
                        if (collision[c])
                        {
                            collisions[c] = true;
                        }
                    }
                }
            }

            player.move(playerMove.X, playerMove.Y, mouseLocation, deltaTime, collisions);

            playerContainer.Location = player.getPosition();
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

            if (player.getLastShot() < DateTime.Now.AddMilliseconds(-player.getShootDelay(inventory.getActiveWeaponPower())))
            {
                Point playerCenterPosition = player.getPosition();
                int weaponPower = inventory.getActiveWeaponPower();
                double recoil = rn.Next(-(player.getWeaponRecoil(weaponPower) / 2), (player.getWeaponRecoil(weaponPower) / 2));
                bulletHndlr = new BulletHandler(playerCenterPosition.X, playerCenterPosition.Y, player.getAngle() + recoil, e.Location, weaponPower);
                bullets.Add(bulletHndlr);

                SizeF bulletSize = bulletHndlr.getBullet().Size;
                bulletSize.Width = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;
                bulletSize.Height = bulletSize.Width > bulletSize.Height ? bulletSize.Width : bulletSize.Height;

                bulletsContainers.Add(new RectangleF(bulletHndlr.getLocation(), bulletSize));
                SoundHandler.playGun();
                player.setLastShot();
            }
        }

        //einde player

        //start UI
        private void checkUI()
        {
            uiContainers = uiHandler.getRectangles();
        }

        //einde UI

      
        //start bullets
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
                    bullets[i].move(deltaTime);
                    RectangleF recBullet = bulletsContainers[i];
                    recBullet.Location = bullets[i].getLocation();
                    bulletsContainers[i] = recBullet;
                }
            }
        }

        //einde bullets

        //start zombies
        public void spawnZombie(int speed, int health, int skin = 0, int damage = 10)
        {
            Point spawn = getRandomSpawn();
            zombieHndlr = new ZombieHandler(spawn.X, spawn.Y, speed, skin, health, player.getCenterPosition(), damage);
            zombies.Add(zombieHndlr);

            SizeF zombieSize = zombieHndlr.getSkin().Size;
            zombiesContainers.Add(new RectangleF(zombieHndlr.getLocation().X, zombieHndlr.getLocation().Y, zombieSize.Width, zombieSize.Height));
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
                            int healtBefore = zombies[i].getHp();
                            bullets.RemoveAt(j);
                            bulletsContainers.RemoveAt(j);
                            deleted = zombies[i].damage(20);
                            int healtAfter = zombies[i].getHp();
                            damageDealt += healtBefore - healtAfter;


                            if (deleted)
                            {
                                
                                uiHandler.addCoins(zombies[i].getMaxHp()/rn.Next(4,5));
                                zombies.RemoveAt(i);
                                zombiesContainers.RemoveAt(i);
                                waveHndlr.zombieKilled();
                                kills++;
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
                        bool[] collision = Utils.checkCollision(zombiesContainers[i], playerContainer);
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
                                bool[] collision = Utils.checkCollision(zombiesContainers[i], zombiesContainers[j]);
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
                            bool[] collision = Utils.checkCollision(zombiesContainers[i], boxesContainers[b]);
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
                    zombies[i].move(deltaTime,zombieCollisions);

                    RectangleF recZombie = zombiesContainers[i];
                    recZombie.Location = zombies[i].getLocation();
                    zombiesContainers[i] = recZombie;
                }
            }
        }

        //einde zombies

        //start gamestatus
        public void gameOver()
        {
            waveHndlr.pause();
            drawTimer.Stop();
            EndGameForm statsFrm = new EndGameForm(waveHndlr.getWave(), turretsPlaced, damageDealt, kills, DateTime.Now - gameStart);
            statsFrm.Show();
            gameForm.Hide();
        }

        public void pauseGame(bool pause)
        {
            lastPaused = DateTime.Now;
            if (pause)
            {
                popUp = new PopUpHandler("Paused", new string[] { "Resume", "Menu", "Quit" }, true, pnlMain, timers);
                backgroundUI = popUp.getPopUp();
                popUp.getPopUp();
                waveHndlr.pause();
                drawTimer.Stop();
            }
            else
            {
                try
                {
                    backgroundUI.Dispose();
                }
                catch
                {

                }
                waveHndlr.play();
                drawTimer.Start();
            }
        }

        //einde gamestatus

        //start draw-move
        public void move()
        {
            timedif = (DateTime.Now - lastFrame).Milliseconds;
            deltaTime = (double)(DateTime.Now - lastFrame).Milliseconds / (double)gameSpeed;
            lastFrame = DateTime.Now;
            Console.WriteLine(deltaTime);
            if (deltaTime != 0)
            {
                checkMouse();
                checkUI();
                checkBullets();
                checkPlayer();
                checkTurrets();
                checkZombies();
            }
        }

        public void draw(Graphics gData)
        {
            g = gData;
            Console.WriteLine(timedif);
            if (timedif!=0)
            {
                g.DrawString($"{(int)(1000  / timedif)} FPS", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), Brushes.White, 5, 5);
            }

            g.DrawString(uiHandler.getCoins().ToString(), new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold), Brushes.White, pnlMain.Width-150, 10);
            for (int i = 0; i < turrets.Count; i++)
            {
                Bitmap turretSkin = (Bitmap)turrets[i].getSkin();
                turretSkin = Utils.rotateImg(turretSkin, turrets[i].getAngle());
                g.DrawImage(turretSkin, turrets[i].getCenterPosition());
            }
            for (int i = 0; i < zombies.Count; i++)
            {
                Bitmap zombieSkin = (Bitmap)zombies[i].getSkin();
                zombieSkin = Utils.rotateImg(zombieSkin, zombies[i].getAngle());

                Bitmap zombieHpBar = Utils.createHealthBar(zombies[i].getMaxHp(), zombies[i].getHp(), 100, 10);
                g.DrawImage(zombieHpBar, (zombies[i].getCenterPosition().X + zombieSkin.Width / 2) - zombieHpBar.Width / 2, zombies[i].getCenterPosition().Y - 30);
                g.DrawImage(zombieSkin, zombies[i].getCenterPosition());
                
                
            }
            foreach (BulletHandler bullet in bullets)
            {
                Bitmap bulletSkin = (Bitmap)bullet.getBullet();
                bulletSkin = Utils.rotateImg(bulletSkin, bullet.getAngle());
                g.DrawImage(bulletSkin, bullet.getLocation());
            }

            Bitmap weaponSkin = (Bitmap)inventory.getActiveWeapon();
            inventory.setAngle(player.getAngle());
            inventory.setLocation(player.getPosition());
            weaponSkin = Utils.rotateImg(weaponSkin, player.getAngle());
            g.DrawImage(weaponSkin, inventory.getLocation());

            Bitmap playerSkin = (Bitmap)player.getSkin();
            playerSkin = Utils.rotateImg(playerSkin, player.getAngle());

            Bitmap healthBar = Utils.createHealthBar(player.getMaxHealt(), player.getHealt(), 100, 10);
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
        //einde draw-move

        //start waves
        public void newWave()
        {
            lastWave = DateTime.Now;
        }

        //einde waves
    }
}
