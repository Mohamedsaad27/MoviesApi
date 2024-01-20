using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGeneras()
        {
            var generas = await _context.Generas.OrderBy(g=> g.Name).ToListAsync();
            return Ok(generas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenera([FromBody]GeneraDto dto)
        {
            var genera = new Genera { Name = dto.Name };
            await _context.Generas.AddAsync(genera);
            _context.SaveChanges();
            return Ok(genera);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateGenera(int id ,[FromBody]GeneraDto dto)
        {
            var genera = await _context.Generas.SingleOrDefaultAsync(g => g.Id == id);

            if (genera == null)
                return NotFound($"No Genera With This Id : {id}");

            genera.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genera);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenera(int id)
        {
            var genera = await _context.Generas.SingleOrDefaultAsync(g => g.Id == id);

            if (genera == null)
                return NotFound($"No Genera With This Id : {id}");
            _context.Generas.Remove(genera);
            _context.SaveChanges();
            return Ok(genera);
        } 
    }
}
