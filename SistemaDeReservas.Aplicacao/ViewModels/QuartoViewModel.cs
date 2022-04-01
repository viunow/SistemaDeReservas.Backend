using SistemaDeReservas.Dominio.Enums;
using System;

namespace SistemaDeReservas.Aplicacao.ViewModels
{
    public class QuartoViewModel
    {
        public Guid Id { get; set; }
        public TipoQuarto TipoQuarto { get; set; }
        public decimal Preco { get; set; }
        public int Numero { get; set; }
    }
}
