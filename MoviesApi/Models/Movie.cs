namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }

        public byte[] Poster { get; set; }

        public int GeneraId { get; set; }

        public Genera Genera { get; set; }
    }
}
