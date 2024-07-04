using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public interface ICancionSpecification
    {
        bool IsValid(Canciones cancion);
    }
}
