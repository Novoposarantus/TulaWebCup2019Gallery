using Models.Models;

namespace Models.DtoModels
{
    public class CaruselImageDto : ImageDto
    {
        public CaruselImageDto() :base () { }
        public CaruselImageDto(ImageModel model) : base(model) { }
        public CaruselImageDto(ImageDto imageDto): base()
        {
            Id = imageDto.Id;
            Name = imageDto.Name;
            DateUpload = imageDto.DateUpload;
            ImageContent = imageDto.ImageContent;
            Rating = imageDto.Rating;
            Tags = imageDto.Tags;
            UserRating = imageDto.UserRating;
        }

        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
    }
}
