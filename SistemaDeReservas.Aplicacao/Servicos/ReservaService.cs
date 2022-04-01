using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.ViewModels;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Dominio.Enums;
using SistemaDeReservas.Dominio.ValueObjects;
using SistemaDeReservas.Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Aplicacao.Servicos
{
    public class ReservaService
    {
        private readonly ReservaRepositorio _reservaRepositorio;
        private readonly QuartoRepositorio _quartoRepositorio;
        private readonly HospedeRepositorio _hospedeRepositorio;
        public ReservaService(ReservaRepositorio reservaRepositorio, QuartoRepositorio quartoRepositorio, HospedeRepositorio hospedeRepositorio)
        {
            _reservaRepositorio = reservaRepositorio;
            _quartoRepositorio = quartoRepositorio;
            _hospedeRepositorio = hospedeRepositorio;
        }

        public async Task<Guid> EfetuarReservaAsync(ReservaInputModel inputModel)
        {
            var quartoId = inputModel.QuartoId;
            var hospede = _hospedeRepositorio.ObterPorId(inputModel.HospedeId);
            var intervaloDatas = new IntervaloDatas(inputModel.DataEntrada.ToLocalTime(), inputModel.DataSaida.ToLocalTime());

            if (quartoId == default)
                throw new Exception("Quarto não informado.");

            if (hospede.Pendencia)
                throw new Exception("Hóspede com pendência, regularize a multa para poder fazer novas reservas.");

            var reservasAtivasPorQuarto = _reservaRepositorio.GetReservasAtivasPorQuarto(quartoId, intervaloDatas);
            if (reservasAtivasPorQuarto.Count() != 0)
                throw new Exception("Quarto não disponível na data selecionada.");

            var reserva = new Reserva(Guid.NewGuid(), quartoId, inputModel.HospedeId, intervaloDatas);

            await _reservaRepositorio.InserirAsync(reserva);
            return reserva.Id;
        }

        public void EditarReserva(Guid id, ReservaInputModel model)
        {
            var quartoId = model.QuartoId;
            var intervaloDatas = new IntervaloDatas(model.DataEntrada.ToLocalTime(), model.DataSaida.ToLocalTime());
            var reservasAtivasPorQuarto = _reservaRepositorio.GetReservasAtivasPorQuarto(quartoId, intervaloDatas);
            if (reservasAtivasPorQuarto.Count() != 0)
                throw new Exception("Quarto não disponível na data selecionada.");

            var reserva = _reservaRepositorio.ObterPorId(id);
            reserva.ModificarReserva(model.QuartoId, new IntervaloDatas(model.DataEntrada.ToLocalTime(), model.DataSaida.ToLocalTime()));
            _reservaRepositorio.Atualizar(reserva);
        }

        public void DeletarReserva(Guid id)
        {
            var reserva =_reservaRepositorio.ObterPorId(id);
            var quarto = _quartoRepositorio.ObterPorId(reserva.QuartoId);
            var multa = reserva.GetMulta(quarto);
            if (multa > 0)
            {
                var hospede = _hospedeRepositorio.ObterPorId(reserva.HospedeId);
                hospede.SetPendencia(true);
                _hospedeRepositorio.Atualizar(hospede);
            }
            _reservaRepositorio.Remover(id);
        }

        public ReservaViewModel ObterReservaPorId(Guid id)
        {
            var reserva = _reservaRepositorio.ObterPorId(id);
            var quarto = _quartoRepositorio.ObterPorId(reserva.QuartoId);

            return new ReservaViewModel()
            {
                Id = reserva.Id,
                TipoQuarto = quarto.Tipo,
                Numero = quarto.Numero,
                DataEntrada = reserva.DataEntrada.ToLocalTime(),
                DataSaida = reserva.DataSaida.ToLocalTime(),
                QuartoId = reserva.QuartoId,
            };
        }
        public List<ReservaViewModel> ListarReservas()
        {
            var reservas = _reservaRepositorio.ObterTodos();
            var hospedes = _hospedeRepositorio.ObterTodos();
            var quartos = _quartoRepositorio.ObterTodos();

            var list = new List<ReservaViewModel>();

            foreach (var reserva in reservas)
            {
                list.Add(new ReservaViewModel()
                {   
                    Id = reserva.Id,
                    NomeHospede = hospedes.FirstOrDefault(a => a.Id == reserva.HospedeId)?.NomeCompleto,
                    DataEntrada = reserva.DataEntrada.ToLocalTime(),
                    DataSaida = reserva.DataSaida.ToLocalTime(),
                    Situacao = reserva.Situacao,
                    QuartoId = reserva.QuartoId,
                    Numero = quartos.FirstOrDefault(a => a.Id == reserva.QuartoId).Numero,
                    TipoQuarto = quartos.FirstOrDefault(a => a.Id == reserva.QuartoId).Tipo
                });
            }

            return list;
        }
    }
}