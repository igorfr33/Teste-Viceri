using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteViceri_Herois.Data;
using TesteViceri_Herois.Model;

namespace TesteViceri_Herois.Controllers
{
    [ApiController]
    public class HeroisSuperpoderesController : ControllerBase
    {
        #region Variáveis
        private readonly ApplicationContext _context;
        #endregion

        #region Construtores
        public HeroisSuperpoderesController(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        #region Metódos Públicos
        [HttpPost, Route("/api/AdcionarHeroisSuperPoder")]
        public async Task<ActionResult<HeroisSuperPoderesModel>> AdcionarHeroiSuperPoder(HeroisSuperPoderesModel heroisSuperPoderesModel)
        {
            _context.HeroisSuperpoderes.Add(heroisSuperPoderesModel);
            await _context.SaveChangesAsync();

            return heroisSuperPoderesModel;
        }

        [HttpDelete, Route("/api/DeletarHeroisSuperPoderesModel/{id}")]
        public async Task<IActionResult> DeletarHeroisSuperPoderesModel(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound();
            }
            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut, Route("/api/AtualizaHeroiSuperPoder/{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, HeroisSuperPoderesModel heroi)
        {
            if (id != heroi.HeroiId)
            {
                return BadRequest();
            }

            _context.Entry(heroi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteHeroiPoder(id))
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet, Route("/api/ListaHeroisSuperPoderes")]
        public async Task<ActionResult<IEnumerable<HeroisSuperPoderesModel>>> ListaHeroi()
        {
            var heroi = await _context.Herois.ToListAsync();
            if (heroi == null)
            {
                return NoContent();
            }

            return Ok(heroi);
        }
        #endregion

        #region Método De Retorno Do Heroi
        private bool ExisteHeroiPoder(int id)
        {
            return _context.Herois.Any(e => e.Id == id);
        }
        #endregion
    }
}
