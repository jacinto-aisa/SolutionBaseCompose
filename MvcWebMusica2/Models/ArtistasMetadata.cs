using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(ArtistasMetadata))]
    public partial class Artistas { }
    public class ArtistasMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre Artista")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Género Musical")]
        public int? GenerosId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly? FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Ciudad Nacimiento")]
        public int? CiudadesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Grupo Musical")]
        public int? GruposId { get; set; }

        [DisplayName("Ciudad Nacimiento")]
        public virtual Ciudades? Ciudades { get; set; }

        [DisplayName("Género Musical")]
        public virtual Generos? Generos { get; set; }

        [DisplayName("Grupo Musical")]
        public virtual Grupos? Grupos { get; set; }

    }
}
