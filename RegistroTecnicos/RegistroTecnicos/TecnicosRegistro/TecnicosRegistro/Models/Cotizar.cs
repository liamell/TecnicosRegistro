﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class Cotizar
{
    [Key]
    public int CotizacionId { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [ForeignKey("ClienteId")]
    public int ClienteId { get; set; }
    public Clientes? Cliente { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string? Observacion { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public decimal Monto { get; set; }

    [ForeignKey("CotizacionId")]
    public ICollection<CotizarDetalles> cotizacionesDetalle { get; set; } = new List<CotizarDetalles>();
    
}
