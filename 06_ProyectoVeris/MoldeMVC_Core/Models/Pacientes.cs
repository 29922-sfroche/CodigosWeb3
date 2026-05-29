using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;
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

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [Display(Name = "Cédula")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "La cédula debe tener exactamente 10 dígitos.")]
        [BsonElement("cedula")]
        public int Cedula { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public int cedula
        {
            get => Cedula;
            set => Cedula = value;
        }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(0, 120, ErrorMessage = "Ingrese una edad válida (0-120).")]
        [Display(Name = "Edad")]
        [BsonElement("edad")]
        public int Edad { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public int edad
        {
            get => Edad;
            set => Edad = value;
        }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        [BsonElement("genero")]
        public string Genero { get; set; } = default!;

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public string? genero
        {
            get => Genero;
            set => Genero = value;
        }

        [Required(ErrorMessage = "La estatura es obligatoria.")]
        [Range(50, 250, ErrorMessage = "Ingrese estatura en cm (50-250).")]
        [Display(Name = "Estatura (cm)")]
        [BsonElement("estatura")]
        public int Estatura { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public int estatura
        {
            get => Estatura;
            set => Estatura = value;
        }

        [Required(ErrorMessage = "El peso es obligatorio.")]
        [Range(1.0, 300.0, ErrorMessage = "Ingrese un peso válido (kg).")]
        [Display(Name = "Peso (kg)")]
        [BsonElement("peso")]
        public double Peso { get; set; }

        [BsonIgnore]
        [BindNever]
        [ValidateNever]
        public double peso
        {
            get => Peso;
            set => Peso = value;
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