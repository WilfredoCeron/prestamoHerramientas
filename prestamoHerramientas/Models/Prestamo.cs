using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prestamoHerramientas.Models;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
    [StringLength(50)]
    public string? PreatadoA { get; set; }

    public int? IdMarca { get; set; }

    public int? IdModelo { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public DateTime? FechaIncio { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public DateTime? FechaFin { get; set; }

    public int? Estado { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Modelo? IdModeloNavigation { get; set; }
}
