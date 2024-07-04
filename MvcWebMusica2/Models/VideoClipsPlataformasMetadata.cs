using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(VideoClipsPlataformasMetadata))]
    public partial class VideoClipsPlataformas { }
    public class VideoClipsPlataformasMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Plataforma de publicación")]
        public int? PlataformasId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("VideoClip")]
        public int? VideoClipsId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("URL del VideoClip")]
        [RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)", ErrorMessage = "La URL no es válida.")]
        [DataType(DataType.Url)]
        public string? url { get; set; }

        [DisplayName("Plataforma de publicación")]
        public virtual Plataformas? Plataformas { get; set; }

        [DisplayName("VideoClip")]
        public virtual VideoClips? VideoClips { get; set; }
    }
}
