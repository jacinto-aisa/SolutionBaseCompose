namespace MvcWebMusica2.Models;

public partial class Funciones
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<FuncionesArtistas> FuncionesArtistas { get; set; } = new List<FuncionesArtistas>();
}
