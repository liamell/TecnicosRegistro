using System.ComponentModel.DataAnnotations;

namespace TecnicosRegistro.Models;

public class Trabajo
{
    [Key]
    public int TrabajoId { get; set; }
    public DateTime Fecha { get; set; }
    public int ClienteId { get; set; }
    public int TecnicoId { get; set; }
	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
	[Required(ErrorMessage = "Nombre obligatorio")]

	public string? Descripcion { get; set; }

	[Range(0.01, float.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Monto obligtorio")]

	public decimal Monto { get; set; }
}
