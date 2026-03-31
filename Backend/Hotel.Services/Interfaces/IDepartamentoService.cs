using Hotel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IDepartamentoService
    {
        Task<IEnumerable<ContactoServicioDto>> ObtenerDirectorioAsync();
    }
}
