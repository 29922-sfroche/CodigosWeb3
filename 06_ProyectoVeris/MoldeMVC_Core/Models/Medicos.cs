using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;
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
        public string? Id { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? _id
        {
            get => Id;
            set => Id = value;
        }

        [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
        [StringLength(200)]
        [Display(Name = "Nombre Completo")]
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

        [Required(ErrorMessage = "Debe seleccionar una especialidad.")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Especialidad")]
        [BsonElement("especialidadId")]
        public string EspecialidadId { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? especialidadId
        {
            get => EspecialidadId;
            set => EspecialidadId = value;
        }

        [Display(Name = "Foto")]
        [BsonElement("foto")]
        public string? Foto { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string foto
        {
            get => Foto;
            set => Foto = value;
        }

        [BsonIgnore]
        [ValidateNever]
        [Display(Name = "Seleccionar Foto")]
        public IFormFile? FotoFile { get; set; }

        [BsonIgnore]
        [ValidateNever]
        public string? EspecialidadNombre { get; set; }
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



