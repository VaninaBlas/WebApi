using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Rol
{
    public Guid Idrol { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Habilitado { get; set; }

    public DateTime? Fechacreacion { get; set; }

}
