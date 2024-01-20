using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };

        private long _maxAllowedPosterSize = 1048576;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDto dto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg and .png Extensions are Allowed ");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed Size Is 1MegaByte");

            var isValidGenera = await _context.Generas.AnyAsync(g => g.Id == dto.GeneraId);
            if (!isValidGenera)
                return BadRequest("Enter Valid Genera ");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = new Movie
            {
                GeneraId = dto.GeneraId,
                Title = dto.Title,
                Rate = dto.Rate,
                Poster = dataStream.ToArray(),
                Year = dto.Year,
                StoreLine = dto.StoreLine

            };
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await  _context.Movies.Include(m=> m.Genera).ToListAsync();
            return Ok(movies);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetMovieById(int id)
        //{
        //    var movie = await _context.Movies.Include(m => m.Genera).SingleOrDefaultAsync(m => m.Id == id);
        //    if (movie == null)
        //        return NotFound();

        //    return Ok(movie);
        //}

        [HttpGet("{GetByGeneraId}")]
        public async Task<IActionResult> GetByGeneraId(int generaid)
        { 
            var movies = await _context.Movies
                .Where(m => m.GeneraId == generaid)
                .Include(m => m.Genera)
                .ToListAsync();
            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound("No Movie With This ID");
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok(movie);

        }
        
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDto dto , int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound($"No Movie With This Id : {id}");


            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg and .png Extensions are Allowed ");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed Size Is 1MegaByte");



            movie.Title = dto.Title;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;
            movie.StoreLine = dto.StoreLine;
            movie.Poster = dto.Poster;
            _context.SaveChanges();
            return Ok(movie );
        }
    
    }
}
