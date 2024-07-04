namespace MvcWebMusica2.Models;

public partial class Giras
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GruposId { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual ICollection<Conciertos> Conciertos { get; set; } = new List<Conciertos>();

    public virtual Grupos? Grupos { get; set; }
}
