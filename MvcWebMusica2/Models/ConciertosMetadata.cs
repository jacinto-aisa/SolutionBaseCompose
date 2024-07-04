using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(ConciertosMetadata))]
    public partial class Conciertos { }
    public class ConciertosMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre de la Gira")]
        public int? GirasId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha Concierto")]
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Ciudad")]
        public int? CiudadesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [MaxLength(100, ErrorMessage = "La longitud debe ser menor de 100 caracteres.")]
        [DisplayName("Dirección")]
        public string? Direccion { get; set; }

        [DisplayName("Ciudad")]
        public virtual Ciudades? Ciudades { get; set; }

        [DisplayName("Nombre de la Gira")]
        public virtual Giras? Giras { get; set; }
    }
}
