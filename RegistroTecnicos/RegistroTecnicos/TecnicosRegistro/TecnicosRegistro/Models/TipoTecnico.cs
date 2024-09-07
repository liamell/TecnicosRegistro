using System.ComponentModel.DataAnnotations;

namespace TecnicosRegistro.Models;

public class TipoTecnico
{
    [Key]
    public int TipoId { get; set; }

	[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se aceptan números ni caracteres especiales")]
	[Required(ErrorMessage = "Descripción Obligatoria")]
	public string? Descripcion { get; set; }

}
