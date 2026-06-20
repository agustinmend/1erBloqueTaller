using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Models.Enums;

namespace Hotel.Models.Patrones
{
    public static class VariacionHabitacionFactory
    {
        public static IVariacionHabitacion CrearVariacion(TipoHabitacionEnum tipoHabitacion)
        {
            return tipoHabitacion switch
            {
                TipoHabitacionEnum.Simple => new Variaciones.HabitacionSimple(),
                TipoHabitacionEnum.Suite => new Variaciones.HabitacionSuite(),
                TipoHabitacionEnum.DobleMatrimonial => new Variaciones.HabitacionDobleCamasIndividuales(),
                TipoHabitacionEnum.DobleIndividual => new Variaciones.HabitacionDobleMatrimonial(),
                _ => throw new ArgumentException($"El tipo de habitacion '{tipoHabitacion}' no es valido")
            };
        }
    }
}
