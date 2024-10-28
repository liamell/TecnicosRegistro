using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class TrabajosDetalle
{
	[Key]

	public int DetalleId { get; set; }

	[ForeignKey("TrabajoId")]
	public int TrabajoId { get; set; }
	public Trabajo? Trabajo { get; set; }

	[ForeignKey("ArticuloId")]
	public int ArticuloId { get; set; }
	public Articulo? Articulo { get; set; }

	[Required(ErrorMessage = "Campo obligatorio")]
	public int Cantidad { get; set; }

	[Required(ErrorMessage = "Campo obligatorio")]
	public double Precio { get; set; }

	[Required(ErrorMessage = "Campo obligatorio")]
	public double Costo { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public DateTime Fecha { get; set; } = DateTime.Now;

}
