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

        public IEnumerable<ImageDto> Images
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Images
                        .Include(image => image.UserToImageTags)
                        .ThenInclude(uiTag => uiTag.Tag)
                        .Include(image => image.UserToImageScores)
                        .ThenInclude(uiScore => uiScore.Score)
                        .Select(image => new ImageDto(image));
                }
            }
        }

        public int GetImageCount()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Images.Count();
            }
        }

        public ImageDto Get(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var imageModel = context.Images
                    .Include(image => image.UserToImageTags)
                    .ThenInclude(image => image.Tag)
                    .FirstOrDefault(image => image.Id == id);
                if(imageModel == null)
                {
                    throw new ImageRepositoryException("Картинка не найдена");
                }
                return new ImageDto(imageModel);
            }
        }

        public List<ImageDto> Get(FilterDto filter)
        {
            var minImageCountForPage = filter.ImagesOnPageCount * (filter.PageNumber - 1) + 1;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                IEnumerable<ImageDto> images = context.Images
                    .Include(image => image.UserToImageTags)
                    .ThenInclude(image => image.Tag)
                    .Where(image => filter.Tags == null || filter.Tags.Count() == 0
                        ? true
                        : image.UserToImageTags.Any(uit => filter.Tags.Contains(uit.Tag.Name)))
                    .Skip(minImageCountForPage - 1)
                    .Take(filter.ImagesOnPageCount)
                    .Select(image => new ImageDto(image));

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

        public void AddScore(int scoreValue, int imageId, int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var score = context.Scores.FirstOrDefault(x => x.Value == scoreValue);
                if(score == null)
                {
                    throw new ImageRepositoryException("Оценки не существует");
                }
                context.UserToImageScores.Add(new UserToImageScore()
                {
                    ScoreId = score.Id,
                    ImageId = imageId,
                    UserId = userId
                });
                context.SaveChanges();
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
