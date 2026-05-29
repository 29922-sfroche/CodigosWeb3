using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string? Id { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? _id
        {
            get => Id;
            set => Id = value;
        }

        [Required(ErrorMessage = "Debe seleccionar un médico.")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Médico")]
        [BsonElement("medicoId")]
        public string MedicoId { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? medicoId
        {
            get => MedicoId;
            set => MedicoId = value;
        }

        [Required(ErrorMessage = "Debe seleccionar un paciente.")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Paciente")]
        [BsonElement("pacienteId")]
        public string PacienteId { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? pacienteId
        {
            get => PacienteId;
            set => PacienteId = value;
        }

        [Required(ErrorMessage = "La fecha de consulta es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Consulta")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("fechaConsulta")]
        public DateTime FechaConsulta { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public DateTime fechaConsulta
        {
            get => FechaConsulta;
            set => FechaConsulta = value;
        }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "Formato HH:mm:ss")]
        [Display(Name = "Hora Inicio")]
        [BsonElement("hi")]
        public string Hi { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? hi
        {
            get => Hi;
            set => Hi = value;
        }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "Formato HH:mm:ss")]
        [Display(Name = "Hora Fin")]
        [BsonElement("hf")]
        public string Hf { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? hf
        {
            get => Hf;
            set => Hf = value;
        }

        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        [StringLength(500)]
        [Display(Name = "Diagnóstico")]
        [BsonElement("diagnostico")]
        public string Diagnostico { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? diagnostico
        {
            get => Diagnostico;
            set => Diagnostico = value;
        }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? MedicoNombre { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? PacienteNombre { get; set; }
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