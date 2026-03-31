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
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }
        public async Task<IEnumerable<ContactoServicioDto>> ObtenerDirectorioAsync()
        {
            return await _departamentoRepository.ObtenerDirectorioContactosAsync();
        }
    }
}
