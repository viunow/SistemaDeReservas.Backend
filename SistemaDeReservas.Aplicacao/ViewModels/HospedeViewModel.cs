using System;

namespace SistemaDeReservas.Aplicacao.ViewModels
{
    public class HospedeViewModel
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public bool Pendencia { get; set; }
    }
}
