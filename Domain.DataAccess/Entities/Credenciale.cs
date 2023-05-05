using System;
using System.Collections.Generic;

namespace Domain.DataAccess.Entities;

public partial class Credenciale
{
    public ulong Id { get; set; }

    public string Correo { get; set; } = null!;

    public byte[] Pwd { get; set; } = null!;

    public ulong UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
