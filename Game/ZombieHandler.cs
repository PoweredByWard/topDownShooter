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
        private int speed;
        private int attackDamage = 10;
        private int x;
        private int y;
        private double angle;
        private int health;
        private int maxhealth;
        private Point destenation;
        public ZombieHandler(int xData,int yData,int speedData,int skinData,int healthData,Point destenationData)
        {
            x = xData;
            y = yData;
            speed = speedData;
            skin = skinData;
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

        public int getHpPercent()
        {
            return health;
        }

        public double getAngle()
        {
            return angle;
        }

        public Image getSkin()
        {
            return Image.FromFile($"{skinsDIR}{skins[skin]}");
        }

        public Point getLocation()
        {
            return new Point(x, y);
        }

        public void move(bool[] collisions = null)
        {
            int xDiff = destenation.X - x;
            int yDiff = destenation.Y - y;
            angle = Math.Atan2(xDiff, yDiff) * (-180 / Math.PI);
            angle += 90;
            if (collisions != null)
            {
                int newX = (int)(x + speed * Math.Cos(angle * (Math.PI / 180.0)));
                int newY = (int)(y + speed * Math.Sin(angle * (Math.PI / 180.0)));

                if (newX > x && collisions[0] || newX < x && collisions[1]) newX = x;
                if (newY > x && collisions[2] || newY < x && collisions[3]) newY = y;

                x = newX;
                y = newY;
                
            }
        }
    }
}
