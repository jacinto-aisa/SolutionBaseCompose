namespace MvcWebMusica2.Models;

public partial class Empleados
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public int? RolesId { get; set; }

    public virtual Roles? Roles { get; set; }
}
