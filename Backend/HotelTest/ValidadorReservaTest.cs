using NUnit.Framework;
using Hotel.Services.Validadaciones;
using System;

namespace HotelTest
{
    [TestFixture]
    public class ValidadorReservaTest
    {
        [Test]
        public void ValidarCapacidad_DebeLanzarExcepcion_CuandoSuperaCapacidadMaxima()
        {
            int cantidadPersonas = 5;
            int capacidadMaxima = 4;
            string tipoHabitacion = "Familiar";
            var ex = Assert.Throws<InvalidOperationException>(() =>
                ValidadorReserva.ValidarCapacidad(cantidadPersonas, capacidadMaxima, tipoHabitacion));

            Assert.That(ex.Message, Does.Contain("supera la capacidad maxima"));
        }

        [Test]
        public void CalcularPrecioTotal_DebeRetornarMontoCorrecto_ParaFechasValidas()
        {
            decimal precioPorNoche = 100m;
            DateTime fechaInicio = new DateTime(2026, 10, 1);
            DateTime fechaFin = new DateTime(2026, 10, 6);
            decimal precioTotal = ValidadorReserva.CalcularPrecioTotal(precioPorNoche, fechaInicio, fechaFin);
            Assert.AreEqual(500m, precioTotal);
        }

        [Test]
        public void CalcularPrecioTotal_DebeLanzarExcepcion_CuandoFechaFinEsMenorAInicio()
        {
            // Arrange
            decimal precioPorNoche = 100m;
            DateTime fechaInicio = new DateTime(2026, 10, 5);
            DateTime fechaFin = new DateTime(2026, 10, 1);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                ValidadorReserva.CalcularPrecioTotal(precioPorNoche, fechaInicio, fechaFin));

            Assert.That(ex.Message, Does.Contain("es menor a la fecha de inicio"));
        }
    }
}