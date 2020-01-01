using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class User
    {
        private string name;
        private string password;
        private string email;
        private string nickname;
        private int wallet;
        private Boolean wagerAvlb;
        private Boolean wagerOverride;
        public string _id { get; set; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int Wallet { get => wallet; set => wallet = value; }
        public bool WagerAvlb { get => wagerAvlb; set => wagerAvlb = value; }
        public bool WagerOverride { get => wagerOverride; set => wagerOverride = value; }
    }
}
