using MongoDB.Driver;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Dominio.Enums;
using SistemaDeReservas.Dominio.ValueObjects;
using SistemaDeReservas.Infra.Repositorios.Base;
using System;
using System.Collections.Generic;

namespace SistemaDeReservas.Infra.Repositorios
{
    public class ReservaRepositorio : BaseRepositorio<Reserva>
    {
        public ReservaRepositorio(DbAccess dbAccess) : base(dbAccess, "colReservas")
        {
            //
        }

        public List<Reserva> GetReservasAtivasPorQuarto(Guid quartoID, IntervaloDatas intervaloDatas)
        {
            return Colecao.Find(Filtro(quartoID, intervaloDatas)).ToList();
        }

        protected FilterDefinition<Reserva> Filtro(Guid quartoID, IntervaloDatas intervaloDatas)
        {
            return Builders<Reserva>.Filter.Eq(p => p.QuartoId, quartoID) &
                   Builders<Reserva>.Filter.Eq(p => p.Situacao, SituacaoReserva.Aberta) &
                   Builders<Reserva>.Filter.Gt(p => p.DataSaida, intervaloDatas.DataEntrada) &
                   Builders<Reserva>.Filter.Lt(p => p.DataEntrada, intervaloDatas.DataSaida);
        }
    }
}