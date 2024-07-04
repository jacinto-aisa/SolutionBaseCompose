namespace MvcWebMusica2.Models;

public partial class Paises
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Ciudades> Ciudades { get; set; } = new List<Ciudades>();
}
