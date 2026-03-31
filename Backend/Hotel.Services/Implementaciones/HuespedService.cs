using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Implementaciones
{
    public class HuespedService : IHuespedService
    {
        private readonly IHuespedRepository _huespedRepository;
        public HuespedService(IHuespedRepository huespedRepository)
        {
            _huespedRepository = huespedRepository;
        }
        public async Task<int> RegistrarHuespedAsync(CrearHuespedDto dto)
        {
            bool existe = await _huespedRepository.ExisteDocumentoAsync(dto.NroDocumentoIdentidad);
            if (existe) throw new InvalidOperationException("Ya existe un Huesped con este Documento de identidad");
            var huesped = new Huesped
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                FechaNacimiento = dto.FechaNacimiento,
                NroDocumentoIdentidad = dto.NroDocumentoIdentidad
            };
            return await _huespedRepository.CrearHuespedAsync(huesped);
        }
        public async Task<IEnumerable<Huesped>> ObtenerTodosAsync()
        {
            return await _huespedRepository.ListarHuespedesAsync();
        }
    }
}
