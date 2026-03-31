using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Entidades
{
    public class Habitacion
    {
        public int HabitacionId { get; set; }
        public string NroHabitacion { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
    }
}
