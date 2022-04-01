using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.ViewModels;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Infra.Repositorios;
using System;
using System.Collections.Generic;

namespace SistemaDeReservas.Aplicacao.Servicos
{
    public class HospedeService
    {
        private readonly HospedeRepositorio _hospedeRepositorio;
        public HospedeService(HospedeRepositorio hospedeRepositorio)
        {
            _hospedeRepositorio = hospedeRepositorio;
        }

        public Guid AddHospede(HospedeInputModel inputModel)
        {
            var hospede = Hospede.Create(inputModel.Nome, inputModel.Telefone, inputModel.Email, inputModel.CPF);
            _hospedeRepositorio.Inserir(hospede);
            return hospede.Id;
        }

        public HospedeViewModel ObterHospedePorId(Guid id)
        {
            var hospede = _hospedeRepositorio.ObterPorId(id);

            if (hospede != null)
            {
                return new HospedeViewModel()
                {
                    Id = hospede.Id,
                    NomeCompleto = hospede.NomeCompleto,
                    Telefone = hospede.Telefone,
                    Email = hospede.Email,
                    CPF = hospede.CPF,
                    Pendencia = hospede.Pendencia
                };
            }

            return null;
        }
        public List<HospedeViewModel> ListarHospedes()
        {
            var hospedes = _hospedeRepositorio.ObterTodos();

            var list = new List<HospedeViewModel>();

            foreach (var hospede in hospedes)
            {
                list.Add(new HospedeViewModel()
                {
                    Id = hospede.Id,
                    NomeCompleto = hospede.NomeCompleto,
                    Telefone = hospede.Telefone,
                    Email = hospede.Email,
                    CPF = hospede.CPF,
                    Pendencia = hospede.Pendencia
                });
            }
            
            return list;
        }

        public void RemoverPendencia(Guid id)
        {
            var hospede = _hospedeRepositorio.ObterPorId(id);
            hospede.SetPendencia(false);
            _hospedeRepositorio.Atualizar(hospede);
        }

        public void Delete(Guid id)
        {
            _hospedeRepositorio.Remover(id);
        }

    }
}
