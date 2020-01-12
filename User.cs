using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class User
    {
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string nickname { get; set; } 
        public int wallet { get; set; }
        //   private Boolean wagerAvlb;
        //   private Boolean wagerOverride;

        public User()
        {

        }
        public User(string id, string name, string password, string email, string nickname, int wallet/*, bool wagerAvlb, bool wagerOverride*/)
        {
            _id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.nickname = nickname;
            this.wallet = wallet;
         //   this.wagerAvlb = wagerAvlb;
         //   this.wagerOverride = wagerOverride;
        }

        public string _id { get; set; }
       
        public override string ToString()
        {
            return _id + " " + name + " " + email + " " + wallet;
        }

    }
}
