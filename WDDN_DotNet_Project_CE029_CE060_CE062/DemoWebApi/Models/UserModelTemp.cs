using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWebApi.Models
{
    public class UserModelTemp
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public User ConvertToUser()
        {
            User user = new User();
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Email = this.Email;
            user.Password = this.Password;
            return user;
        }
    }
}