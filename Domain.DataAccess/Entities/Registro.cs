using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Registro
{
    public ulong Id { get; set; }

    public string Cedula { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
