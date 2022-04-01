using SistemaDeReservas.Dominio.Enums;
using System;

namespace SistemaDeReservas.Aplicacao.InputModels
{
    public class ReservaInputModel
    {
        public Guid HospedeId { get; set; }
        public Guid QuartoId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
