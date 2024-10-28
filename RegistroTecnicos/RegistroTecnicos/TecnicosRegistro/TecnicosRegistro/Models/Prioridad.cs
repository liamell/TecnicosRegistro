using System.ComponentModel.DataAnnotations;

namespace TecnicosRegistro.Models
{
    public class Prioridad
    {
        [Key]
        public int PrioridadId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El tiempo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El tiempo debe ser un valor positivo.")]
        public int Tiempo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;

    }
}
