using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{

    public interface IFuncionSpecification
    {
        bool IsValid(FuncionesArtistas element);
    }

}