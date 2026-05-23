using NUnit.Framework;
using Hotel.Services.Validadaciones;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelTest
{
    [TestFixture]
    public class ValidadorCheckInTest
    {
        [Test]
        public void ValidarDtos_DebeLanzarInvalidOperation_CuandoEstadoCancelada()
        {
            string Estado = "Cancelada";
            int Capacidad = 1;
            int HabitacionId = 1;
            List<int> HuespedesIds = [1, 2];
            var ex = Assert.Throws<InvalidOperationException>(() =>
                ValidadorCheckIn.ValidarDtos(Estado, Capacidad, HabitacionId, HuespedesIds));
            Assert.That(ex.Message, Does.Contain("No se puede hacer check-in en una reserva cancelada"));
     
        }
    }
}
