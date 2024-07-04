using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(FuncionesArtistasMetadata))]
    public partial class FuncionesArtistas { }
    public class FuncionesArtistasMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Función del Artista")]
        public int? FuncionesId { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DisplayName("Artista")]
        public int? ArtistasId { get; set; }

        [DisplayName("Artista")]
        public virtual Artistas? Artistas { get; set; }

        [DisplayName("Función del Artista")]
        public virtual Funciones? Funciones { get; set; }

    }
}
