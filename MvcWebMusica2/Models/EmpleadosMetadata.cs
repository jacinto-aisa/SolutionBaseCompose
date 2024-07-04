using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(EmpleadosMetadata))]
    public partial class Empleados { }
    public class EmpleadosMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre del Empleado")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Rol del Empleado")]
        public int? RolesId { get; set; }

        [DisplayName("Rol del Empleado")]
        public virtual Roles? Roles { get; set; }
    }
}
