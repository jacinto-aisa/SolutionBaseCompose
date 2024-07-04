namespace MvcWebMusica2.Models;

public partial class Plataformas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<VideoClipsPlataformas> VideoClipsPlataformas { get; set; } = new List<VideoClipsPlataformas>();
}
