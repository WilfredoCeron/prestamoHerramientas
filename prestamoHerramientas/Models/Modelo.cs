using System;
using System.Collections.Generic;

namespace prestamoHerramientas.Models;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public string? Serie { get; set; }

    public int? IdMarca { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
