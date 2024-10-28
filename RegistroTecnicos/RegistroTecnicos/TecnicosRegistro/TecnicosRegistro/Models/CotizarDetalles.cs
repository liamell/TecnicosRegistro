using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class CotizarDetalles
{

    [Key]
    public int DetalleId { get; set; }

    [ForeignKey("Cotizar")]
    public int CotizacionId { get; set; }
    public Cotizar? Cotizar { get; set; }

    [ForeignKey("Articulo")]
    public int ArticuloId { get; set; }
    public Articulo? Articulo { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public decimal Precio { get; set; }
}
