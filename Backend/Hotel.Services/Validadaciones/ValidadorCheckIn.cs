using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.Services.Validadaciones
{
    public static class ValidadorCheckIn
    {
        public static void ValidarDtos(string Estado, int Capacidad, int HabitacionId, List<int> HuespedesIds)
        {
            if (Estado == "Cancelada") throw new InvalidOperationException("No se puede hacer check-in en una reserva cancelada");
            if (Estado == "Estadía en curso") throw new InvalidOperationException("Esta reserva ya hizo check-in");
            var huespedesUnicos = HuespedesIds.ToHashSet();
            if (huespedesUnicos.Count > Capacidad)
            {
                throw new InvalidOperationException($"La cantidad de huespedes ({huespedesUnicos.Count}) supera la capacidad ({Capacidad})");
            }
        }
    }
}
