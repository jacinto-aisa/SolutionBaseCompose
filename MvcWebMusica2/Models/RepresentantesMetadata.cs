using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(RepresentantesMetadata))]
    public partial class Representantes { }
    public class RepresentantesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre del Representante")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Identificación")]
        public string? Identificacion { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("@E-Mail")]
        //[DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "La dirección de correo no es válida.")]
        public string? mail { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Teléfono")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "El teléfono no es válido.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Ciudad de nacimiento")]
        public int? CiudadesID { get; set; }

        [DisplayName("Ciudad de nacimiento")]
        public virtual Ciudades? Ciudades { get; set; }
    }
}
