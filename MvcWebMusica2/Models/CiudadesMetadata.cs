using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(CiudadesMetadata))]
    public partial class Ciudades { }
    public class CiudadesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre Ciudad")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("País")]
        public int? PaisesID { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("País")]
        public virtual Paises? Paises { get; set; }
    }
}
