using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class PlayerHandler
    {
        private int[] weaponShootingDelay = new int[] { 500, 400, 250 };
        private int[] weaponRecoil = new int[] { 6, 8, 15 };
        private int maxhealth = 100;
        private int health;
        private DateTime lastShot;
        private int speed;
        private double x;
        private double y;
        private double angle;
        private Image skinImg;
        
        public PlayerHandler(int xData, int yData)
        {
            health = maxhealth;
            skinImg = Image.FromFile(DataHandler.getPlayerSkin());
            speed = 4;
            x = xData;
            y = yData;
            lastShot = DateTime.Now;
        }


        public int getShootDelay(int weapon)
        {
            return weaponShootingDelay[weapon];
        }

        public bool damage(int damage)
        {
            health -= damage;
            return health<=0;
        }

        public int getHealt()
        {
            return health;
        }
        public int getMaxHealt()
        {
            return maxhealth;
        }

        public int getWeaponRecoil(int weapon)
        {
            return weaponRecoil[weapon];
        }
        public DateTime getLastShot()
        {
            return lastShot;
        }
        public void setLastShot()
        {
            lastShot = DateTime.Now;
        }
        public Image getSkin()
        {
            return skinImg;
        }
        public void setHealth(int healthData)
        {
            health = healthData;
        }

        public Point getCenterPosition()
        {
            Image skin = getSkin();
            return new Point((int)x - (skin.Width / 2), (int)y - (skin.Width / 2));
        }
        public Point getPosition()
        {
            return new Point((int)x, (int)y);
        }
        public double getAngle()
        {
            return angle;
        }

        public void move(int moveX,int moveY,Point mouseLocationData, double deltaTime , bool[] collisions = null)
        {
            if (collisions != null)
            {
                if (moveX + x > x && collisions[0] || moveX + x < x && collisions[1]) moveX = 0;
                if (moveY + y > y && collisions[2] || moveY + y < y && collisions[3]) moveY = 0;
                x += moveX * speed * deltaTime;
                y += moveY * speed * deltaTime;
                double xDiff = mouseLocationData.X - x;
                double yDiff = mouseLocationData.Y - y;
                angle = Math.Atan2(xDiff, yDiff) * (-180 / Math.PI);
                angle += 90;
            }
            else
            {
                x += moveX * speed * deltaTime;
                y += moveY * speed * deltaTime;
                double xDiff = mouseLocationData.X - x;
                double yDiff = mouseLocationData.Y - y;
                angle = Math.Atan2(xDiff, yDiff) * (-180 / Math.PI);
                angle += 90;
            }
        }
    }
}
