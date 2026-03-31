using Dapper;
using Hotel.Models.DTOs;
using Hotel.Models.Entidades;
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
    public class ReservaRepository : IReservaRepository
    {
        private readonly string _connectionString;
        public ReservaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int?> BuscarHabitacionDisponibleAsync(string tipo, int cantidadPersonas, DateTime fechaInicio, DateTime fechaFin)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                SELECT TOP 1 h.HabitacionId
                FROM Habitacion h
                WHERE h.Tipo = @Tipo
                  AND h.Capacidad >= @CantidadPersonas
                  AND h.Estado != 'Mantenimiento'
                  AND h.HabitacionId NOT IN (
                      SELECT rh.HabitacionId
                      FROM ReservaHabitacion rh
                      INNER JOIN Reserva r ON rh.ReservaId = r.ReservaId
                      WHERE r.Estado != 'Cancelada'
                        AND (@FechaInicio < r.FechaFin AND @FechaFin > r.FechaInicio)
                  )";
            return await coneccion.QueryFirstOrDefaultAsync<int?>(query, new
            {
                Tipo = tipo,
                CantidadPersonas = cantidadPersonas,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            });
        }
        public async Task<int> CrearReservaAsync(Reserva reserva, ReservaHabitacion detalle)
        {
            using var coneccion = new SqlConnection(_connectionString);
            coneccion.Open();
            using var transaccion = coneccion.BeginTransaction();
            try
            {
                string queryReserva = @"
                    INSERT INTO Reserva (TitularId, FechaInicio, FechaFin, Estado) 
                    VALUES (@TitularId, @FechaInicio, @FechaFin, @Estado);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int nuevaReservaId = await coneccion.ExecuteScalarAsync<int>(queryReserva, reserva, transaccion);
                detalle.ReservaId = nuevaReservaId;
                string queryDetalle = @"
                    INSERT INTO ReservaHabitacion (ReservaId, HabitacionId, PrecioCobrado) 
                    VALUES (@ReservaId, @HabitacionId, @PrecioCobrado);";
                await coneccion.ExecuteAsync(queryDetalle, detalle, transaccion);
                transaccion.Commit();
                return nuevaReservaId;
            }
            catch(Exception)
            {
                transaccion.Rollback();
                throw;
            }
        }
        public async Task<IEnumerable<AgendaReservaDto>> ObtenerAgendaReservasAsync(string? terminoBusqueda = null)
        {
            using var coneccion = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    r.ReservaId,
                    (h.Nombres + ' ' + h.Apellidos) AS HuespedTitular,
                    hab.NroHabitacion AS HabitacionAsignada,
                    hab.Tipo AS TipoHabitacion,
                    r.FechaInicio AS FechaLlegada,
                    r.FechaFin AS FechaSalida,
                    rh.PrecioCobrado AS PrecioCongelado,
                    r.Estado
                FROM Reserva r
                INNER JOIN Huesped h ON r.TitularId = h.HuespedId
                INNER JOIN ReservaHabitacion rh ON r.ReservaId = rh.ReservaId
                INNER JOIN Habitacion hab ON rh.HabitacionId = hab.HabitacionId
                WHERE r.Estado IN ('Confirmada', 'Estadía en curso')
                    AND (
                        @Termino IS NULL OR 
                        h.Nombres LIKE '%' + @Termino + '%' OR 
                        h.Apellidos LIKE '%' + @Termino + '%' OR 
                        h.NroDocumentoIdentidad LIKE '%' + @Termino + '%'
                        )
                ORDER BY r.FechaInicio ASC";
            string? terminoProcesado = string.IsNullOrWhiteSpace(terminoBusqueda) ? null : terminoBusqueda.Trim();
            return await coneccion.QueryAsync<AgendaReservaDto>(query, new {Termino = terminoProcesado});
        }
    }
}
