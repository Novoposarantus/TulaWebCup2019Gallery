using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DtoModels
{
    public class ImageDto
    {
        public ImageDto(ImageModel image)
        {
            Id = image.Id;
            Name = image.Name;
            DateUpload = image.DateUpload;
            Image = Convert.ToBase64String(image.Image);
            Rating = GetRating(image.UserToImageScores);
            Tags = image.UserToImageTags.Select(uiTag => uiTag.Tag.Name);

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpload { get; set; }
        public string Image { get; set; }
        public decimal Rating { get; set; }
        public IEnumerable<string> Tags { get; set; }
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
