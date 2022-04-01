using MongoDB.Bson.Serialization.Attributes;
using SistemaDeReservas.Dominio.Enums;
using SistemaDeReservas.Dominio.ValueObjects;
using System;

namespace SistemaDeReservas.Dominio.Entidades
{
    public class Reserva
    {
        [BsonElement("Id")]
        [BsonId]
        public Guid Id { get; private set; }
        public Guid QuartoId { get; private set; }
        public decimal Preco { get; private set; }
        public Guid HospedeId { get; set; }
        public SituacaoReserva Situacao { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }
        public DateTime DataCriacao { get; private set; }


        //Construtor da entidade Reserva
        public Reserva(Guid id, Guid quartoId, Guid hospedeId, IntervaloDatas intervaloDatas)
        {
            Id = id;
            QuartoId = quartoId;
            HospedeId = hospedeId;
            DataCriacao = DateTime.UtcNow;
            DataEntrada = intervaloDatas.DataEntrada;
            DataSaida = intervaloDatas.DataSaida;
            Situacao = SituacaoReserva.Aberta;
        }

        public void ModificarReserva(Guid quartoId, IntervaloDatas intervaloDatas)
        {
            TimeSpan intervalo = DataEntrada.ToLocalTime().Subtract(DateTime.UtcNow.ToLocalTime());
            if (intervalo.TotalHours < 24)
                throw new Exception("Você não pode modificar uma reserva com menos de 24 horas de antecedência!");

            QuartoId = quartoId;
            DataEntrada = intervaloDatas.DataEntrada;
            DataSaida = intervaloDatas.DataSaida;
        }

        public decimal GetMulta(Quarto quarto)
        {
            var multa = decimal.Zero;
            
            TimeSpan intervalo = DataEntrada.ToLocalTime().Subtract(DateTime.UtcNow.ToLocalTime());
            if (intervalo.TotalHours < 24)
            {
                multa = (0.2M * quarto.Preco);
            }

            return multa;
        }
    }
}
