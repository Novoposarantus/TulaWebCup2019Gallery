namespace Models.DtoModels
{
    public class FilterDto
    {
        public int ImagesOnPageCount { get; set; }
        public int PageNumber { get; set; }
        public string[] Tags { get; set; }
        public int SortBy { get; set; } 
        public bool ReverseSort { get; set; }
    }
}
