using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class ResourceHandler
    {
        public static void checkResources()
        {
            DataTable tbl = DataHandler.getItems();
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                byte[] imageBytes = (byte[])tbl.Rows[i][6];
                MemoryStream buf = new MemoryStream(imageBytes);
                Image img = Image.FromStream(buf, true);
                img.Save($"{Environment.CurrentDirectory}\\{tbl.Rows[i][2].ToString().Replace("/","\\")}");
            }
        }
    }
}
