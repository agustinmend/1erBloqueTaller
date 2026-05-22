using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Validadaciones
{
    public class ValidadorReserva
    {
        public static void ValidarCapacidad(int cantidadPersonas, int capacidadMaxima, string tipoHabitacion)
        {
            if (cantidadPersonas > capacidadMaxima)
            {
                throw new InvalidOperationException($"La cantidad de personas ({cantidadPersonas}) supera la capacidad maxima de la habitacion tipo {tipoHabitacion} ({capacidadMaxima})");
            }
        }

        public static decimal CalcularPrecioTotal(decimal precioPorNoche, DateTime fechaInicio, DateTime fechaFin)
        {
            if(fechaFin < fechaInicio)
            {
                throw new InvalidOperationException($"La fecha de fin ({fechaFin}) es menor a la fecha de inicio ({fechaInicio})");
            }
            int cantidadNoches = (fechaFin - fechaInicio).Days;

            if (cantidadNoches <= 0) cantidadNoches = 1;

            return precioPorNoche * cantidadNoches;
        }
    }
}
