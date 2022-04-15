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
            string mensagem = "Heroi e Superpoder Cadastrado";

            _context.HeroisSuperpoderes.Add(heroisSuperPoderesModel);
            await _context.SaveChangesAsync();

            return Ok(mensagem);
        }

        [HttpDelete, Route("/api/DeletarHeroisSuperPoderesModel/{id}")]
        public async Task<IActionResult> DeletarHeroisSuperPoderesModel(int id)
        {
            string mensagemNotFound = "Heroi Superpoder não encontrado";
            string mensagemOk = "Heroi Superpoder deletado";

            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound(mensagemNotFound);
            }
            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return Ok(mensagemOk);
        }

        [HttpPut, Route("/api/AtualizaHeroiSuperPoder/{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, HeroisSuperPoderesModel heroi)
        {
            string mensagemBadRequest = "Id não localizado";

            if (id != heroi.HeroiId)
            {
                return BadRequest(mensagemBadRequest);
            }

            _context.Entry(heroi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                string mensagem = "Heroi Superpoder atualizado";
                return Ok(mensagem);
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
          
            var heroiSuperpoder = await _context.HeroisSuperpoderes.ToListAsync();
            if (heroiSuperpoder == null)
            {
                return NoContent();
            }

            return Ok(heroiSuperpoder);
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
