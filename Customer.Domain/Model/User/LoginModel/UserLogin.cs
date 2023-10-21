using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Model.User.LoginModel
{
    public class UserLogin
    {
        public string Id { get; set; }
        public string AccountType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
