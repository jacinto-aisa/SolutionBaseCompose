namespace MvcWebMusica2.Models;

public partial class Conciertos
{
    public int Id { get; set; }

    public int? GirasId { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? CiudadesId { get; set; }

    public string? Direccion { get; set; }

    public virtual Ciudades? Ciudades { get; set; }

    public virtual Giras? Giras { get; set; }
}
