using SistemaDeReservas.Dominio.Enums;
namespace SistemaDeReservas.Aplicacao.InputModels
{
    public class QuartoInputModel
    {
        public TipoQuarto TipoQuarto { get; set; }
        public decimal Preco { get; set; }
        public int Numero { get; set; }
    }
}
