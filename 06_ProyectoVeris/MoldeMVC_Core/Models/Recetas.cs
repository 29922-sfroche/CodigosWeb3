using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{
    public class Recetas
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = default!;
        [BsonRepresentation(BsonType.ObjectId)]
        public string consultaId { get; set; } = default!;
        [BsonRepresentation(BsonType.ObjectId)]
        public string medicamentoId { get; set; } = default!;
        public int cantidad { get; set; } = default!;

    }
}

/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
    {
      "_id": {
        "$oid": "65a300000000000000000501"
      },
      "consultaId": {
        "$oid": "65a300000000000000000401"
      },
      "medicamentoId": {
        "$oid": "65a300000000000000000101"
      },
      "cantidad": 2
    }
 * 
 ***************************************************************************
 */
