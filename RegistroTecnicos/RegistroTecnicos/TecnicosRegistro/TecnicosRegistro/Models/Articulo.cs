using System.ComponentModel.DataAnnotations;

namespace TecnicosRegistro.Models;

public class Articulo
{	
	[Key]

	public int ArticuloId { get; set; }

	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
	[Required(ErrorMessage = "Campo obligatorio")]

	public string Descripcion { get; set; }

	[Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Costo obligatorio")]

	public decimal Costo { get; set; }

	[Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Precio obligatorio")]

	public decimal Precio { get; set; }

	[Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Campo obligatorio")]

	public int Existencia { get; set; }

}
