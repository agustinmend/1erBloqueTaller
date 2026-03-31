using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IHuespedService
    {
        Task<int> RegistrarHuespedAsync(CrearHuespedDto dto);
        Task<IEnumerable<Huesped>> ObtenerTodosAsync();
    }
}
