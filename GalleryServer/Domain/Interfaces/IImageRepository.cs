using Models.DtoModels;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        IEnumerable<ImageDto> Images { get; }
        IEnumerable<ImageDto> GetImages(FilterDto filter);
        void AddTags(ImageDto image, int userId);
        void AddScore(int scoreValue, int imageId, int userId);
    }
}
