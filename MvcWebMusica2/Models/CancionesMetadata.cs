using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(CancionesMetadata))]
    public partial class Canciones { }
    public class CancionesMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Título Canción")]
        [MaxLength(50, ErrorMessage = "La longitud debe ser menor de 50 caracteres.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Duración (hh:mm:ss)")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeOnly? Duracion { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Album")]
        public int? AlbumesId { get; set; }

        [DisplayName("Single")]
        public bool Single { get; set; }

        [DisplayName("Album")]
        public virtual Albumes? Albumes { get; set; }
    }
}
