using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteViceri_Herois.Data;
using TesteViceri_Herois.Model;

namespace TesteViceri_Herois.Controllers
{
    [ApiController]
    public class SuperpoderesController : ControllerBase
    {
        #region Variáveis
        private readonly ApplicationContext _context;
        #endregion

        #region Construtores
        public SuperpoderesController(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        #region Metódos Públicos
        [HttpPost, Route("/api/AdcionarSuperpoder")]
        public async Task<ActionResult<SuperpoderesModel>> AdcionarSuperpoderes(SuperpoderesModel superPoder)
        {
            string mensagem = "Superpoder cadastrado";

            _context.Superpoderes.Add(superPoder);
            await _context.SaveChangesAsync();

            return Ok(mensagem);
        }

        [HttpDelete, Route("/api/DeletarSuperpoder/{id}")]
        public async Task<IActionResult> DeletarSuperpoderes(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);

            string mensagem = "Não há conteudo";
            string mensagemOK = "Superpoder deletado";

            if (heroi == null)
            {
                return NotFound(mensagem);
            }
            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return Ok(mensagemOK);
        }

        [HttpPut, Route("/api/AtualizaSuperPoder/{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, SuperpoderesModel superpoderes)
        {
            string mensagemBadRequest = "Id não localizado";

            if (id != superpoderes.Id)
            {
                return BadRequest(mensagemBadRequest);
            }

            _context.Entry(superpoderes).State = EntityState.Modified;

            try
            {
                string mensagem = "Superpoder Atualizado";

                await _context.SaveChangesAsync();
                return Ok(mensagem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteSuperpoder(id))
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

        [HttpGet, Route("/api/ListaSuperpoder")]
        public async Task<ActionResult<IEnumerable<HeroisSuperPoderesModel>>> ListaSuperpoder()
        {
            var superpoder = await _context.Superpoderes.ToListAsync();
            if (superpoder == null)
            {
                return NoContent();
            }

            return Ok(superpoder);
        }
        #endregion

        private bool ExisteSuperpoder(int id)
        {
            return _context.Herois.Any(e => e.Id == id);
        }
    }
}
