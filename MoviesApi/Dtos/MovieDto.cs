namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        [MaxLength(100)]
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        
        [MaxLength(2500)]
        public string StoreLine { get; set; }

        public  IFormFile Poster { get; set; }

        public int GeneraId { get; set; }

    }
}
