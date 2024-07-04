using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(AlbumesMetadata))]
    public partial class Albumes { }
    public class AlbumesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre Album")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Género Musical")]
        public int? GenerosId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Grupo Musical")]
        public int? GruposId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha Publicación")]
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        [DisplayName("Género")]
        public virtual Generos? Generos { get; set; }

        [DisplayName("Grupo")]
        public virtual Grupos? Grupos { get; set; }
    }
}
