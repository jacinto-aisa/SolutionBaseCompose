using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class RepresentanteSpecification (int representanteId) : IGrupoSpecification
    {
        public bool IsValid(Grupos element)
        {
            return element.RepresentantesId == representanteId;
        }
    }
}