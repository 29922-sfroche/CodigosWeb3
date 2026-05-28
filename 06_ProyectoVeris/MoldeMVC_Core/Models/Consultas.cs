using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{
    public class Consultas
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = default!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string medicoId { get; set; } = default!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string pacienteId { get; set; } = default!;

        // Asegura que la fecha se almacene en formato UTC en MongoDB
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime fechaConsulta { get; set; } = default!;
        public string hi { get; set; } = default!;
        public string hf { get; set; } = default!;
        public string diagnostico { get; set; } = default!;
    }

}


/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
   {
      "_id": {
        "$oid": "65a300000000000000000401"
      },
      "medicoId": {
        "$oid": "65a300000000000000000201"
      },
      "pacienteId": {
        "$oid": "65a300000000000000000301"
      },
      "fechaConsulta": {
        "$date": "2023-01-10T00:00:00.000Z"
      },
      "hi": "09:00:00",
      "hf": "10:00:00",
      "diagnostico": "Presion arterial alta"
   }
 * 
 ***************************************************************************
 */