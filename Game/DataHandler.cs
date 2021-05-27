using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
                MySqlCommand sqlcm = new MySqlCommand("SELECT * FROM Game_Accounts WHERE username = @username", connection);
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

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Game_Accounts WHERE username = @username AND hash = @hash", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Game_Accounts WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", username);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);

                if (data.Rows.Count > 0)
                {
                    connection.Close();
                    return false;
                }

                string hash = gethash(password);

                cmd = new MySqlCommand("INSERT INTO Game_Accounts (username,password,hash) VALUES (@username,@password,@hash)", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
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

        public static DataTable getTop(int amount)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT A.username,M.zombie_kills,M.wave,M.duration  FROM Game_Matches M, Game_Accounts A  WHERE M.player = A.user_id ORDER BY M.zombie_kills DESC LIMIT @amount", connection);
                cmd.Parameters.AddWithValue("@amount", amount);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static Size getTypeSizeByName(string name)
        {
            data = new DataTable();
            Size size = new Size(0,0);
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT size FROM Game_ItemTypes WHERE name = @name", connection);
                cmd.Parameters.AddWithValue("@name", name);
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
                MySqlCommand cmd = new MySqlCommand("SELECT username  FROM Game_Accounts WHERE username LIKE CONCAT('%', @search, '%') LIMIT 5", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(zombie_kills),SUM(turrets_placed),SUM(wave),SEC_TO_TIME(SUM(TIME_TO_SEC(duration))),SUM(damage_dealt)  FROM Game_Matches  WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username)", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT coins  FROM Game_Accounts  WHERE username =  @username", connection);
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
                MySqlCommand cmd = new MySqlCommand("UPDATE Game_Accounts SET coins = @coins WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@coins", coins);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static bool isAdmin(string username)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT admin FROM Game_Accounts  WHERE username =  @username AND admin = 1", connection);
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
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Game_Accounts WHERE username = @username AND admin = 0", connection);
                cmd.Parameters.AddWithValue("@username", username);
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
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Game_Matches WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username); UPDATE Game_Accounts SET coins = 0 WHERE username = @username; DELETE FROM Game_Inventory WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND item IN (SELECT item_id FROM Game_Items WHERE price !=0)", connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
        }

        public static void saveGame(int wave,int kills,int turretsPlaced,int damgeDealt,TimeSpan duration)
        {
            if (datastatus)
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO Game_Matches (player,zombie_kills,turrets_placed,wave,duration,damage_dealt) VALUES ((SELECT user_id FROM Game_Accounts WHERE username = @username),@kills,@turrets,@wave,@duration,@damage)", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Game_ItemTypes", connection);
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
                
                MySqlCommand cmd = new MySqlCommand(type==null? "SELECT *  FROM Game_Items ORDER BY price" : "SELECT *  FROM Game_Items WHERE type = @type ORDER BY price", connection);
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

                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Game_Items WHERE item_id = @item", connection);
                cmd.Parameters.AddWithValue("@item", item);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }

        public static DataTable getPlayerItems()
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Game_Inventory WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username)", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT It.source FROM Game_Items It, Game_Inventory Inv WHERE Inv.player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 2 AND Inv.item = It.item_id", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT It.power FROM Game_Items It, Game_Inventory Inv WHERE Inv.player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 1 AND Inv.item = It.item_id", connection);
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
                MySqlCommand cmd = new MySqlCommand("SELECT It.source FROM Game_Items It, Game_Inventory Inv WHERE Inv.player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND Inv.equipped = 1 AND It.type = 1 AND Inv.item = It.item_id", connection);
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
                MySqlCommand cmd = new MySqlCommand("UPDATE Game_Inventory SET equipped = 0 WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND item IN (SELECT item_id FROM Game_Items WHERE type = (SELECT type FROM Game_Items WHERE item_id = @item))", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@item", itemID);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("UPDATE Game_Inventory SET equipped = 1 WHERE player = (SELECT user_id FROM Game_Accounts WHERE username = @username) AND item = @item ", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                cmd.Parameters.AddWithValue("@item", itemID);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public static bool buyItem(string itemID)
        {
            data.Clear();
            if (datastatus)
            {
                int balance = 0;
                int price = 1;

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT coins  FROM Game_Accounts WHERE username = @username", connection);
                cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();

                if (data.Rows.Count == 0) return false;
                balance = int.Parse(data.Rows[0][4].ToString());

                data.Clear();

                connection.Open();
                cmd = new MySqlCommand("SELECT price  FROM Game_Items WHERE item_id = @item", connection);
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
                    cmd = new MySqlCommand("UPDATE Game_Accounts SET coins = @balance WHERE username = @username", connection);
                    cmd.Parameters.AddWithValue("@username", AccountHandler.getUsername());
                    cmd.Parameters.AddWithValue("@balance", balance - price);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("INSERT INTO Game_Inventory (player,item) VALUES ((SELECT user_id FROM Game_Accounts WHERE username = @username),@item)", connection);
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
