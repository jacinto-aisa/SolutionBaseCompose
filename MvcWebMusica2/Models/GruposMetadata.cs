using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(GruposMetadata))]
    public partial class Grupos { }
    public class GruposMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre del Grupo")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }

        [DisplayName("Grupo")]
        public bool grupo { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateOnly? FechaCreacion { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Ciudad de creación")]
        public int? CiudadesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Representante")]
        public int? RepresentantesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Género Musical")]
        public int? GenerosId { get; set; }

        [DisplayName("Ciudad de creación")]
        public virtual Ciudades? Ciudades { get; set; }

        [DisplayName("Género Musical")]
        public virtual Generos? Generos { get; set; }

        [DisplayName("Representante")]
        public virtual Representantes? Representantes { get; set; }
    }
}
