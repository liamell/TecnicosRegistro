using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class Tecnicos
{
	[Key]
	public int TecnicoId { get; set; }

	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
	[Required(ErrorMessage = "Nombre obligatorio")]
	public string? Nombres { get; set; }

	[Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Sueldo obligatorio")]
	public decimal SueldoHora { get; set; }

	[ForeignKey("TipoTecnico")]
	public int TipoId { get; set; }

	public TipoTecnico? TipoTecnico { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public DateTime Fecha { get; set; } = DateTime.Now;

}




