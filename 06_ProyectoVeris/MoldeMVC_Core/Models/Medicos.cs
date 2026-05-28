using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{

    public class Medicos
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = default!;
        public string nombre { get; set; } = default!;
        [BsonRepresentation(BsonType.ObjectId)] 
        public string especialidadId { get; set; } = default!;
        public string foto { get; set; } = default!;
    }


}

/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
    {
      "_id": {
        "$oid": "65a300000000000000000201"
      },
      "nombre": "Manotas",
      "especialidadId": {
        "$oid": "65a300000000000000000001"
      },
      "foto": "usu01.jpg"
    }
 * 
 ***************************************************************************
 */



