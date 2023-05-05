using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Usuario
{
    public ulong Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string Cedula { get; set; } = null!;

    public ulong NegocioId { get; set; }

    public virtual ICollection<Credenciale> Credenciales { get; set; } = new List<Credenciale>();

    public virtual Negocio Negocio { get; set; } = null!;

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    public virtual ICollection<UsuariosRole> UsuariosRoles { get; set; } = new List<UsuariosRole>();
}
