namespace MvcWebMusica2.Models;

public partial class Grupos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool grupo { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public int? CiudadesId { get; set; }

    public int? RepresentantesId { get; set; }

    public int? GenerosId { get; set; }

    public virtual ICollection<Albumes> Albumes { get; set; } = new List<Albumes>();

    public virtual ICollection<Artistas> Artistas { get; set; } = new List<Artistas>();

    public virtual Ciudades? Ciudades { get; set; }

    public virtual Generos? Generos { get; set; }

    public virtual ICollection<Giras> Giras { get; set; } = new List<Giras>();

    public virtual Representantes? Representantes { get; set; }
}
