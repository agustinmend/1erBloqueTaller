using Dapper;
using Hotel.Models.DTOs;
using Hotel.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementaciones
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly string _connectionString;
        public DepartamentoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<ContactoServicioDto>> ObtenerDirectorioContactosAsync()
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    d.Nombre AS NombreServicio,
                    (e.Nombres + ' ' + e.Apellidos) AS NombreEncargado,
                    e.Rol AS RolEncargado,
                    e.Telefono
                FROM Departamento d
                INNER JOIN Empleado e ON d.EncargadoId = e.EmpleadoId
                ORDER BY d.Nombre ASC";
            return await coneccion.QueryAsync<ContactoServicioDto>(query);
        }
    }
}
