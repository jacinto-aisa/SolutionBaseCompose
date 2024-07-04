using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public interface IGrupoSpecification
    {
        bool IsValid(Grupos element);
    }
}
