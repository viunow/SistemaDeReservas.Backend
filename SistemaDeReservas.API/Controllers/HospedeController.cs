using Microsoft.AspNetCore.Mvc;
using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.Servicos;
using SistemaDeReservas.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;

namespace SistemaDeReservas.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HospedeController : ControllerBase
    {
        private readonly HospedeService _hospedeService;
        public HospedeController(HospedeService hospedeService)
        {
            _hospedeService = hospedeService;
        }

        //Cadastrar um hospede
        [HttpPost]
        public IActionResult AddHospede(HospedeInputModel inputModel)
        {
            var id = _hospedeService.AddHospede(inputModel);
            return Ok(id);
        }

        //Obter hospede pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterHospedePorId(Guid id)
        {
            try
            {
                var hospede = _hospedeService.ObterHospedePorId(id);
                return Ok(hospede);
            }
            catch (Exception)
            {

                return BadRequest("Não foi possível obter o hóspede por Id.");
            }
        }

        //Editar um hospede
        [HttpPatch("{id}/removerpendencia")]
        public IActionResult RemoverPendencia(Guid id) 
        {
            _hospedeService.RemoverPendencia(id);
            return Ok(_hospedeService.ObterHospedePorId(id));
        }

        //Deletar um hospede
        [HttpDelete("{id}")]
        public IActionResult DeletarHospede(Guid id)
        {
            _hospedeService.Delete(id);
            return NoContent();
        }


        //Listar todos os hospedes
        [HttpGet]
        public List<HospedeViewModel> ListarHospedes()
        {
            var hospedes = _hospedeService.ListarHospedes();
            return hospedes;
        }
    }
}