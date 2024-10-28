using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicosRegistro.Models;

public class Trabajo
{
    [Key]
    public int TrabajoId { get; set; }
   
    
    public string? Descripcion { get; set; }
	
	[Range(0.01, float.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
	[Required(ErrorMessage = "Monto obligtorio")]

	public decimal Monto { get; set; }

	[ForeignKey("TrabajoId")]
	public ICollection<TrabajosDetalle> TrabajosDetalle { get; set; } = new List<TrabajosDetalle>();

    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }
    public Clientes Cliente { get; set; }

    [ForeignKey("Tecnicos")]
    public int TecnicoId { get; set; }
    public Tecnicos Tecnico { get; set; }

    [ForeignKey("Prioridad")]
    public int PrioridadId { get; set; }
    public Prioridad Prioridad { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public DateTime Fecha { get; set; } = DateTime.Now;


}
