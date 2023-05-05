using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Reporte
{
    public ulong Id { get; set; }

    public ulong UsuarioId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
