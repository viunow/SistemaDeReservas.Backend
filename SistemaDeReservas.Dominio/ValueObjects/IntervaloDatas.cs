using System;

namespace SistemaDeReservas.Dominio.ValueObjects
{
    public struct IntervaloDatas
    {
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }

        public IntervaloDatas(DateTime dataEntrada, DateTime dataSaida)
        {
            if (dataEntrada >= dataSaida)
            {
                throw new ArgumentException("Data inicial não pode ser posterior ou igual a data de saída");
            }

            if (dataEntrada.Hour != 12)
            {
                throw new ArgumentException("A data inicial deve começar ao meio dia");
            }

            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
        }
    }
}
