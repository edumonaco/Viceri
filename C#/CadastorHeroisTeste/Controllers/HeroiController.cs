using CadastorHeroisTeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CadastorHeroisTeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController : ControllerBase
    {
        private readonly Contexto _context;

        public HeroiController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Heroi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var heroList = await _context.Herois.ToListAsync();
            if  (heroList?.Count == 0)
            {
                return Ok("Não existem Herois cadastrados");
            }
            return Ok(heroList);
        }

        // GET: api/Heroi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var heroi = await _context.Herois.FirstOrDefaultAsync(m => m.Id == id);
            if (heroi == null)
            {
                return NotFound("Id inválido");
            }
            return Ok(heroi);
        }

        // POST: api/Heroi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Heroi heroi)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool heroiExists = await _context.Herois
                .AnyAsync(h => h.Nome == heroi.Nome);

            if (heroiExists)
            {
                return Conflict("Já existe um herói com o mesmo nome.");
            }

            if (heroi.SuperPoderes != null)
            {
                _context.AddRange(heroi.SuperPoderes);
            }
            await _context.SaveChangesAsync();
            _context.Add(heroi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = heroi.Id }, heroi);
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Heroi heroi)
        {
            if (id != heroi.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeHeroi = await _context.Herois.FindAsync(id);
            if (existeHeroi == null)
            {
                return NotFound($"Heroi com ID {id} não encontrado.");
            }

            bool heroiExists = await _context.Herois
               .AnyAsync(h => h.Nome == heroi.Nome);

            if (heroiExists)
            {
                return Conflict("Já existe um herói com o mesmo nome.");
            }

            try
            {
                _context.Update(heroi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroiExists(heroi.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Heroi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound();
            }

            var existeHeroi = await _context.Herois.FindAsync(id);
            if (existeHeroi == null)
            {
                return NotFound($"Heroi com ID {id} não encontrado.");
            }

            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroiExists(int id)
        {
            return _context.Herois.Any(e => e.Id == id);
        }
    }
}
