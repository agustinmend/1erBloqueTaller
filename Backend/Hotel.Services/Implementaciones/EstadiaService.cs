using Hotel.Models.DTOs;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Implementaciones
{
    public class EstadiaService : IEstadiaService
    {
        private readonly IEstadiaRepository _estadiaRepository;
        public EstadiaService(IEstadiaRepository estadiaRepository)
        {
            _estadiaRepository = estadiaRepository;
        }
        public async Task<int> ProcesarCheckInAsync(CheckInDto dto)
        {
            var datos = await _estadiaRepository.ObtenerDatosValidacionCheckInAsync(dto.ReservaId);
            if(datos == null) throw new KeyNotFoundException("La reserva no existe.");
            if (datos.Value.Estado == "Cancelada") throw new InvalidOperationException("No se puede hacer check-in en una reserva cancelada");
            if (datos.Value.Estado == "Estadía en curso") throw new InvalidOperationException("Esta reserva ya hizo check-in");
            var huespedesUnicos = dto.HuespedesIds.ToHashSet();
            if(huespedesUnicos.Count > datos.Value.Capacidad)
            {
                throw new InvalidOperationException($"La cantidad de huespedes ({huespedesUnicos.Count}) supera la capacidad ({datos.Value.Capacidad})");
            }
            return await _estadiaRepository.RegistrarCheckInTransaccionalAsync(dto, datos.Value.HabitacionId);
        }
        public async Task<PreCheckInInDto> ObtenerDatosPreCheckInAsync(int reservaId)
        {
            var datos = await _estadiaRepository.ObtenerDatosPreCheckInAsync(reservaId);
            if (datos == null) throw new KeyNotFoundException("Reserva no encontrada.");
            return datos;
        }
    }
}
