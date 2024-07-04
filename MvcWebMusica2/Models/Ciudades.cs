namespace MvcWebMusica2.Models;

public partial class Ciudades
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? PaisesID { get; set; }

    public virtual ICollection<Artistas> Artistas { get; set; } = new List<Artistas>();

    public virtual ICollection<Conciertos> Conciertos { get; set; } = new List<Conciertos>();

    public virtual ICollection<Grupos> Grupos { get; set; } = new List<Grupos>();

    public virtual Paises? Paises { get; set; }

    public virtual ICollection<Representantes> Representantes { get; set; } = new List<Representantes>();
}
