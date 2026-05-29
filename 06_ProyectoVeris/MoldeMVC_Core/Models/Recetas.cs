using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string? Id { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? _id
        {
            get => Id;
            set => Id = value;
        }

        [Required(ErrorMessage = "Debe seleccionar una consulta.")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Consulta")]
        [BsonElement("consultaId")]
        public string ConsultaId { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? consultaId
        {
            get => ConsultaId;
            set => ConsultaId = value;
        }

        [Required(ErrorMessage = "Debe seleccionar un medicamento.")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Medicamento")]
        [BsonElement("medicamentoId")]
        public string MedicamentoId { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? medicamentoId
        {
            get => MedicamentoId;
            set => MedicamentoId = value;
        }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, 999, ErrorMessage = "La cantidad debe ser entre 1 y 999.")]
        [Display(Name = "Cantidad")]
        [BsonElement("cantidad")]
        public int Cantidad { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public int cantidad
        {
            get => Cantidad;
            set => Cantidad = value;
        }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? MedicamentoNombre { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? ConsultaDescripcion { get; set; }

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
