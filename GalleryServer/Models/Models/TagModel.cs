using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserToImageTag> UserToImageTags { get; set; }
    }
}
