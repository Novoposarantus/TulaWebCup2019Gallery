using System.Collections.Generic;

namespace Models.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<ScoreModel> Scores { get; set; }

    }
}
