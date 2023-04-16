using System;
using System.Collections.Generic;

namespace prestamoHerramientas.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public string? NombreEquipo { get; set; }

    public int? IdMarca { get; set; }

    public string? NumeroSerie { get; set; }

    public string? Descripcion { get; set; }

    public int? Estado { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }
}
