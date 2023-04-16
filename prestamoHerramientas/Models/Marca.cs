using System;
using System.Collections.Generic;

namespace prestamoHerramientas.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? NombreMarca { get; set; }

    public string? Descripcion { get; set; }

    public int? IdTipoHerramienta { get; set; }

    public decimal? Exactitud { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual TipoHerramienta? IdTipoHerramientaNavigation { get; set; }

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
