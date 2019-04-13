using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(string userName, string password, int roleId)
        {
            UserName = userName;
            Password = password;
            RoleId = roleId;
        }
        public int Id { get;set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("RoleModel")]
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}
