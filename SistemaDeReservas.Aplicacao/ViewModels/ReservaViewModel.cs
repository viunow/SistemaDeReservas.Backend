using SistemaDeReservas.Dominio.Enums;
using System;

namespace SistemaDeReservas.Aplicacao.ViewModels
{
    public class ReservaViewModel
    {
        public Guid Id { get; set; }
        public string NomeHospede { get; set; }
        public TipoQuarto TipoQuarto { get; set; }
        public int Numero { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public SituacaoReserva Situacao { get; set; }
        public Guid QuartoId { get; set; }
    }
}