using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public interface IArtistaSpecification
    {
        bool IsValid(Artistas element);
    }
}

