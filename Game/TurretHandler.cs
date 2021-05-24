using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class TurretHandler
    {
        private int x;
        private int y;
        private int shootingDelay;
        private DateTime lastShot;
        private Point target;
        private double angle;
        private Image skin;
        private int type;

        public TurretHandler(Point loc,int typeData)
        {
            type = typeData;
            x = loc.X;
            y = loc.Y;
            shootingDelay = type == 0? 2000 : 1700;
            skin = Image.FromFile($"Turrets/turret{type+1}.png");
            lastShot = DateTime.Now;
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
            return skin;
        }

        public int getType()
        {
            return type;
        }

        public Point getCenterPosition()
        {
            Image skin = getSkin();
            return new Point(x - (skin.Width / 2), y - (skin.Width / 2));
        }

        public Point getLocation()
        {
            return new Point(x, y);
        }

        public void setTarget(Point targetPoint)
        {
            target = targetPoint;


            int xDiff = target.X - x;
            int yDiff = target.Y - y;

            angle = Math.Atan2(xDiff, yDiff) * (-180 / Math.PI);
            angle += 180;
        }

        public Point getTarget()
        {
            return target;
        }

        public int getDelay()
        {
            return shootingDelay;
        }

        public double getAngle()
        {
            return angle;
        }
    }
}
