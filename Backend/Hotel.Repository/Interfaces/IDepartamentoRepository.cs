using Hotel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<ContactoServicioDto>> ObtenerDirectorioContactosAsync();
    }
}
