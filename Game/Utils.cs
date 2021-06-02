using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    static class Utils
    {
        static public void quit()
        {
            System.Windows.Forms.Application.Exit();
        }
        static public Bitmap rotateImg(Bitmap btm, double angleData)
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

        static public Bitmap createHealthBar(int max, float current, int width, int height)
        {
            float difference = (float)width / (float)max;
            int health = Convert.ToInt32(current * difference);
            
            Bitmap res = new Bitmap(Image.FromFile("GUI/hp_bar.png"), width, height);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.FillRectangle(Brushes.Green, 1, 1, health - 2, height - 2);
                g.FillRectangle(Brushes.Red, 1 + (width - (width - health)), 1, width-(health - 2)-2, height - 2);
            }
            return res;
        }

        static public bool[] checkCollision(RectangleF item1, RectangleF item2)
        {
            bool[] collisions = new bool[4];

            Console.WriteLine(item1.Bottom <= item2.Top && item1.Top <= item2.Bottom);

            collisions[0] = item1.Right >= item2.Left && item1.Right <= item2.Left + 4;
            collisions[1] = item1.Left <= item2.Right && item1.Left >= item2.Right - 4;

            collisions[2] = item1.Bottom >= item2.Top && item1.Bottom <= item2.Top + 4;
            collisions[3] = item1.Top <= item2.Bottom && item1.Top >= item2.Bottom - 4;

            if (collisions[0]&& collisions[2])collisions[2] = false;
            if (collisions[1] && collisions[3]) collisions[3] = false;
            if (collisions[0]&& collisions[3])collisions[3] = false;
            if (collisions[1] && collisions[2]) collisions[2] = false;


            return collisions;
        }

        static public Panel showPnl(string name,List<Panel>tabs)
        {
            foreach (Panel pnl in tabs)
            {
                if (pnl.Name != name)
                {
                    pnl.Hide();
                }
                else
                {
                    pnl.Show();
                    pnl.BringToFront();
                    return pnl;
                }
            }
            return null;
        }

        static public void validateTextInt(TextBox txt,string oldPrice, string defaultValue = "0")
        {
            string text = txt.Text;
            if (text == "")
            {
                txt.Text = defaultValue;
                return;
            }
            try
            {
                int number = int.Parse(text);
                text = number.ToString();
                if (number>=0)
                {
                    oldPrice = text;
                }
                else
                {
                    txt.Text = oldPrice;
                }
            }
            catch (Exception)
            {
                int focus = txt.SelectionStart;
                txt.Text = oldPrice;
                txt.SelectionStart = focus - 1;
                txt.SelectionLength = 0;
            }
        }

        static public int calculateScore(int kills,double minutes)
        {
            return (int)Math.Round((kills * 2 + (minutes * 5 > 50 ? 50 : minutes * 5)) * 3);
        }

        static public bool checkPassword(string password, string repeat)
        {
            if (password.Length <= 5)
            {
                MessageBox.Show("Your password has to be at least 6 karaketers.");
                return false;
            }
            else if (password != repeat)
            {
                MessageBox.Show("Please fill in the same password.");
                return false;
            }
            return true;
        }
    }
}
