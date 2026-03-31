using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DTOs
{
    public class CheckInDto
    {
        [Required(ErrorMessage = "El ID de la reserva es obligatorio.")]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "La fecha y hora de ingreso es obligatoria.")]
        public DateTime FechaLlegada { get; set; }

        [Required(ErrorMessage = "Debe haber al menos un huésped para el check-in.")]
        [MinLength(1, ErrorMessage = "Debe registrar al menos al titular.")]
        public List<int> HuespedesIds { get; set; } = new List<int>();
    }
}
