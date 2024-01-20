namespace MoviesApi.Models
{
    public class Genera
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
