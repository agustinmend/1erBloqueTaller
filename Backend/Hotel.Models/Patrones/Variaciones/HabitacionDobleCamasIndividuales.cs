using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones.Variaciones
{
    public class HabitacionDobleCamasIndividuales : IVariacionHabitacion
    {
        public string Tipo => "Doble con camas individuales";
        public int CapacidadBase => 2;
        public decimal PrecioPorNoche => 80m;
        public string ObtenerDescripcion() => "Habitación doble  con dos camas individuales";
    }
}
