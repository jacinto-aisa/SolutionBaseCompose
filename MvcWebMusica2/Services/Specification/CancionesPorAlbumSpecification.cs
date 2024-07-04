using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class CancionesPorAlbumSpecification(int albumId) : ICancionSpecification
    {
        public bool IsValid(Canciones cancion)
        {
            return cancion.AlbumesId == albumId;
        }
    }
}
