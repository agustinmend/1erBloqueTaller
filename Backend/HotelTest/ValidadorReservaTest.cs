using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Services.Validadaciones;

namespace HotelTest
{
    [TestFixture]
    class ValidadorReservaTest
    {
        [Test]
        public void ValidarCapacidad_CantidadExcedeElMaximo_LanzaInvalidOperationException()
        {
            int personas = 5;
            int capacidadMaxima = 2;
            string tipo = "Simple";

            var ex = Assert.Throws<InvalidOperationException>(() =>
                ValidadorReserva.ValidarCapacidad(personas, capacidadMaxima, tipo));

            Assert.That(ex.Message, Does.Contain("supera la capacidad maxima"));
        }
        
        [Test]
        public void ValidarCapacidad_CantidadPermitida_NoLanzaExcepcion()
        {
            Assert.DoesNotThrow(() => ValidadorReserva.ValidarCapacidad(2, 2, "Doble"));
        }
        
        [Test]
        public void CalcularPrecioTotal_FechasCorrectas_CalculaMontoExacto()
        {
            decimal precioNoche = 100m;
            DateTime inicio = new DateTime(2026, 1, 1);
            DateTime fin = new DateTime(2026, 1, 4);
            decimal resultado = ValidadorReserva.CalcularPrecioTotal(precioNoche, inicio, fin);
            Assert.That(resultado, Is.EqualTo(300m));
        }

        [Test]
        public void CalcularPrecioTotal_FechasInvertidas_LanzaExcepcion()
        {
            decimal precioNoche = 100m;
            DateTime inicio = new DateTime(2026, 1, 5);
            DateTime fin = new DateTime(2026, 1, 1);
            var ex = Assert.Throws<InvalidOperationException>(() => ValidadorReserva.CalcularPrecioTotal(precioNoche, inicio, fin)); 
            Assert.That(ex.Message, Does.Contain("menor a la fecha de inicio"));
        }
    }
}
