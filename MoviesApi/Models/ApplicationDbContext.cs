namespace MoviesApi.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Genera> Generas { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
