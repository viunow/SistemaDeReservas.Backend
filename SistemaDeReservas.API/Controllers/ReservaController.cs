using Microsoft.AspNetCore.Mvc;
using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.Servicos;
using SistemaDeReservas.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeReservas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;
        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        //Efetuar uma reserva
        [HttpPost]
        public async Task<IActionResult> AddReservaAsync(ReservaInputModel inputModel)
        {
            new Exception();

            var id = await _reservaService.EfetuarReservaAsync(inputModel);

            return Ok(id);
        }

        //Obter uma reserva pelo ID
        [HttpGet("{id}")]
        public IActionResult ObterReservaPorId(Guid id)
        {
            try
            {
                var reserva = _reservaService.ObterReservaPorId(id);
                return Ok(reserva);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível obter a reserva por Id.");
            }
        }

        //Editar uma reserva
        [HttpPut("{id}")]
        public IActionResult EditarReserva(Guid id, ReservaInputModel model)
        {
            try
            {
                _reservaService.EditarReserva(id, model);
                return Ok(_reservaService.ObterReservaPorId(id));
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível modificar esta reserva, verifique a disponibilidade de quarto ou data da reserva!");
            }
        }

        //Deletar uma reserva
        [HttpDelete("{id}")]
        public IActionResult DeletarReserva(Guid id)
        {
            _reservaService.DeletarReserva(id);
            return NoContent();
        }

        //Listar todas as reservas
        [HttpGet]
        public List<ReservaViewModel> ListarReservas()
        {
            var reservas = _reservaService.ListarReservas();
            return reservas;
        }
    }
}
