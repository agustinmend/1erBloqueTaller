using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones.Variaciones
{
    public class HabitacionSuite : IVariacionHabitacion
    {
        public string Tipo => "Suite";
        public int CapacidadBase => 4;
        public decimal PrecioPorNoche => 250m;
        public string ObtenerDescripcion() => "Amplia suite de lujo con sala de estar.";
    }
}
