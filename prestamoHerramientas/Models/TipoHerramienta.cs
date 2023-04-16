using System;
using System.Collections.Generic;

namespace prestamoHerramientas.Models;

public partial class TipoHerramienta
{
    public int IdTipoHerramienta { get; set; }

    public string? TipoHerramienta1 { get; set; }

    public virtual ICollection<Marca> Marcas { get; set; } = new List<Marca>();
}
