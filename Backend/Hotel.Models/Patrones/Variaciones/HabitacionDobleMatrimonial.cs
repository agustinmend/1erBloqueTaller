using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones.Variaciones
{
    public class HabitacionDobleMatrimonial : IVariacionHabitacion
    {
        public string Tipo => "Doble Matrimonial";
        public int CapacidadBase => 2;
        public decimal PrecioPorNoche => 90m;
        public string ObtenerDescripcion() => "Habitación con cama matrimonial ideal para parejas.";
    }
}
