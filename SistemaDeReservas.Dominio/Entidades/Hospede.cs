using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SistemaDeReservas.Dominio.Entidades
{
    public class Hospede
    {
        [BsonElement("Id")]
        [BsonId]
        public Guid Id { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public bool Pendencia { get; private set; }

        protected Hospede()
        {

        }

        protected Hospede(string nomeCompleto, string telefone, string email, string cpf)
        {
            Id = Guid.NewGuid();
            NomeCompleto = nomeCompleto;
            Telefone = telefone;
            Email = email;
            CPF = cpf;
            Pendencia = false;
        }

        public static Hospede Create(string nomeCompleto, string telefone, string email, string cpf)
        {
            if (string.IsNullOrEmpty(nomeCompleto))
                throw new Exception("Nome invalido ou nao informado!");

            if (string.IsNullOrEmpty(telefone))
                throw new Exception("Telefone invalido ou nao informado!");

            if (string.IsNullOrEmpty(email))
                throw new Exception("Email invalido ou nao informado!");

            if (string.IsNullOrEmpty(cpf))
                throw new Exception("CPF invalido ou nao informado!");

            return new Hospede(nomeCompleto, telefone, email, cpf);
        }

        public void SetPendencia(bool temPendencia)
        {
            Pendencia = temPendencia;
        }

        public void ModificarHospede(bool pendencia)
        {
            Pendencia = pendencia;
        }
    }
}
