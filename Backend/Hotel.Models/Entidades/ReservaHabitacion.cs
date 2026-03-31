using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Entidades
{
    public class ReservaHabitacion
    {
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public decimal PrecioCobrado { get; set; }
    }
}
