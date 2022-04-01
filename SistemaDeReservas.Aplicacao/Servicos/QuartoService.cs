using SistemaDeReservas.Aplicacao.InputModels;
using SistemaDeReservas.Aplicacao.ViewModels;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Infra.Repositorios;
using System;
using System.Collections.Generic;

namespace SistemaDeReservas.Aplicacao.Servicos
{
    public class QuartoService
    {
        private readonly QuartoRepositorio _quartoRepositorio;
        public QuartoService(QuartoRepositorio quartoRepositorio)
        {
            _quartoRepositorio = quartoRepositorio;
        }

        public Guid AddQuarto(QuartoInputModel inputModel)
        {
            var quartoExistente = _quartoRepositorio.ObterPorNumero(inputModel.Numero);
            if (quartoExistente != null)
                throw new Exception("Número de quarto já cadastrado, por favor utilize outro número.");


            var quarto = new Quarto(inputModel.TipoQuarto, inputModel.Numero);
            _quartoRepositorio.Inserir(quarto);
            return quarto.Id;
        }

        public QuartoViewModel ObterQuartoPorId(Guid id)
        {
            var quarto = _quartoRepositorio.ObterPorId(id);

            if (quarto != null)
            {
                return new QuartoViewModel()
                {
                    Id = quarto.Id,
                    TipoQuarto = quarto.Tipo,
                    Preco = quarto.Preco,
                    Numero = quarto.Numero
                };
            }

            return null;
        }

        public List<QuartoViewModel> ListarQuartos()
        {
            var quartos = _quartoRepositorio.ObterTodos();

            var list = new List<QuartoViewModel>();

            foreach (var quarto in quartos)
            {
                list.Add(new QuartoViewModel()
                {
                    Id = quarto.Id,
                    TipoQuarto = quarto.Tipo,
                    Preco = quarto.Preco,
                    Numero = quarto.Numero
                });
            }

            return list;
        }

        public void Update(Guid id, QuartoInputModel model)
        {
            var quarto = _quartoRepositorio.ObterPorId(id);
            quarto.ModificarQuarto(model.Numero, model.TipoQuarto);
            _quartoRepositorio.Atualizar(quarto);
        }

        public void Delete(Guid id)
        {
            _quartoRepositorio.Remover(id);
        }
    }
}
