using MongoDB.Driver;
using SistemaDeReservas.Dominio.Entidades;
using SistemaDeReservas.Infra.Repositorios.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeReservas.Infra.Repositorios
{
    public class HospedeRepositorio : BaseRepositorio<Hospede>
    {
        public HospedeRepositorio(DbAccess dbAccess) : base(dbAccess, "colHospedes")
        {
            
        }
    }
}