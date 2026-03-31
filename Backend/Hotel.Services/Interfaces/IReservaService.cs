using Hotel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IReservaService
    {
        Task<int> CrearReservaAsync(CrearReservaDto dto);
        Task<IEnumerable<AgendaReservaDto>> ObtenerAgendaAsync(string? terminoBusqueda = null);
    }
}
