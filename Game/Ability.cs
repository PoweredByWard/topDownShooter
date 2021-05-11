using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Ability
    {
        private int price;
        Image itemButton;
        public Ability(string name,int priceData)
        {
            itemButton = Image.FromFile($"GUI/items/{name}");
            price = priceData;
        }

        public int getPrice()
        {
            return price;
        }

        public Image getAbility()
        {
            return itemButton;
        }
    }
}
