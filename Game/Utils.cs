using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Utils
    {
        public Utils()
        {
        }
        public Bitmap rotateImg(Bitmap btm, double angleData)
        {
            double rotationWidth = Math.Sqrt(btm.Width * btm.Width + btm.Height * btm.Height);
            double height = 0.5 * rotationWidth - 0.5 * rotationWidth * Math.Cos((btm.Height / (2 * Math.PI * rotationWidth)) * ((Math.PI / 180) * 360));
            double width = 0.5 * rotationWidth - 0.5 * rotationWidth * Math.Cos((btm.Width / (2 * Math.PI * rotationWidth)) * ((Math.PI / 180) * 360));
            Bitmap res = new Bitmap((int)rotationWidth, (int)rotationWidth);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.TranslateTransform((float)res.Width / 2, (float)res.Height / 2);
                g.RotateTransform((float)angleData);
                g.TranslateTransform(-(float)res.Width / 2, -(float)res.Height / 2);
                g.DrawImage(btm, new Point((int)height, (int)width));
            }
            return res;
        }

        public Bitmap createHealthBar(int max, float current, int width, int height)
        {
            float difference = (float)width / (float)max;
            int health = Convert.ToInt32(current * difference);
            
            Bitmap res = new Bitmap(Image.FromFile("GUI/hp_bar.png"), width, height);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.FillRectangle(Brushes.Green, 1, 1, health - 2, height - 2);
                g.FillRectangle(Brushes.Red, 1 + (width - (width - health)), 1, width-(health - 2), height - 2);
            }
            return res;
        }

        public bool[] checkCollision(RectangleF item1, RectangleF item2)
        {
            
            bool[] collisions = new bool[4];
            collisions[0] = item1.Right >= item2.Left && item1.Right < item2.Right? true:false;
            collisions[1] = item1.Left <= item2.Right && item1.Left > item2.Left ? true:false;
            collisions[2] = item1.Bottom >= item2.Top && item1.Bottom < item2.Bottom ? true:false;
            collisions[3] = item1.Top <= item2.Bottom && item1.Top > item2.Top ? true:false;
            return collisions;
        }
    }
}
