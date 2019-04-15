using Models.DtoModels;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        IEnumerable<ImageDto> Images { get; }
        ImageDto Get(int id);
        List<ImageDto> Get(FilterDto filter);
        void Save(List<ImageDto> image, int userId);
        void AddTags(ImageTagsDto image, int userId);
        void AddScore(int scoreValue, int imageId, int userId);
    }
}
