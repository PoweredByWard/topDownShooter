using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class DataHandler
    {
        static MySqlConnection connection;
        static MySqlDataAdapter daGegevens;
        static DataTable data = new DataTable();

        static private string connectionString;
        static private bool datastatus;

        static DataHandler()
        {
            connectionString = "Server=83.217.67.11;Port=3306;SslMode=none;Database=06InfoWard;Uid=06InfoWard;Pwd=wardCostNi@6I";
            connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                datastatus = true;
                connection.Close();
            }
            catch
            {
                datastatus = false;
            }
        }

        public static bool isTaken(string username)
        {
            Console.WriteLine(datastatus);
            if (datastatus)
            {
                connection.Open();
                MySqlParameter pmblobnick = new MySqlParameter();
                pmblobnick.ParameterName = "@username";
                pmblobnick.Value = username;
                MySqlCommand sqlcm = new MySqlCommand("SELECT * FROM EX2_Accounts WHERE username = @username", connection);
                sqlcm.Parameters.Add(pmblobnick);
                daGegevens = new MySqlDataAdapter(sqlcm);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count==0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool loginUser(string username, string password)
        {
            if (datastatus)
            {
                string hash = gethash(password);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM EX2_Accounts WHERE username = @username AND hash = @hash", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@hash", hash);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count > 0) return true;
            }
            return false;
        }
        public static bool createUser(string username, string password)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM EX2_Accounts WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", username);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);

                if (data.Rows.Count > 0)
                {
                    connection.Close();
                    return false;
                }

                string hash = gethash(password);

                cmd = new MySqlCommand("INSERT INTO EX2_Accounts (username,password,hash) VALUES (@username,@password,@hash)", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.ExecuteNonQuery();
                connection.Close();

                createUserDefaultSettings(username);

                return true;

            }
            return false;
        }

        public static bool changePassword(string password)
        {
            if (datastatus)
            {
                string hash = gethash(password);

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE EX2_Accounts SET password = @password, hash = @hash WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
        }

        public static void createUserDefaultSettings(string username)
        {
            DataTable controls = getControls();

            for (int i = 0; i < controls.Rows.Count; i++)
            {
                createUserControl(controls.Rows[i][0].ToString(), username);
            }
        }

        public static void updateUser(string username = null)
        {
            if (username == null) username = AccountHandler.getUsername();
            MySqlCommand cmd = new MySqlCommand("UPDATE EX2_Accounts SET last_update = NOW() WHERE username = @username", connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static string gethash(string password)
        {
            List<string> targets = new List<string>();
            List<string> replace = new List<string>();
            Console.WriteLine(password);
            for (int i = 0; i < password.Length; i++)
            {

                string karakter = password.Substring(i, 1);
                Console.WriteLine(karakter);
                if (!targets.Contains(karakter))
                {
                    targets.Add(karakter);
                    replace.Add(password.Substring(password.Length - (i+1), 1));
                }
            }

            string dataHash = getRawHash(password);
            string hash = "";
            int lastNumber = 10%replace.Count==0?7: replace.Count;

            bool changedKarakter = false;
            for (int i = 0; i < dataHash.Length; i++)
            {
                string karakter = dataHash.Substring(i, 1);
                if (targets.Contains(karakter))
                {
                    karakter = replace[targets.IndexOf(karakter)];
                }
                else
                {
                    try
                    {
                        int number = int.Parse(karakter);
                        karakter = (lastNumber % number).ToString();
                        lastNumber = number;
                        changedKarakter = false;
                    }
                    catch (Exception)
                    {
                        if (replace.Count>=lastNumber+1 && !changedKarakter)
                        {
                            karakter = replace[lastNumber];
                            changedKarakter = true;
                        }
                    }
                }
                hash += karakter;
            }
            char[] chars = hash.ToCharArray();
            Array.Reverse(chars);
            hash = string.Join("",chars);
            return getRawHash(hash);
            
        }

        public static string getRawHash(string password)
        {
            using (SHA256 shaHash = SHA256.Create())
            {
                byte[] bytes = shaHash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static DataTable getTop(int amount, bool isPersonal)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd;
                if (isPersonal)
                {
                    cmd = new MySqlCommand("SELECT A.username,M.zombie_kills,M.wave,M.duration  FROM EX2_Matches M, EX2_Accounts A  WHERE M.player = A.user_id AND A.username = @username AND date(time) >= CURDATE() - interval @amount day ORDER BY M.zombie_kills DESC", connection);
                    cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());

                }
                else
                {
                    cmd = new MySqlCommand("SELECT A.username,M.zombie_kills,M.wave,M.duration  FROM EX2_Matches M, EX2_Accounts A  WHERE M.player = A.user_id ORDER BY M.zombie_kills DESC LIMIT @amount", connection);
                }
                cmd.Parameters.AddWithValue("@amount", amount);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static string getTypeItem(string item)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT type FROM EX2_Items WHERE item_id = @item", connection);
                cmd.Parameters.AddWithValue("@item", item);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count > 0) return data.Rows[0][0].ToString();
            }
            return "";
        }

        public static Size getTypeSize(string type)
        {
            data = new DataTable();
            Size size = new Size(0,0);
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT size FROM EX2_ItemTypes WHERE type_id = @type", connection);
                cmd.Parameters.AddWithValue("@type", type);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                string sizeString = data.Rows[0][0].ToString();
                size = new Size(int.Parse(sizeString.Split(',')[0]), int.Parse(sizeString.Split(',')[1]));
            }
            return size;
        }

        public static DataTable findUser(string searchValue)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT username  FROM EX2_Accounts WHERE username LIKE CONCAT('%', @search, '%') LIMIT 5", connection);
                cmd.Parameters.AddWithValue("@search", searchValue);
                Console.WriteLine(searchValue);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                Console.WriteLine(data.Rows.Count);
                
            }
            return data;
        }
        public static DataTable getProfile(string username)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(zombie_kills),SUM(turrets_placed),SUM(wave),SEC_TO_TIME(SUM(TIME_TO_SEC(duration))),SUM(damage_dealt)  FROM EX2_Matches  WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username)", connection);
                cmd.Parameters.AddWithValue("@username", username);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                Console.WriteLine(data.Rows[0][0]);
            }
            return data;
        }

        public static int getCoins()
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT coins  FROM EX2_Accounts  WHERE username =  @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                return int.Parse(data.Rows[0][0].ToString());
            }
            return 0;
        }

        public static void setCoins(int coins)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE EX2_Accounts SET coins = @coins WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@coins", coins);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static bool isAdmin(string username = null)
        {
            data = new DataTable();
            if (datastatus)
            {
                if (username == null) username = AccountHandler.getUsername();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT admin FROM EX2_Accounts  WHERE username =  @username AND admin = 1", connection);
                cmd.Parameters.AddWithValue("@username", username);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count > 0) return true;
            }
            return false;
        }

        public static bool deleteAccount(string username)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM EX2_Accounts WHERE username = @username AND admin = 0", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
        }

        public static bool deleteItem(string item)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM EX2_Items WHERE item_id = @item AND default = 0 ", connection);
                cmd.Parameters.AddWithValue("@item", item);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
        }

        public static bool resetAccount(string username)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM EX2_Matches WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username); UPDATE EX2_Accounts SET coins = 0 WHERE username = @username; DELETE FROM EX2_Inventory WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND item IN (SELECT item_id FROM EX2_Items WHERE price !=0)", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
                connection.Close();
                updateUser(username);
                return true;
            }
            return false;
        }

        public static void saveGame(int wave,int kills,int turretsPlaced,int damgeDealt,TimeSpan duration)
        {
            if (datastatus)
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO EX2_Matches (player,zombie_kills,turrets_placed,wave,duration,damage_dealt) VALUES ((SELECT user_id FROM EX2_Accounts WHERE username = @username),@kills,@turrets,@wave,@duration,@damage)", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@wave", wave);
                cmd.Parameters.AddWithValue("@kills", kills);
                cmd.Parameters.AddWithValue("@damage", damgeDealt);
                cmd.Parameters.AddWithValue("@turrets", turretsPlaced);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static DataTable getItemTypes()
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM EX2_ItemTypes", connection);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static DataTable getItems(string type = null)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                
                MySqlCommand cmd = new MySqlCommand(type==null? "SELECT *  FROM EX2_Items ORDER BY price" : "SELECT *  FROM EX2_Items WHERE type = @type ORDER BY price", connection);
                if (type!=null)cmd.Parameters.AddWithValue("@type",type);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static DataTable getItem(string item)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM EX2_Items WHERE item_id = @item", connection);
                cmd.Parameters.AddWithValue("@item", item);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static string getTypeIDByName(string name)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT type_id  FROM EX2_ItemTypes WHERE name = @itemname", connection);
                cmd.Parameters.AddWithValue("@itemname", name);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count > 0) return data.Rows[0][0].ToString();
            }
            return "";
        }

        public static DataTable getType(string type)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM EX2_ItemTypes WHERE type_id = @type", connection);
                cmd.Parameters.AddWithValue("@type", type);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static void saveItem(string name, string power, Color color, Image itemImg, string price, bool isDefault, string itemID,string typeID)
        {
            if (datastatus)
            {
                byte[] img;
                using (MemoryStream st = new MemoryStream())
                {
                    itemImg.Save(st, ImageFormat.Png);
                    img = st.ToArray();
                }


                MySqlCommand cmd;
                DataTable item;
                DataTable type;
                if (isDefault)
                {
                    item = getItem(itemID);
                    type = getType(item.Rows[0][3].ToString());
                    cmd = new MySqlCommand("UPDATE EX2_Items SET is_default = 0 WHERE type = @type", connection);
                    cmd.Parameters.AddWithValue("@type", type.Rows[0][0]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                if (itemID == null) 
                {
                    type = getType(typeID);
                    cmd = new MySqlCommand("INSERT INTO EX2_Items (name,source,type,price,color,image,power) VALUES (@name,@source,@type,@price,@color,@image,@power)", connection);
                    cmd.Parameters.AddWithValue("@type", type.Rows[0][0]);
                    ResourceHandler.checkResources();
                }
                else
                {
                    item = getItem(itemID);
                    type = getType(item.Rows[0][3].ToString());
                    cmd = new MySqlCommand("UPDATE EX2_Items SET name = @name, source = @source, price = @price, power = @power, color = @color, image = @image, is_default = @default WHERE item_id = @itemid", connection);
                    cmd.Parameters.AddWithValue("@itemid", itemID);
                }
                connection.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@source", $"{type.Rows[0][3]}/{name}.png");
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@power", power);
                cmd.Parameters.AddWithValue("@color", $"{color.R},{color.G},{color.B}");
                cmd.Parameters.AddWithValue("@image", img);
                cmd.Parameters.AddWithValue("@default", isDefault?"1":"0");
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
        }

        public static DataTable getPlayerItems()
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM EX2_Inventory WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username)", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static string getPlayerSkin()
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT It.source FROM EX2_Items It, EX2_Inventory Inv WHERE Inv.player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 2 AND Inv.item = It.item_id", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count == 0) return "Players/survivor.png";
                return data.Rows[0][0].ToString();
            }
            return "Players/survivor.png";

        }

        public static int getGunPower()
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT It.power FROM EX2_Items It, EX2_Inventory Inv WHERE Inv.player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 1 AND Inv.item = It.item_id", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count == 0) return 0;
                return int.Parse(data.Rows[0][0].ToString());
            }
            return 0;
        }

        public static string getGunSkin()
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT It.source FROM EX2_Items It, EX2_Inventory Inv WHERE Inv.player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 1 AND Inv.item = It.item_id", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
                if (data.Rows.Count==0) return "Weapons/pistol.png";
                return data.Rows[0][0].ToString();
            }
            return "Weapons/pistol.png";
        }
        public static void equipItem(string itemID)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE EX2_Inventory SET equipped = 0 WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND item IN (SELECT item_id FROM EX2_Items WHERE type = (SELECT type FROM EX2_Items WHERE item_id = @item))", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@item", itemID);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("UPDATE EX2_Inventory SET equipped = 1 WHERE player = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND item = @item ", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@item", itemID);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public static DataTable getControls()
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM EX2_Controls", connection);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static void setUserControl(string control, string keycode = null)
        {
            if (datastatus)
            {
                MySqlCommand cmd;
                connection.Open();
                if (keycode == null)
                {
                    cmd = new MySqlCommand("UPDATE EX2_UserControls SET keycode = (SELECT default_keycode FROM EX2_Controls WHERE control_id = @control) WHERE control = @control AND user = (SELECT user_id FROM EX2_Accounts WHERE username = @username)", connection);
                }
                else
                {
                    cmd = new MySqlCommand("UPDATE EX2_UserControls SET keycode = @keycode WHERE control = @control AND user = (SELECT user_id FROM EX2_Accounts WHERE username = @username)", connection);
                    cmd.Parameters.AddWithValue("@keycode", keycode);
                }
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@control", control);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static DataTable getUserControls()
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT keycode,title,control_id FROM EX2_UserControls , EX2_Controls WHERE control = control_id AND user = (SELECT user_id FROM EX2_Accounts WHERE username = @username)", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }
        
        public static DataTable getUserControl(string control)
        {
            data = new DataTable();
            if (datastatus)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT keycode,title,control_id FROM EX2_UserControls , EX2_Controls WHERE control = control_id AND user = (SELECT user_id FROM EX2_Accounts WHERE username = @username) AND control_id = @control", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@control", control);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static void createUserControl(string controlID,string username, string keycode = null)
        {
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd;
                if (keycode != null)
                {
                    cmd = new MySqlCommand("INSERT INTO EX2_UserControls (user,control,keycode) VALUES ((SELECT user_id FROM EX2_Accounts WHERE username = @username),@control,@item)", connection);
                    cmd.Parameters.AddWithValue("@keycode", keycode);
                }
                else
                {
                    cmd = new MySqlCommand("INSERT INTO EX2_UserControls (user,control,keycode) VALUES ((SELECT user_id FROM EX2_Accounts WHERE username = @username),@control,(SELECT default_keycode FROM EX2_Controls WHERE control_id = @control))", connection);
                }
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@control", controlID);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static bool buyItem(string itemID)
        {
            data = new DataTable();
            if (datastatus)
            {
                int balance = 0;
                int price = 1;

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT coins  FROM EX2_Accounts WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();

                if (data.Rows.Count == 0) return false;
                balance = int.Parse(data.Rows[0][4].ToString());

                data.Clear();

                connection.Open();
                cmd = new MySqlCommand("SELECT price  FROM EX2_Items WHERE item_id = @item", connection);
                cmd.Parameters.AddWithValue("@item", itemID);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();

                if (data.Rows.Count == 0) return false;
                price = int.Parse(data.Rows[0][5].ToString());

                data.Clear();

                if (price > balance) return false;
                try
                {
                    connection.Open();
                    cmd = new MySqlCommand("UPDATE EX2_Accounts SET coins = @balance WHERE username = @username", connection);
                    cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                    cmd.Parameters.AddWithValue("@balance", balance - price);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("INSERT INTO EX2_Inventory (player,item) VALUES ((SELECT user_id FROM EX2_Accounts WHERE username = @username),@item)", connection);
                    cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                    cmd.Parameters.AddWithValue("@item", itemID);
                    cmd.ExecuteNonQuery();
                    connection.Close();


                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
