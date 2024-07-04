using Microsoft.EntityFrameworkCore;

namespace MvcWebMusica2.Models;

public partial class GrupoBContext : DbContext
{
    public GrupoBContext()
    {
    }

    public GrupoBContext(DbContextOptions<GrupoBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Albumes> Albumes { get; set; }

    public virtual DbSet<Artistas> Artistas { get; set; }

    public virtual DbSet<Canciones> Canciones { get; set; }

    public virtual DbSet<Ciudades> Ciudades { get; set; }

    public virtual DbSet<Conciertos> Conciertos { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Funciones> Funciones { get; set; }

    public virtual DbSet<FuncionesArtistas> FuncionesArtistas { get; set; }

    public virtual DbSet<Generos> Generos { get; set; }

    public virtual DbSet<Giras> Giras { get; set; }

    public virtual DbSet<Grupos> Grupos { get; set; }

    public virtual DbSet<Paises> Paises { get; set; }

    public virtual DbSet<Plataformas> Plataformas { get; set; }

    public virtual DbSet<Representantes> Representantes { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<VideoClips> VideoClips { get; set; }

    public virtual DbSet<VideoClipsPlataformas> VideoClipsPlataformas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albumes>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Generos).WithMany(p => p.Albumes)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Albumes_Generos");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Albumes)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Albumes_Grupos");
        });

        modelBuilder.Entity<Artistas>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Artistas)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Artistas_Ciudades");

            entity.HasOne(d => d.Generos).WithMany(p => p.Artistas)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Artistas_Generos");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Artistas)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Artistas_Grupos");
        });

        modelBuilder.Entity<Canciones>(entity =>
        {
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Albumes).WithMany(p => p.Canciones)
                .HasForeignKey(d => d.AlbumesId)
                .HasConstraintName("FK_Canciones_Albumes");
        });

        modelBuilder.Entity<Ciudades>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Paises).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.PaisesID)
                .HasConstraintName("FK_Ciudades_Paises");
        });

        modelBuilder.Entity<Conciertos>(entity =>
        {
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Conciertos)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Conciertos_Ciudades");

            entity.HasOne(d => d.Giras).WithMany(p => p.Conciertos)
                .HasForeignKey(d => d.GirasId)
                .HasConstraintName("FK_Conciertos_Giras");
        });

        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Roles).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK_Empleados_Roles");
        });

        modelBuilder.Entity<Funciones>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FuncionesArtistas>(entity =>
        {
            entity.HasOne(d => d.Artistas).WithMany(p => p.FuncionesArtistas)
                .HasForeignKey(d => d.ArtistasId)
                .HasConstraintName("FK_FuncionesArtistas_Artistas");

            entity.HasOne(d => d.Funciones).WithMany(p => p.FuncionesArtistas)
                .HasForeignKey(d => d.FuncionesId)
                .HasConstraintName("FK_FuncionesArtistas_Funciones");
        });

        modelBuilder.Entity<Generos>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Giras>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Grupos).WithMany(p => p.Giras)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Giras_Grupos");
        });

        modelBuilder.Entity<Grupos>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Grupos_Ciudades");

            entity.HasOne(d => d.Generos).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Grupos_Generos");

            entity.HasOne(d => d.Representantes).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.RepresentantesId)
                .HasConstraintName("FK_Grupos_Representantes");
        });

        modelBuilder.Entity<Paises>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plataformas>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Representantes>(entity =>
        {
            entity.Property(e => e.Identificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.mail)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Representantes)
                .HasForeignKey(d => d.CiudadesID)
                .HasConstraintName("FK_Representantes_Ciudades");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VideoClips>(entity =>
        {
            entity.HasOne(d => d.Canciones).WithMany(p => p.VideoClips)
                .HasForeignKey(d => d.CancionesId)
                .HasConstraintName("FK_VideoClips_Canciones");
        });

        modelBuilder.Entity<VideoClipsPlataformas>(entity =>
        {
            entity.Property(e => e.url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Plataformas).WithMany(p => p.VideoClipsPlataformas)
                .HasForeignKey(d => d.PlataformasId)
                .HasConstraintName("FK_VideoClipsPlataformas_Plataformas");

            entity.HasOne(d => d.VideoClips).WithMany(p => p.VideoClipsPlataformas)
                .HasForeignKey(d => d.VideoClipsId)
                .HasConstraintName("FK_VideoClipsPlataformas_VideoClips");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
