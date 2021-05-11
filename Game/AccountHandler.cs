using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class AccountHandler
    {
        string username;
        public AccountHandler(string usernameData)
        {
            username = usernameData;
        }

        public string getUsername() {
            return username;
        }
    }
}
