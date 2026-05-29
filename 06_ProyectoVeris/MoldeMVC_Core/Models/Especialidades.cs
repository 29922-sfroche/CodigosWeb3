
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MoldeMVC_Core.Models
{
    public class Especialidades
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

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100)]
        [Display(Name = "Especialidad")]
        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? descripcion
        {
            get => Descripcion;
            set => Descripcion = value;
        }

        [Required(ErrorMessage = "Los días de atención son obligatorios.")]
        [Display(Name = "Días de Atención")]
        [BsonElement("dias")]
        public string Dias { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? dias
        {
            get => Dias;
            set => Dias = value;
        }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "Formato HH:mm:ss")]
        [Display(Name = "Hora Inicio")]
        [BsonElement("franjaHI")]
        public string FranjaHI { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? franjaHI
        {
            get => FranjaHI;
            set => FranjaHI = value;
        }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "Formato HH:mm:ss")]
        [Display(Name = "Hora Fin")]
        [BsonElement("franjaHF")]
        public string FranjaHF { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? franjaHF
        {
            get => FranjaHF;
            set => FranjaHF = value;
        }

    }
}

/******************* EJEMPLO DE LA ESTRUCTURA JSON *******************
    {
      "_id": {
        "$oid": "65a300000000000000000001"
      },
      "descripcion": "Cardiologia",
      "dias": "MJV",
      "franjaHI": "08:00:00",
      "franjaHF": "12:00:00"
    }
 * 
 ***************************************************************************
 */
