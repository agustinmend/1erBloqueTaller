using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using Hotel.Services.Implementaciones;
using Hotel.Repository.Interfaces;
using Hotel.Models.DTOs;

namespace Hotel.Tests.Services
{
    [TestFixture]
    public class HuespedServiceTests
    {
        [Test]
        public void RegistrarHuespedAsync_NombreVacio_LanzaArgumentException()
        {
            var mockRepo = new Mock<IHuespedRepository>();
            var service = new HuespedService(mockRepo.Object);
            var dtoInvalido = new CrearHuespedDto 
            { 
                Nombres = "",
                Apellidos = "Perez", 
                NroDocumentoIdentidad = "1234567" 
            };

            var excepcion = Assert.ThrowsAsync<ArgumentException>(() => 
                service.RegistrarHuespedAsync(dtoInvalido));
                
            Assert.That(excepcion.Message, Is.EqualTo("El nombre del huésped es obligatorio."));
            
            mockRepo.Verify(r => r.CrearHuespedAsync(It.IsAny<Hotel.Models.Entidades.Huesped>()), Times.Never);
        }

        [Test]
        public async Task RegistrarHuespedAsync_DatosCompletosYValidos_RetornaNuevoId()
        {
            var mockRepo = new Mock<IHuespedRepository>();
            var service = new HuespedService(mockRepo.Object);
            
            var dtoValido = new CrearHuespedDto 
            { 
                Nombres = "Jose Agustin", 
                Apellidos = "Mendoza", 
                FechaNacimiento = new DateTime(2000, 1, 1),
                NroDocumentoIdentidad = "1234567" 
            };
            mockRepo.Setup(r => r.ExisteDocumentoAsync(dtoValido.NroDocumentoIdentidad))
                    .ReturnsAsync(false);
            
            mockRepo.Setup(r => r.CrearHuespedAsync(It.IsAny<Hotel.Models.Entidades.Huesped>()))
                    .ReturnsAsync(10);

            var resultadoId = await service.RegistrarHuespedAsync(dtoValido);

            Assert.That(resultadoId, Is.EqualTo(10));
            mockRepo.Verify(r => r.CrearHuespedAsync(It.IsAny<Hotel.Models.Entidades.Huesped>()), Times.Once);
        }
    }
}