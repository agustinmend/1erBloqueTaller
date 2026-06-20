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
    }
}