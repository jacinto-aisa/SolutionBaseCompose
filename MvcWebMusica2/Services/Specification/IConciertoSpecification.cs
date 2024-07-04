using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public interface IConciertoSpecification
    {
        bool IsValid(Conciertos element);
    }
}
