using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class Tecnicos
{
	[Key]
	public int TecnicoId { get; set; }

	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
	[Required(ErrorMessage = "Nombre obligatorio")]
	public string Nombres { get; set; }

	[Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Sueldo obligatorio")]
	public decimal SueldoHora { get; set; }

	[ForeignKey("TipoTecnico")]
	public int TipoId { get; set; }

	public TipoTecnico? TipoTecnico { get; set; }

	[ForeignKey("Clientes")]
	public int ClienteId { get; set; }

	public Clientes? Clientes { get; set; }

	[ForeignKey("Trabajo")]
	public int TrabajoId { get; set; }

	public Trabajo? Trabajo { get; set; }


	[ForeignKey("Prioridad")]

	public int PrioridadId { get; set; }

	public Prioridad? Prioridad { get; set; }

}




