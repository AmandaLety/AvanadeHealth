using AvanadeHealth.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AvanadeHealth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfissionalController : Controller
    {
        private readonly ProfissionalRepository profissionalRepository;

        public ProfissionalController()
        {
            profissionalRepository = new ProfissionalRepository();
        }

        [HttpGet]
        [Route("/ListarTodos")]
        public IActionResult Consultar()
        {
            try
            {
                var listaProfissional = profissionalRepository.Listar();
                return Ok(listaProfissional);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        [Route("/ConsultarPorId/{id}")]

        public ActionResult Consultar(int id)
        {
            try
            {
                var tipoProfissional = profissionalRepository.Consultar(id);
                if (tipoProfissional.IdProfissional != 0)
                {
                    return Ok(tipoProfissional);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("/CadastrarProfissional")]
        public ActionResult Cadastrar(Entidade.Profissional profissional)
        {
            try
            {
                profissionalRepository.Inserir(profissional);
                return Ok(profissional);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }


        [HttpPut]
        [Route("/AtualizarProfissional")]
        public ActionResult Editar(Entidade.Profissional IdProfissional)
        {

            try
            {
                profissionalRepository.Alterar(IdProfissional);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }



        [HttpDelete]
        [Route("/Excluir")]
        public ActionResult Excluir(int Id)
        {
            try
            {
                profissionalRepository.Excluir(Id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}
