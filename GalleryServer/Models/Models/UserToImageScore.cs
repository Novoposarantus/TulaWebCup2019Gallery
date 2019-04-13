using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class UserToImageScore
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int ImageId { get; set; }
        public ImageModel Image { get; set; }
        public int ScoreId { get; set; }
        public ScoreModel Score { get; set; }
    }
}
