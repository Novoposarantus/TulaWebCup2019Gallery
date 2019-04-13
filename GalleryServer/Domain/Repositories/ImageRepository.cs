﻿using Models.DtoModels;
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

        public IEnumerable<ImageDto> GetImages(FilterDto filter)
        {
            var minImageCountForPage = filter.ImagesOnPageCount * (filter.PageNumber - 1) + 1;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                IEnumerable<ImageDto> images = context.Images
                    .Include(image => image.UserToImageTags)
                    .ThenInclude(image => image.Tag)
                    .Where(image => filter.Tags == null || filter.Tags.Length == 0
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

        public void SaveImage(ImageDto image, int userId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                ImageModel imageModel = new ImageModel()
                {
                    Name = image.Name,
                    DateUpload = DateTime.Now,
                    Image = ImageToByteArray(image.Image)
                };
                context.Images.Add(imageModel);
                context.SaveChanges();
            }
            var tags = SaveTags(image.Tags);
            SaveUserToImageTags(tags, image.Id, userId);
        }

        public void AddTags(ImageDto image, int userId)
        {
            var tags = SaveTags(image.Tags);
            SaveUserToImageTags(tags, image.Id, userId);
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

        private IEnumerable<ImageDto> SortImages(IEnumerable<ImageDto> images, FilterDto filter)
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

        private IEnumerable<ImageDto> SortBy<T>(Func<ImageDto,T> keySelector, IEnumerable<ImageDto> images, bool reverseSort)
        {
            if (reverseSort)
            {
                return images.OrderByDescending(keySelector);
            }
            return images.OrderBy(keySelector);
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
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