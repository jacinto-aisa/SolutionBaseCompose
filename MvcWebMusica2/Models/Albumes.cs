namespace MvcWebMusica2.Models;

public partial class Albumes
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GenerosId { get; set; }

    public int? GruposId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = [];

    public virtual Generos? Generos { get; set; }

    public virtual Grupos? Grupos { get; set; }
}
