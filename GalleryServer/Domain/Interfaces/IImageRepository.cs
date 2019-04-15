using Models.DtoModels;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IImageRepository
    {
        CaruselImageDto GetNext(FilterIdDto filter, int? userId = null);
        CaruselImageDto GetPrev(FilterIdDto filter, int? userId = null);
        CaruselImageDto Get(FilterIdDto imageData, int? userId = null);
        List<ImageDto> Get(FilterDto filter, int? userId = null);
        int GetImageCount();
        void Save(List<ImageDto> image, int userId);
        void AddTags(ImageTagsDto image, int userId);
        void AddScore(int scoreValue, int imageId, int userId);
    }
}
