using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DTOs
{
    public class AgendaReservaDto
    {
        public int ReservaId { get; set; }
        public string HuespedTitular { get; set; } = string.Empty;
        public string HabitacionAsignada { get; set; } = string.Empty;
        public string TipoHabitacion { get; set; } = string.Empty;
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal PrecioCongelado { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
