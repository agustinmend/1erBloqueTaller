using NUnit.Framework;
using Moq;
using Hotel.Services.Implementaciones;
using Hotel.Repository.Interfaces;
using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
using Hotel.Models.Enums;
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
                TipoHabitacion = TipoHabitacionEnum.Simple,
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

        [Test]
        public void CrearReservaAsync_FechasIncongruentes_LanzaArgumentException()
        {
            var mockRepo = new Mock<IReservaRepository>();
            var service = new ReservaService(mockRepo.Object); 
            
            var dtoInvalido = new CrearReservaDto
            {
                TitularId = 1,
                TipoHabitacion = TipoHabitacionEnum.Simple,
                CantidadPersonas = 1,
                FechaInicio = DateTime.Today,
                FechaFin = DateTime.Today.AddDays(-1)
            };

            var excepcion = Assert.ThrowsAsync<ArgumentException>(() => 
                service.CrearReservaAsync(dtoInvalido));
                
            Assert.That(excepcion.Message, Is.EqualTo("La fecha de salida debe ser estrictamente posterior a la fecha de ingreso."));   
            mockRepo.Verify(r => r.CrearReservaAsync(It.IsAny<Reserva>(), It.IsAny<ReservaHabitacion>()), Times.Never);
        }

        [Test]
        public async Task CrearReservaAsync_DisponibilidadConfirmada_RegistraReservaYRetornaId()
        {
            var mockRepo = new Mock<IReservaRepository>();
            var service = new ReservaService(mockRepo.Object);            
            var dtoValido = new CrearReservaDto
            {
                TitularId = 1,
                TipoHabitacion = TipoHabitacionEnum.Simple,
                CantidadPersonas = 1,
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(3),
                Estado = "confirmada"
            };

            mockRepo.Setup(r => r.BuscarHabitacionDisponibleAsync(
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(101);

            mockRepo.Setup(r => r.CrearReservaAsync(It.IsAny<Reserva>(), It.IsAny<ReservaHabitacion>()))
                .ReturnsAsync(5);

            var resultadoId = await service.CrearReservaAsync(dtoValido);
            Assert.That(resultadoId, Is.EqualTo(5));
            mockRepo.Verify(r => r.CrearReservaAsync(It.IsAny<Reserva>(), It.IsAny<ReservaHabitacion>()), Times.Once);
        }
    }
}