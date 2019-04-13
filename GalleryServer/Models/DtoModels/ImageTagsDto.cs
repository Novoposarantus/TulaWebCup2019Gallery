using System.Collections.Generic;

namespace Models.DtoModels
{
    public class ImageTagsDto
    {
        public int ImmageId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
