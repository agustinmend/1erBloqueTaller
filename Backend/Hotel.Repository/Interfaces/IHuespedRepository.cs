using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Models.Entidades;

namespace Hotel.Repository.Interfaces
{
    public interface IHuespedRepository
    {
        Task<bool> ExisteDocumentoAsync(string nroDocumento);
        Task<int> CrearHuespedAsync(Huesped huesped);
        Task<IEnumerable<Huesped>> ListarHuespedesAsync();
    }
}
