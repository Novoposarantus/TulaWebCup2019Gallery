using System.Collections.Generic;

namespace Models.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IEnumerable<UserToImageTag> UserToImageTags { get; set; }
        public IEnumerable<UserToImageScore> UserToImageScores { get; set; }

    }
}
