using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class AccountHandler
    {
        static string username;
        public static string getUsername() {
            return username;
        }

        public static void setUsername(string userData) {
            username = userData;
        }
    }
}
