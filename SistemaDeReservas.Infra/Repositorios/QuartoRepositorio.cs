using MongoDB.Driver;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Dominio.Enums;
using SistemaDeReservas.Infra.Repositorios.Base;
using System.Collections.Generic;

namespace SistemaDeReservas.Infra.Repositorios
{
    public class QuartoRepositorio : BaseRepositorio<Quarto>
    {
        public QuartoRepositorio(DbAccess dbAccess) : base(dbAccess, "colQuartos")
        {
            //
        }

        public List<Quarto> ObterListaDoTipo(TipoQuarto tipoQuarto)
        {
            return Colecao.Find(FiltroPorTipo(tipoQuarto)).ToList();
        }

        protected FilterDefinition<Quarto> FiltroPorTipo(TipoQuarto tipoQuarto)
        {
            return Builders<Quarto>.Filter.Eq(p => p.Tipo, tipoQuarto);
        }

        private FilterDefinition<Quarto> FiltroPorNumero(int numero)
        {
            return Builders<Quarto>.Filter.Eq(nameof(Quarto.Numero), numero);
        }

        public Quarto ObterPorNumero(int numero)
        {
            return Colecao.Find(FiltroPorNumero(numero)).FirstOrDefault();
        }
    }
}
