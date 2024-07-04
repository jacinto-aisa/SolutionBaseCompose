using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(VideoClipsMetadata))]
    public partial class VideoClips { }
    public class VideoClipsMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Nombre de la Canción")]
        public int? CancionesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Fecha Publicación")]
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        [DisplayName("Nombre de la Canción")]
        public virtual Canciones? Canciones { get; set; }
    }
}
