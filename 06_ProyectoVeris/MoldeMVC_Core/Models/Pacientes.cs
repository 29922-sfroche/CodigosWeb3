using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{
    public class Pacientes
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = default!;
        public string nombre { get; set; } = default!;
        public int cedula { get; set; } = default!;
        public int edad { get; set; } = default!;
        public string genero { get; set; } = default!;
        public int estatura { get; set; } = default!;
        public int peso { get; set; } = default!;
        public string foto { get; set; } = default!;

    }
}

/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
    {
      "_id": {
        "$oid": "65a300000000000000000301"
      },
      "nombre": "Plutarco",
      "cedula": 1718684408,
      "edad": 18,
      "genero": "Masculino",
      "estatura": 160,
      "peso": 60,
      "foto": "usu02.jpg"
    }
 * 
 ***************************************************************************
 */