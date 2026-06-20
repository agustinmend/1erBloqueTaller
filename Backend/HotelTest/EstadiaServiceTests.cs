using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Services.Implementaciones;
using Hotel.Repository.Interfaces;
using Hotel.Models.DTOs;

namespace Hotel.Tests.Services
{
    [TestFixture]
    public class EstadiaServiceTests
    {
        [Test]
        public void ProcesarCheckInAsync_ReservaInexistente_LanzaKeyNotFoundException()
        {
            var mockRepo = new Mock<IEstadiaRepository>();
            (string Estado, int Capacidad, int HabitacionId)? reservaNula = null;
            mockRepo.Setup(r => r.ObtenerDatosValidacionCheckInAsync(It.IsAny<int>()))
                    .ReturnsAsync(reservaNula);

            var service = new EstadiaService(mockRepo.Object);
            var dtoInvalido = new CheckInDto
            {
                ReservaId = 999,
                FechaLlegada = DateTime.Now,
                HuespedesIds = new List<int> { 1 }
            };

            var excepcion = Assert.ThrowsAsync<KeyNotFoundException>(() => 
                service.ProcesarCheckInAsync(dtoInvalido));
                
            Assert.That(excepcion.Message, Is.EqualTo("La reserva no existe."));
            mockRepo.Verify(r => r.RegistrarCheckInTransaccionalAsync(It.IsAny<CheckInDto>(), It.IsAny<int>()), Times.Never);
        }
    }
}