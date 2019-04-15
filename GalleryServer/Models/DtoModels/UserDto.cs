using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class UserDto
    {
        public UserDto() { }

        public UserDto(UserModel user)
        {
            UserName = user.UserName;
            RoleId = user.RoleId;
        }

        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
