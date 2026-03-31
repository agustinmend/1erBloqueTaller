using Hotel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IEstadiaRepository
    {
        Task<(string Estado, int Capacidad, int HabitacionId)?> ObtenerDatosValidacionCheckInAsync(int reservaId);
        Task<int> RegistrarCheckInTransaccionalAsync(CheckInDto dto, int habitacionId);
        Task<PreCheckInInDto?> ObtenerDatosPreCheckInAsync(int reservaId);
    }
}
