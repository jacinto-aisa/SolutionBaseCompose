using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(RolesMetadata))]
    public partial class Roles { }
    public class RolesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre del Rol")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
