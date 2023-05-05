using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class UsuariosRole
{
    public ulong Id { get; set; }

    public ulong UsuarioId { get; set; }

    public ulong RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
