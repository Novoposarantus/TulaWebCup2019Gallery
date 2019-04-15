using Models.Models;

namespace Models.DtoModels
{
    public class CaruselImageDto : ImageDto
    {
        public CaruselImageDto() :base () { }
        public CaruselImageDto(ImageModel model) : base(model) { }

        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
    }
}
