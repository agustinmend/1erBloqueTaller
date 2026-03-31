using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IReservaRepository
    {
        Task<int?> BuscarHabitacionDisponibleAsync(string tipo, int cantidadPersonas, DateTime fechaInicio, DateTime fechaFin);
        Task<int> CrearReservaAsync(Reserva reserva, ReservaHabitacion detalle);
        Task<IEnumerable<AgendaReservaDto>> ObtenerAgendaReservasAsync(string? terminoBusqueda = null);
    }
}
