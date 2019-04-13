using Models.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Models.DtoModels
{
    public class ImageDto
    {
        public ImageDto(ImageModel image)
        {
            Id = image.Id;
            Name = image.Name;
            DateUpload = image.DateUpload;
            Image = ConvertImage(image.Image);
            Rating = GetRating(image.UserToImageScores);
            Tags = image.UserToImageTags.Select(uiTag => uiTag.Tag.Name);

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpload { get; set; }
        public Image Image { get; set; }
        public decimal Rating { get; set; }
        public IEnumerable<string> Tags { get; set; }
        private Image ConvertImage(byte[] imageArray)
        {
            using (var ms = new MemoryStream(imageArray))
            {
                return Image.FromStream(ms);
            }
        }
        private decimal GetRating(IEnumerable<UserToImageScore> userToImageScores)
        {
            decimal rating = 0;
            foreach(var uiScore in userToImageScores)
            {
                rating += uiScore.Score.Value;
            }
            return rating / userToImageScores.Count();
        }
    }
}
