using NUnit.Framework;
using Moq;
using Hotel.Services.Implementaciones;
using Hotel.Repository.Interfaces;
using Hotel.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace HotelTest
{
    [TestFixture]
    public class ReservaServiceTest
    {
        private Mock<IReservaRepository> _reservaRepositoryMock;
        private ReservaService _reservaService;

        [SetUp]
        public void Setup()
        {
            _reservaRepositoryMock = new Mock<IReservaRepository>();
            _reservaService = new ReservaService(_reservaRepositoryMock.Object);
        }

        [Test]
        public void CrearReservaAsync_DebeLanzarExcepcion_CuandoNoHayHabitacionFisicaDisponible()
        {
            var dto = new CrearReservaDto
            {
                TipoHabitacion = "Simple",
                CantidadPersonas = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(2),
                TitularId = 1
            };

            _reservaRepositoryMock.Setup(repo => repo.BuscarHabitacionDisponibleAsync(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
                .ReturnsAsync((int?)null);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _reservaService.CrearReservaAsync(dto));
            Assert.That(ex.Message, Does.Contain("No hay habitaciones fisicas disponibles"));
            _reservaRepositoryMock.Verify(repo => repo.CrearReservaAsync(It.IsAny<Hotel.Models.Entidades.Reserva>(), It.IsAny<Hotel.Models.Entidades.ReservaHabitacion>()), Times.Never);
        }
    }
}