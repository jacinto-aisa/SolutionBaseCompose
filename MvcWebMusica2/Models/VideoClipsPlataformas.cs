namespace MvcWebMusica2.Models;

public partial class VideoClipsPlataformas
{
    public int Id { get; set; }

    public int? PlataformasId { get; set; }

    public int? VideoClipsId { get; set; }

    public string? url { get; set; }

    public virtual Plataformas? Plataformas { get; set; }

    public virtual VideoClips? VideoClips { get; set; }
}
