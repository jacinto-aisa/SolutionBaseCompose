using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class GrupoSpecification (int grupoId): IArtistaSpecification
    {
        public bool IsValid(Artistas element)
        {
            return element.GruposId == grupoId;
        }
    }
}
