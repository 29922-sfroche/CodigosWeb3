using MoldeMVC_Core.Models;
using MongoDB.Driver;

namespace MoldeMVC_Core.Data
{
    public class VerisMongoContext
    {
        public IMongoCollection<Pacientes> Pacientes { get; }

        public IMongoCollection<Medicos> Medicos { get; }

        public VerisMongoContext(IMongoDatabase database)
        {
            Pacientes = database.GetCollection<Pacientes>("pacientes");

            Medicos = database.GetCollection<Medicos>("medicos");
        }



    }
}