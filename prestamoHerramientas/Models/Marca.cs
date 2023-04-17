//using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prestamoHerramientas.Models;

public partial class Marca
{
    
    public int IdMarca { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(100)]
    public string? NombreMarca { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public int? IdTipoHerramienta { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0, 100, ErrorMessage = "Rango no valido")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Entrada Invalida, debe de ser un numero con 2 decimales")]
    public decimal? Exactitud { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual TipoHerramienta? IdTipoHerramientaNavigation { get; set; }

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
