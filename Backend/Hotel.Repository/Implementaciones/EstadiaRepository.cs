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
    public class EstadiaRepository : IEstadiaRepository
    {
        private readonly string _connectionString;
        public EstadiaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<(string Estado, int Capacidad, int HabitacionId)?> ObtenerDatosValidacionCheckInAsync(int reservaId)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                SELECT r.Estado, h.Capacidad, h.HabitacionId
                FROM Reserva r
                INNER JOIN ReservaHabitacion rh ON r.ReservaId = rh.ReservaId
                INNER JOIN Habitacion h ON rh.HabitacionId = h.HabitacionId
                WHERE r.ReservaId = @ReservaId";
            return await coneccion.QuerySingleOrDefaultAsync<(string, int, int)>(query, new { ReservaId = reservaId });
        }
        public async Task<int> RegistrarCheckInTransaccionalAsync(CheckInDto dto, int habitacionId)
        {
            using var coneccion = new SqlConnection(_connectionString);
            coneccion.Open();
            using var transaction = coneccion.BeginTransaction();
            try
            {
                string queryEstadia = @"
                    INSERT INTO Estadia (ReservaId, HabitacionId, FechaLlegada) 
                    VALUES (@ReservaId, @HabitacionId, @FechaLlegada);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int nuevaEstadiaId = await coneccion.ExecuteScalarAsync<int>(queryEstadia, new { dto.ReservaId, HabitacionId = habitacionId, dto.FechaLlegada }, transaction);
                string queryHuespedes = @"
                    INSERT INTO EstadiaHuesped (EstadiaId, HuespedId) 
                    VALUES (@EstadiaId, @HuespedId);";

                foreach(var huespedId in dto.HuespedesIds)
                {
                    await coneccion.ExecuteAsync(queryHuespedes, new { EstadiaId = nuevaEstadiaId, HuespedId = huespedId }, transaction);
                }
                string queryActualizarReserva = @"
                    UPDATE Reserva 
                    SET Estado = 'Estadía en curso' 
                    WHERE ReservaId = @ReservaId;";

                await coneccion.ExecuteAsync(queryActualizarReserva, new { dto.ReservaId }, transaction);
                transaction.Commit();
                return nuevaEstadiaId;
            }
            catch(Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<PreCheckInInDto?> ObtenerDatosPreCheckInAsync(int reservaId)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    r.ReservaId,
                    h.HuespedId AS TitularId,
                    (h.Nombres + ' ' + h.Apellidos) AS TitularNombreCompleto,
                    h.NroDocumentoIdentidad AS TitularDocumento,
                    (hab.NroHabitacion + ' - ' + hab.Tipo) AS HabitacionAsignada,
                    hab.Capacidad
                FROM Reserva r
                INNER JOIN Huesped h ON r.TitularId = h.HuespedId
                INNER JOIN ReservaHabitacion rh ON r.ReservaId = rh.ReservaId
                INNER JOIN Habitacion hab ON rh.HabitacionId = hab.HabitacionId
                WHERE r.ReservaId = @ReservaId";

            return await coneccion.QueryFirstOrDefaultAsync<PreCheckInInDto>(query, new { ReservaId = reservaId });
        }

    }
}
