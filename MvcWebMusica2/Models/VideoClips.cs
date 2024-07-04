namespace MvcWebMusica2.Models;

public partial class VideoClips
{
    public int Id { get; set; }

    public int? CancionesId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Canciones? Canciones { get; set; }

    public virtual ICollection<VideoClipsPlataformas> VideoClipsPlataformas { get; set; } = new List<VideoClipsPlataformas>();
}
