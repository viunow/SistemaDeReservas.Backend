using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SistemaDeReservas.Dominio.Enums;

namespace SistemaDeReservas.Dominio.Entidades
{
    public class Quarto
    {
        [BsonElement("Id")]
        [BsonId]
        public Guid Id { get; private set; }
        public int Numero { get; private set; }
        public TipoQuarto Tipo { get; private set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Preco { get; private set; }

        public Quarto(TipoQuarto tipoQuarto, int numero)
        {
            Id = Guid.NewGuid();

            if (tipoQuarto == TipoQuarto.Convencional)
                Preco = 98;

            if (tipoQuarto == TipoQuarto.Luxo)
                Preco = 150;

            Tipo = tipoQuarto;
            Numero = numero;
        }

        public void ModificarQuarto(int numero, TipoQuarto tipoQuarto)
        {
            Numero = numero;
            Tipo = tipoQuarto;
        }
    }
}
