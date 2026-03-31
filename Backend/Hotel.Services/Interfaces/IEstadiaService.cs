using Hotel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IEstadiaService
    {
        Task<int> ProcesarCheckInAsync(CheckInDto dto);
        Task<PreCheckInInDto> ObtenerDatosPreCheckInAsync(int reservaId);
    }
}
