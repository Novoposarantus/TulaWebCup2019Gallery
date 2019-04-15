using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DtoModels
{
    public class ImageDto
    {
        public ImageDto() { }
        public ImageDto(ImageModel image, int? userId = null)
        {
            Id = image.Id;
            Name = image.Name;
            DateUpload = image.DateUpload;
            ImageContent = $"data:image/png;base64,{Convert.ToBase64String(image.Image)}";
            Rating = GetRating(image.UserToImageScores);
            Tags = image?.UserToImageTags.Select(uiTag => uiTag.Tag.Name);
            if (userId.HasValue)
            {

                UserRating = image.UserToImageScores?.FirstOrDefault(s => s.UserId == userId.Value)?.Score.Value;
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpload { get; set; }
        public string ImageContent { get; set; }
        public decimal Rating { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int? UserRating { get; set; }
        private decimal GetRating(IEnumerable<UserToImageScore> userToImageScores)
        {
            if(userToImageScores == null || userToImageScores.Count() == 0)
            {
                return 0;
            }
            decimal rating = 0;
            foreach(var uiScore in userToImageScores)
            {
                rating += uiScore.Score.Value;
            }
            return rating / userToImageScores.Count();
        }
    }
}
