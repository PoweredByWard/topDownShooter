using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class InventoryHandler
    {
        private double x;
        private double y;
        private double angle;
        private int distanceFromCenter = 15;
        private Image weapon = Image.FromFile(DataHandler.getGunSkin());
        private int weaponPower = DataHandler.getGunPower();
        public InventoryHandler(Point loc)
        {
            x = loc.X;
            y = loc.Y;
        }

        public void setAngle(double angleData)
        {
            angle = angleData;
        }

        public Point getLocation()
        {
            return new Point((int)(x + distanceFromCenter * Math.Cos(angle * (Math.PI / 180.0))), (int)(y + distanceFromCenter * Math.Sin(angle * (Math.PI / 180.0))));
        }

        public void setLocation(Point loc)
        {
            x = loc.X;
            y = loc.Y;
        }

        public Image getActiveWeapon()
        {
            return weapon;
        }

        public int getActiveWeaponPower()
        {
            return weaponPower;
        }
    }
}
