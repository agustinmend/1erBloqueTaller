using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones
{
    public interface IVariacionHabitacion
    {
        string Tipo { get; }
        int CapacidadBase { get; }
        decimal PrecioPorNoche { get; }
        string ObtenerDescripcion();
    }
}
