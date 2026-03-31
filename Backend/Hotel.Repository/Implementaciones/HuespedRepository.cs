using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Hotel.Repository.Interfaces;
using Hotel.Models.Entidades;

namespace Hotel.Repository.Implementaciones
{
    public class HuespedRepository : IHuespedRepository
    {
        private readonly string _connectionString;
        public HuespedRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> ExisteDocumentoAsync(string nroDocumento)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = "SELECT COUNT(1) FROM Huesped WHERE NroDocumentoIdentidad = @NroDocumento";
            var contador = await coneccion.ExecuteScalarAsync<int>(query, new { NroDocumento = nroDocumento });
            return contador > 0;
        }

        public async Task<int> CrearHuespedAsync(Huesped huesped)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                INSERT INTO Huesped (Nombres, Apellidos, FechaNacimiento, NroDocumentoIdentidad)
                VALUES (@Nombres, @Apellidos, @FechaNacimiento, @NroDocumentoIdentidad)
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var nuevoId = await coneccion.ExecuteScalarAsync<int>(query, huesped);
            return nuevoId;
        }
        public async Task<IEnumerable<Huesped>> ListarHuespedesAsync()
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = "SELECT HuespedId, Nombres, Apellidos, NroDocumentoIdentidad FROM Huesped";
            return await coneccion.QueryAsync<Huesped>(query);
        }
    }
}
