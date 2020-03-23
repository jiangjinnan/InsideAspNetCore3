using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class User
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();

        public User() { }
        public User(string userName, string password)
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpper();
            Password = password;
        }
    }

}
