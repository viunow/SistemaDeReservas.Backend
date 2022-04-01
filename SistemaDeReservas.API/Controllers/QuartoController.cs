using Microsoft.AspNetCore.Mvc;
using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.Servicos;
using SistemaDeReservas.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuartoController : ControllerBase
    {
        private readonly QuartoService _quartoService;
        public QuartoController(QuartoService quartoService)
        {
            _quartoService = quartoService;
        }

        //Cadastrar um quarto
        [HttpPost]
        public IActionResult AddQuarto(QuartoInputModel inputModel)
        {
            var id = _quartoService.AddQuarto(inputModel);
            return Ok(id);
        }

        //Obter um quarto pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterQuartoPorId(Guid id)
        {
            try
            {
                var quarto = _quartoService.ObterQuartoPorId(id);
                return Ok(quarto);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possível obter o quarto por Id.");
            }
        }

        //Editar um quarto
        [HttpPut("{id}")]
        public IActionResult EditarQuarto(Guid id, QuartoInputModel model)
        {
            _quartoService.Update(id, model);
            return Ok(_quartoService.ObterQuartoPorId(id));
        }

        //Deletar um quarto
        [HttpDelete("{id}")]
        public IActionResult DeletarQuarto(Guid id)
        {
            _quartoService.Delete(id);
            return NoContent();
        }

        //Listar todos os quartos
        [HttpGet]
        public List<QuartoViewModel> ListarQuartos()
        {
            var quartos = _quartoService.ListarQuartos();
            return quartos;
        }
    }
}