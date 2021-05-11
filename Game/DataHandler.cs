using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class DataHandler
    {
        MySqlConnection connection;
        MySqlDataAdapter daGegevens;
        DataTable data = new DataTable();

        private string connectionString;
        private bool datastatus;

        public DataHandler()
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

        public bool isTaken(string username)
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

        public bool loginUser(string username, string password)
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
        public bool createUser(string username, string password)
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

        public string gethash(string password)
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
            hash = chars.ToString();
            return getRawHash(hash);
            
        }

        public string getRawHash(string password)
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

        public DataTable getTop(int amount)
        {
            data = new DataTable();
            if (datastatus)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT A.username,M.score,M.zombie_kills,M.wave  FROM Game_Matches M, Game_Accounts A  WHERE M.player = A.user_id ORDER BY M.score LIMIT @amount", connection);
                cmd.Parameters.AddWithValue("@amount", amount);
                daGegevens = new MySqlDataAdapter(cmd);
                daGegevens.Fill(data);
                connection.Close();
            }
            return data;
        }
    }
}
