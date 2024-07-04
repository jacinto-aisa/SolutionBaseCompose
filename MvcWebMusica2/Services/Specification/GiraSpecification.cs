using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class GiraSpecification(int giraId) : IConciertoSpecification
    {
        public bool IsValid(Conciertos element)
        {
            return element.GirasId == giraId;
        }
    }
}
