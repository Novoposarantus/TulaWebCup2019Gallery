using Models.DtoModels;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Enums;
using System.IO;
using System.Drawing;
using Domain.Context;
using System.Text.RegularExpressions;

namespace Domain.Repositories
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        public ImageRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public int GetImageCount()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Images.Count();
            }
        }

        public CaruselImageDto GetNext(FilterIdDto filter, int? userId = null)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var currentImage = GetImage(filter, out List<CaruselImageDto> sortedImages, out int indexOfImage, userId);

                if (indexOfImage + 1 >= sortedImages.Count())
                {
                    throw new ImageRepositoryException("Вне гранниц данных");
                }
                indexOfImage++;
                currentImage = sortedImages[indexOfImage];
                currentImage.IsFirst = indexOfImage == 0;
                currentImage.IsLast = indexOfImage == sortedImages.Count() - 1;
                return currentImage;
            }
        }

        public CaruselImageDto GetPrev(FilterIdDto filter, int? userId = null)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var currentImage = GetImage(filter, out List<CaruselImageDto> sortedImages, out int indexOfImage, userId);
                if(indexOfImage - 1 < 0)
                {
                    throw new ImageRepositoryException("Вне гранниц данных");
                }
                indexOfImage--;
                currentImage = sortedImages[indexOfImage];
                currentImage.IsFirst = indexOfImage == 0;
                currentImage.IsLast = indexOfImage == sortedImages.Count() - 1;
                return currentImage;
            }
        }

        public CaruselImageDto Get(FilterIdDto filter, int? userId = null)
        {
            var currentImage = GetImage(filter, out List<CaruselImageDto> sortedImages, out int indexOfImage, userId);
            currentImage.IsFirst = indexOfImage == 0;
            currentImage.IsLast = indexOfImage == sortedImages.Count() - 1;
            return currentImage;
        }

        public List<ImageDto> Get(FilterDto filter, int? userId = null )
        {
            var minImageCountForPage = filter.ImagesOnPageCount.HasValue
                ? filter.ImagesOnPageCount.Value * (filter.PageNumber - 1) + 1
                : 0;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                IEnumerable<ImageDto> images = context.Images
                    .Include(image => image.UserToImageScores)
                    .ThenInclude(image => image.Score)
                    .Include(image => image.UserToImageTags)
                    .ThenInclude(image => image.Tag)
                    .Where(image => filter.Tags == null || filter.Tags.Count() == 0
                        ? true
                        : image.UserToImageTags.Any(uit => filter.Tags.Contains(uit.Tag.Name)))
                    .Select(image => new ImageDto(image, userId));
                if(filter.ImagesOnPageCount.HasValue)
                {
                    images = images
                                .Skip(minImageCountForPage - 1)
                                .Take(filter.ImagesOnPageCount.Value);
                }

                if (images.Count() == 0)
                {
                    throw new ImageRepositoryException("Недостаточно файлов для вывода страницы");
                }

                return SortImages(images, filter);
            }
        }

        public void Save(List<ImageDto> images, int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                foreach(var image in images)
                {
                    var base64Data = Regex.Match(image.ImageContent, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    ImageModel imageModel = new ImageModel()
                    {
                        Name = image.Name,
                        DateUpload = DateTime.Now,
                        Image = Convert.FromBase64String(base64Data)
                    };
                    context.Images.Add(imageModel);
                }
                context.SaveChanges();
            }
        }

        public void AddTags(ImageTagsDto image, int userId)
        {
            var tags = SaveTags(image.Tags);
            SaveUserToImageTags(tags, image.ImmageId, userId);
        }

        public CaruselImageDto AddScore(int scoreValue, int imageId, int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var score = context.Scores.FirstOrDefault(x => x.Value == scoreValue);
                if(score == null)
                {
                    throw new ImageRepositoryException("Оценки не существует");
                }
                var oldScore = context.UserToImageScores.FirstOrDefault(x => x.ImageId == imageId && x.UserId == userId);
                if(oldScore == null)
                {
                    context.UserToImageScores.Add(new UserToImageScore()
                    {
                        ScoreId = score.Id,
                        ImageId = imageId,
                        UserId = userId
                    });
                }
                else
                {
                    oldScore.ScoreId = score.Id;
                }
                context.SaveChanges();
                return new CaruselImageDto(context.Images
                        .Include(image => image.UserToImageScores)
                        .ThenInclude(image => image.Score)
                        .Include(image => image.UserToImageTags)
                        .ThenInclude(image => image.Tag)
                        .FirstOrDefault(image => image.Id == imageId));
            }
        }

        private CaruselImageDto GetImage(FilterIdDto filter, out List<CaruselImageDto> sortedImages, out int indexOfImage, int? userId = null)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                IEnumerable<ImageDto> images = context.Images
                    .Include(image => image.UserToImageScores)
                    .ThenInclude(image => image.Score)
                    .Include(image => image.UserToImageTags)
                    .ThenInclude(image => image.Tag)
                    .Select(image => new ImageDto(image, userId));

                if (images.Count() == 0)
                {
                    throw new ImageRepositoryException("Недостаточно файлов для вывода страницы");
                }
                sortedImages = SortImages(images, filter).Select(image=> new CaruselImageDto(image)).ToList();
                CaruselImageDto currentImage = sortedImages.FirstOrDefault(i => i.Id == filter.Id);
                if (currentImage == null)
                {
                    throw new ImageRepositoryException("Изображение не найдено");
                }
                indexOfImage = sortedImages.IndexOf(currentImage);
                return currentImage;
            }
        }
        private List<ImageDto> SortImages(IEnumerable<ImageDto> images, FilterDto filter)
        {
            
            switch (filter.SortBy)
            {
                case (int)SortByEnum.Name:
                    return SortBy(x => x.Name, images, filter.ReverseSort);
                case (int)SortByEnum.Rating:
                    return SortBy(x => x.Rating, images, filter.ReverseSort);
                default:
                    return SortBy(x => x.DateUpload, images, filter.ReverseSort);
            }
        }

        private List<ImageDto> SortBy<T>(Func<ImageDto,T> keySelector, IEnumerable<ImageDto> images, bool reverseSort)
        {
            if (reverseSort)
            {
                return images.OrderByDescending(keySelector).ToList();
            }
            return images.OrderBy(keySelector).ToList();
        }

        private void SaveUserToImageTags(IEnumerable<TagModel> tags, int imageId, int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                foreach(var tag in tags)
                {
                    if(context.UserToImageTags.Any(uiTags=> uiTags.TagId == tag.Id 
                        && uiTags.ImageId == imageId
                        && uiTags.UserId == userId))
                    {
                        continue;
                    }
                    context.UserToImageTags.Add(new UserToImageTag()
                    {
                        TagId = tag.Id,
                        ImageId = imageId,
                        UserId = userId
                    });
                }
                context.SaveChanges();
            }
        }

        private IEnumerable<TagModel> SaveTags(IEnumerable<string> tagNames)
        {
            var tags = new List<TagModel>();
            foreach (var tagName in tagNames)
            {
                tags.Add(GetTag(tagName));
            }
            return tags;
        }

        private TagModel GetTag(string tagName)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var tag = context.Tags.FirstOrDefault(x => x.Name == tagName);
                if (tag != null)
                {
                    return tag;
                }
                tag = new TagModel() { Name = tagName };
                var newTag = context.Tags.Add(tag);
                context.SaveChanges();
                return newTag.Entity;
            }
        }
    }
}
