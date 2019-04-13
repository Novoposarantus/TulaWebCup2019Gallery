using Models.DtoModels;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        IEnumerable<ImageDto> Images { get; }
        ImageDto Get(int id);
        IEnumerable<ImageDto> Get(FilterDto filter);
        void Save(ImageDto image, int userId);
        void AddTags(ImageTagsDto image, int userId);
        void AddScore(int scoreValue, int imageId, int userId);
    }
}
