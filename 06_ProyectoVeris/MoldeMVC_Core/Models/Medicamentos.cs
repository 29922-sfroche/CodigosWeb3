using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{
    public class Medicamentos
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = default!;
        public string nombre { get; set; } = default!;
        public string tipo { get; set; } = default!;


    }
}

/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
    {
      "_id": {
        "$oid": "65a300000000000000000101"
      },
      "nombre": "Paracetamol",
      "tipo": "Analgesico"
    }
 * 
 ***************************************************************************
 */
