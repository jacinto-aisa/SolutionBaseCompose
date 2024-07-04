using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(GirasMetadata))]
    public partial class Giras { }
    public class GirasMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre de la Gira")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Grupo Musical")]
        public int? GruposId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateOnly? FechaInicio { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha de Finalización")]
        [DataType(DataType.Date)]
        public DateOnly? FechaFin { get; set; }

        [DisplayName("Grupo Musical")]
        public virtual Grupos? Grupos { get; set; }
    }
}
