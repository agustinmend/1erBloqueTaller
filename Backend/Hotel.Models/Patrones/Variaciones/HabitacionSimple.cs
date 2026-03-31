using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones.Variaciones
{
    public class HabitacionSimple : IVariacionHabitacion
    {
        public string Tipo => "Simple";
        public int CapacidadBase => 1;
        public decimal PrecioPorNoche => 50m;
        public string ObtenerDescripcion() => "Habitación básica para una persona.";
    }
}
