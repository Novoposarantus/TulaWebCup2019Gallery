using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class UserToImageTag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int ImageId { get; set; }
        public ImageModel Image { get; set; }
        public int TagId { get; set; }
        public TagModel Tag { get; set; }
    }
}
