using System;
using System.Collections.Generic;

namespace prestamoHerramientas.Models;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public string? PreatadoA { get; set; }

    public int? IdMarca { get; set; }

    public int? IdModelo { get; set; }

    public DateTime? FechaIncio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? Estado { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Modelo? IdModeloNavigation { get; set; }
}
