using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.DTOs
{
    public class CrearReservaDto : IValidatableObject
    {
        [Required(ErrorMessage = "El Titular es obligatorio.")]
        public int TitularId { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de habitación.")]
        public string TipoHabitacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
        public string Estado { get; set; } = "Confirmada";

        [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
        [Range(1, 20, ErrorMessage = "La cantidad de personas debe ser mayor a 0.")]
        public int CantidadPersonas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaFin <= FechaInicio)
            {
                yield return new ValidationResult(
                    "La fecha de salida debe ser estrictamente posterior a la fecha de ingreso.",
                    new[] { nameof(FechaFin) }
                );
            }
            if (FechaInicio < DateTime.Today)
            {
                yield return new ValidationResult(
                    "La fecha de ingreso no puede ser en el pasado",
                    new[] { nameof(FechaInicio) }
                );
            }
        }
    }
}
