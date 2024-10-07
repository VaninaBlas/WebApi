using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NombreUsuario{ get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public bool Habilitado { get; set; }

    public DateTime? Fechacreacion { get; set; }

}
