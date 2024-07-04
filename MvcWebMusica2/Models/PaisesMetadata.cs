using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(PaisesMetadata))]
    public partial class Paises { }
    public class PaisesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre del País")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }
    }
}
