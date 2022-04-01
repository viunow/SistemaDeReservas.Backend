using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SistemaDeReservas.Infra
{
    public class DbAccess
    {
        public readonly IMongoClient Client;
        public readonly IMongoDatabase Db;
        public DbAccess(IConfiguration configuration)
        {
            Client = new MongoClient(configuration["ConnectionString"]);
            Db = Client.GetDatabase(configuration["database"]);
        }
    }
}
