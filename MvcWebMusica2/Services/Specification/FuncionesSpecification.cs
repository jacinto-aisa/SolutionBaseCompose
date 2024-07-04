
using MvcWebMusica2.Models;


namespace MvcWebMusica2.Services.Specification
{
    public class FuncionesSpecification (int artistaId): IFuncionSpecification
    {
        public bool IsValid(FuncionesArtistas element)
        {
            return element.ArtistasId == artistaId;
        }
    }
}