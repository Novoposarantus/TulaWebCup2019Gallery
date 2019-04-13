using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class ScoreModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public IEnumerable<UserToImageScore> UserToImageScores { get; set; }
    }
}
