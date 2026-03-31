using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Patrones
{
    public static class VariacionHabitacionFactory
    {
        public static IVariacionHabitacion CrearVariacion(string tipoHabitacion)
        {
            return tipoHabitacion switch
            {
                "Simple" => new Variaciones.HabitacionSimple(),
                "Suite" => new Variaciones.HabitacionSuite(),
                "Doble con camas individuales" => new Variaciones.HabitacionDobleCamasIndividuales(),
                "Doble matrimonial" => new Variaciones.HabitacionDobleMatrimonial(),
                _ => throw new ArgumentException($"El tipo de habitacion '{tipoHabitacion}' no es valido")
            };
        }
    }
}
