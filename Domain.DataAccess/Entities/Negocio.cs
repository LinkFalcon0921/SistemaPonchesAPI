using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Negocio
{
    public ulong Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Verificado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
