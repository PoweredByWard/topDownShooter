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
        public string weaponsDIR = "Weapons/";
        public string[] weapons = new string[] { "pistol.png", "pistolsilence.png", "smg.png" };
        private List<string> inventory = new List<string>();
        private int activeWeapon;
        private double x;
        private double y;
        private double angle;
        private int distanceFromCenter = 15;
        public InventoryHandler(Point loc)
        {
            activeWeapon = 0;
            inventory.Add(weapons[activeWeapon]);
            inventory.Add(weapons[1]);
            x = loc.X;
            y = loc.Y;
        }

        
        public void addToInventory(int weapon)
        {
            inventory.Add(weapons[weapon]);
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
            return Image.FromFile($"{weaponsDIR}{inventory[activeWeapon]}");
        }

        public void setActiveWeapon(int weapon)
        {
            activeWeapon = weapon;
        }
        public int getActiveWeaponNumber()
        {
            return activeWeapon;
        }
    }
}
