using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
using Hotel.Models.Patrones;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfaces;
using Hotel.Services.Validadaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Implementaciones
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }
        public async Task<int> CrearReservaAsync(CrearReservaDto dto)
        {
            IVariacionHabitacion variacion = VariacionHabitacionFactory.CrearVariacion(dto.TipoHabitacion);
            ValidadorReserva.ValidarCapacidad(dto.CantidadPersonas, variacion.CapacidadBase, variacion.Tipo);
            int? HabitacionIdAsignada = await _reservaRepository.BuscarHabitacionDisponibleAsync(
                variacion.Tipo,
                dto.CantidadPersonas,
                dto.FechaInicio,
                dto.FechaFin);
            if(!HabitacionIdAsignada.HasValue)
            {
                throw new InvalidOperationException($"No hay habitaciones fisicas disponibles de tipo {variacion.Tipo} para las fechas y capacidad solicitada");
            }
            decimal precioTotalCobrado = ValidadorReserva.CalcularPrecioTotal(variacion.PrecioPorNoche, dto.FechaInicio, dto.FechaFin);

            var reserva = new Reserva
            {
                TitularId = dto.TitularId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = "Confirmada"
            };
            var detalle = new ReservaHabitacion
            {
                HabitacionId = HabitacionIdAsignada.Value,
                PrecioCobrado = precioTotalCobrado
            };
            return await _reservaRepository.CrearReservaAsync(reserva, detalle);
        }
        public async Task<IEnumerable<AgendaReservaDto>> ObtenerAgendaAsync(string? terminoBusqueda = null)
        {
            return await _reservaRepository.ObtenerAgendaReservasAsync(terminoBusqueda);
        }
    }
}
