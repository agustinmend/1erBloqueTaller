using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DTOs
{
    public class PreCheckInInDto
    {
        public int ReservaId { get; set; }
        public int TitularId { get; set; }
        public string TitularNombreCompleto { get; set; } = string.Empty;
        public string TitularDocumento { get; set; } = string.Empty;
        public string HabitacionAsignada { get; set; } = string.Empty;
        public int Capacidad { get; set; }
    }
}
