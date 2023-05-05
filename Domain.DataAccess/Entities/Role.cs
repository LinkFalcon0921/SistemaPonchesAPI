using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Role
{
    public ulong Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<UsuariosRole> UsuariosRoles { get; set; } = new List<UsuariosRole>();
}
