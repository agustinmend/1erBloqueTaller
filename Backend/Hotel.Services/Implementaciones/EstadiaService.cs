using Hotel.Models.DTOs;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Services.Validadaciones;

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
            ValidadorCheckIn.ValidarDtos(datos.Value.Estado, datos.Value.Capacidad, datos.Value.HabitacionId, dto.HuespedesIds);
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
