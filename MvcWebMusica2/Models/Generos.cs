namespace MvcWebMusica2.Models;

public partial class Generos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Albumes> Albumes { get; set; } = new List<Albumes>();

    public virtual ICollection<Artistas> Artistas { get; set; } = new List<Artistas>();

    public virtual ICollection<Grupos> Grupos { get; set; } = new List<Grupos>();
}
