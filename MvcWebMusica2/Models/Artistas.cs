namespace MvcWebMusica2.Models;

public partial class Artistas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GenerosId { get; set; }

    public DateOnly? FechaDeNacimiento { get; set; }

    public int? CiudadesId { get; set; }

    public int? GruposId { get; set; }

    public virtual Ciudades? Ciudades { get; set; }

    public virtual ICollection<FuncionesArtistas> FuncionesArtistas { get; set; } = [];

    public virtual Generos? Generos { get; set; }

    public virtual Grupos? Grupos { get; set; }
}
