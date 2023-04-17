using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prestamoHerramientas.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(50)]
    public string? NombreEquipo { get; set; }


    public int? IdMarca { get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(100)]
    public string? NumeroSerie { get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(250)]
    public string? Descripcion { get; set; }

    public int? Estado { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }
}
