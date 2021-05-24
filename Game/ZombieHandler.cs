using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class ZombieHandler
    {
        public string skinsDIR = "Zombies/";
        public string[] skins = new string[] { "zombie1.png", "zombie2.png" };
        private int attackDelay = 1000;
        private DateTime lastAttack = DateTime.Now;
        private int skin;
        private Image skinImg;
        private int speed;
        private int attackDamage;
        private double x;
        private double y;
        private double angle;
        private int health;
        private int maxhealth;
        private Point destenation;
        public ZombieHandler(int xData,int yData,int speedData,int skinData,int healthData,Point destenationData, int damage=10)
        {
            attackDamage = damage;
            x = xData;
            y = yData;
            speed = speedData;
            skin = skinData;
            skinImg = Image.FromFile($"{skinsDIR}{skins[skin]}");
            maxhealth = healthData;
            health = maxhealth;
            destenation = destenationData;
        }

        public void setDestenation(Point destenationData)
        {
            destenation = destenationData;
        }

        public int getDamage()
        {
            return attackDamage;
        }

        public bool attack()
        {
             if(lastAttack.AddMilliseconds(attackDelay) <= DateTime.Now)
             {
                lastAttack = DateTime.Now;
                return true;
             }
            return false;
        }

        public bool damage(int damage)
        {
            health -= damage;
            return health <= 0;
        }

        public int getHp()
        {
            return health;
        }

        public int getMaxHp()
        {
            return maxhealth;
        }

        public double getAngle()
        {
            return angle;
        }

        public Image getSkin()
        {
            return skinImg;
        }

        public Point getCenterPosition()
        {
            Image skin = getSkin();
            return new Point((int)x - (skin.Width / 2), (int)y - (skin.Width / 2));
        }

        public Point getLocation()
        {
            return new Point((int)x, (int)y);
        }

        public void move(double deltaTime, bool[] collisions = null)
        {
            double xDiff = destenation.X - x;
            double yDiff = destenation.Y - y;
            angle = Math.Atan2(xDiff, yDiff) * (-180 / Math.PI);
            angle += 90;
            if (collisions != null)
            {

                double newX = (x + speed * deltaTime * Math.Cos(angle * (Math.PI / 180.0)));
                double newY = (y + speed * deltaTime * Math.Sin(angle * (Math.PI / 180.0)));

                if (newX > x && collisions[0] || newX < x && collisions[1]) newX = x;
                if (newY > y && collisions[2] || newY < y && collisions[3]) newY = y;

                x = newX;
                y = newY;
                
            }
        }
    }
}
