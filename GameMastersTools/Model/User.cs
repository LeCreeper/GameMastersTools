using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMastersTools.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public User(string userName, string password)
        {
            UserName = userName;
            UserPassword = password;
        }

       
    }
}
