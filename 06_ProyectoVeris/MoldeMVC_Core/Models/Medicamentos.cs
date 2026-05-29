using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string? Id { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? _id
        {
            get => Id;
            set => Id = value;
        }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150)]
        [Display(Name = "Medicamento")]
        [BsonElement("nombre")]
        public string Nombre { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? nombre
        {
            get => Nombre;
            set => Nombre = value;
        }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [Display(Name = "Tipo / Categoría")]
        [BsonElement("tipo")]
        public string Tipo { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? tipo
        {
            get => Tipo;
            set => Tipo = value;
        }


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
