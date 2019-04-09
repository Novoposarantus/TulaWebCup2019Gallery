using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class User
    {
        public User() { }
        public User(string userName, string password, int roleId)
        {
            UserName = userName;
            Password = password;
            RoleId = roleId;
        }
        public int Id { get;set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
