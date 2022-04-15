using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteViceri_Herois.Data;
using TesteViceri_Herois.Model;

namespace TesteViceri_Herois.Controllers
{
    [ApiController]
    public class HeroisController : ControllerBase
    {
        #region Variáveis
        private readonly ApplicationContext _context;
        #endregion

        #region Construtores
        public HeroisController(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        #region Metódos Públicos
        [HttpPost, Route("/api/AdcionarHeroi")]
        public async Task<ActionResult<HeroisModel>> AdcionarHeroi(HeroisModel herois)
        {
            string mensagem = "Heroi Cadastrado";

            _context.Herois.Add(herois);
            await _context.SaveChangesAsync();

            return Ok(mensagem);
        }

        [HttpDelete, Route("/api/DeletarHeroi/{id}")]
        public async Task<IActionResult> DeletarHeroi(int id)
        {
            string mensagem = "Heroi Não Encontrado";
            string mensagemOk = "Heroi Deletado";
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound(mensagem);
            }
            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return Ok(mensagemOk);
        }

        [HttpPut, Route("/api/AtualizaHeroi{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, HeroisModel heroi)
        {
            string mensagem = "Id não localizado";
            if (id != heroi.Id)
            {
                return BadRequest(mensagem);
            }

            string mensagemNoContent = "Não há herois cadastrados";
            _context.Entry(heroi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteHeroi(id))
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

        [HttpGet, Route("/api/ListaHerois")]
        public async Task<ActionResult<IEnumerable<HeroisModel>>> ListaHeroi()
        {
            var heroi = await _context.Herois.ToListAsync();
            if (heroi == null)
            {
                return NoContent();
            }

            return Ok(heroi);
        }

        [HttpGet, Route("/api/ConsultaHeroiId/{id}")]
        public async Task<ActionResult<HeroisModel>> ConsultaHeroiPeloId(int id)
        {
            string mensagem = "Heroi não localizado";

            var heroi = await _context.Herois.FindAsync(id);

            if (heroi == null)
            {
                return NotFound(mensagem);
            }

            return heroi;
        }

        #endregion

        #region Método De Retorno Do Heroi
        private bool ExisteHeroi(int id)
        {
            return _context.Herois.Any(e => e.Id == id);
        }
        #endregion
    }
}
