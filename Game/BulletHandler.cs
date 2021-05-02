using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class BulletHandler
    {
        public string skinssDIR = "Bullets/";
        public string[] bullets = new string[] { "pistol.png", "pistolsilence.png", "smg.png" };
        private string bulletSkin;
        private int speed = 15;
        private double x;
        private double y;
        private double angle;

        public BulletHandler(int xData,int yData, double angleData,Point mouseLocationData, int weapon)
        {
            x = (int)(xData + 20 * Math.Cos(angleData * (Math.PI / 180.0)));
            y = (int)(yData + 20 * Math.Sin(angleData * (Math.PI / 180.0)));
            angle = angleData;
            bulletSkin = $"{skinssDIR}{bullets[weapon]}";
        }

        public Point getLocation()
        {
            return new Point((int)x, (int)y);
        }
        public double getAngle()
        {
            return angle;
        }

        public Image getBullet()
        {
            return Image.FromFile(bulletSkin);
        }

        public void move()
        {
           x = x+ speed * Math.Cos(angle * (Math.PI / 180.0));
           y = y+ speed * Math.Sin(angle * (Math.PI / 180.0));
        }
    }
}
