using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Infra.Repositorios.Base
{
    public class BaseRepositorio<TColecao>
    {
        protected readonly IMongoCollection<TColecao> Colecao;

        public BaseRepositorio(DbAccess dbAccess, string colecao)
        {
            Colecao = dbAccess.Db.GetCollection<TColecao>(colecao);
        }

        /// <summary>
        /// Obter por Id
        /// </summary>
        public virtual TColecao ObterPorId(Guid id)
        {
            return Colecao.Find(FiltroPorId(id)).FirstOrDefault();
        }

        /// <summary>
        /// Obter todos hospedes
        /// </summary>
        public List<TColecao> ObterTodos()
        {
            //return Colecao.Find(Builders<TColecao>.Filter.Empty).ToList();
            return Colecao.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Inserir varios registros
        /// </summary>
        public virtual void Inserir(IList<TColecao> modelo)
        {
            Colecao.InsertMany(modelo);
        }

        /// <summary>
        /// Inserir um registro
        /// </summary>
        public virtual void Inserir(TColecao modelo)
        {
            Colecao.InsertOne(modelo);
        }

        public virtual async Task InserirAsync(IList<TColecao> modelo)
        {
            await Colecao.InsertManyAsync(modelo);
        }

        public virtual async Task InserirAsync(TColecao modelo)
        {
            await Colecao.InsertOneAsync(modelo);
        }

        public virtual void Atualizar(TColecao modelo)
        {
            Colecao.ReplaceOne(FiltroPorId(BsonTypeMapper.MapToDotNetValue(modelo.ToBsonDocument().GetElement("_id").Value) as Guid?), modelo);
        }

        public virtual void Remover(Guid id)
        {
            Colecao.DeleteOne(FiltroPorId(id));
        }

        protected FilterDefinition<TColecao> FiltroPorId(Guid? id)
        {
            return Builders<TColecao>.Filter.Eq("_id", id);
        }

    }
}
