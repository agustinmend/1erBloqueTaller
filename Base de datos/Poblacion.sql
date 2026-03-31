use Hotel

INSERT INTO Habitacion (NroHabitacion, Capacidad, Tipo, Estado, PrecioBase) VALUES
('101', 1, 'Simple', 'Activo', 50.00),
('102', 2, 'Simple', 'Mantenimiento', 50.00),
('201', 2, 'Doble con camas individuales', 'Activo', 80.00),
('202', 2, 'Doble matrimonial', 'Activo', 90.00),
('301', 2, 'Doble matrimonial', 'Inactivo', 90.00),
('500', 4, 'Suite', 'Activo', 250.00),
('103', 1, 'Simple', 'Inactivo', 50.00),
('401', 2, 'Doble con camas individuales', 'Mantenimiento', 80.00);

INSERT INTO Huesped (Nombres, Apellidos, FechaNacimiento, NroDocumentoIdentidad) VALUES
('Carlos', 'Mendoza', '1985-04-12', '12345678'),
('Lucia', 'Fernandez', '1990-08-25', '87654321'),
('Miguel', 'Angulo', '2005-11-05', '55556666');

INSERT INTO Empleado (Nombres, Apellidos, Telefono, Rol) VALUES
('Ana', 'Gomez', '77711122', 'Gerente'),
('Roberto', 'Perez', '77733344', 'Recepcionista'),
('Luis', 'Torres', '77755566', 'Mantenimiento');

INSERT INTO Departamento (Nombre, EncargadoId) VALUES
('Administracion', 1),
('Recepcion', 2),
('Mantenimiento', 3);

INSERT INTO DepartamentoEmpleado (DepartamentoId, EmpleadoId) VALUES
(1, 1),
(2, 2),
(3, 3);

INSERT INTO Reserva (TitularId, FechaInicio, FechaFin, Estado) VALUES
(1, '2026-04-01', '2026-04-05', 'Confirmada');

INSERT INTO ReservaHabitacion (ReservaId, HabitacionId, PrecioCobrado) VALUES
(1, 1, 50.00), 
(1, 4, 90.00); 

INSERT INTO Estadia (ReservaId, HabitacionId, FechaLlegada, FechaSalida) VALUES
(1, 1, GETDATE(), NULL); -- FechaSalida es NULL porque aún está hospedado

INSERT INTO EstadiaHuesped (EstadiaId, HuespedId) VALUES
(1, 1);

INSERT INTO Huesped (Nombres, Apellidos, FechaNacimiento, NroDocumentoIdentidad) VALUES
('Laura', 'Vargas', '1995-02-14', '99887766'),
('Jorge', 'Salinas', '1982-10-30', '44332211');

INSERT INTO Empleado (Nombres, Apellidos, Telefono, Rol) VALUES
('Carmen', 'Ruiz', '77799900', 'Limpieza'),
('Diego', 'Ortiz', '77788811', 'Recepcionista');

INSERT INTO Departamento (Nombre, EncargadoId) VALUES 
('Limpieza', 4); 

INSERT INTO DepartamentoEmpleado (DepartamentoId, EmpleadoId) VALUES 
(4, 4), 
(2, 5);

INSERT INTO Reserva (TitularId, FechaInicio, FechaFin, Estado) VALUES
(4, '2026-05-10', '2026-05-15', 'Confirmada');

INSERT INTO ReservaHabitacion (ReservaId, HabitacionId, PrecioCobrado) VALUES
(2, 3, 80.00); 

INSERT INTO Reserva (TitularId, FechaInicio, FechaFin, Estado) VALUES
(5, '2026-02-01', '2026-02-05', 'Finalizada');

INSERT INTO ReservaHabitacion (ReservaId, HabitacionId, PrecioCobrado) VALUES
(3, 6, 250.00);

INSERT INTO Estadia (ReservaId, HabitacionId, FechaLlegada, FechaSalida) VALUES
(3, 6, '2026-02-01 14:30:00', '2026-02-05 10:15:00');

INSERT INTO EstadiaHuesped (EstadiaId, HuespedId) VALUES
(2, 5);

DELETE FROM ReservaHabitacion 
WHERE ReservaId = 1 AND HabitacionId = 4;

UPDATE Reserva 
SET Estado = 'Estadía en curso' 
WHERE ReservaId = 1;


DECLARE @ReservaCanceladaId INT;

-- 1. Insertar la reserva con estado Cancelada
INSERT INTO Reserva (TitularId, FechaInicio, FechaFin, Estado) 
VALUES (3, '2026-06-01', '2026-06-05', 'Cancelada');

-- 2. Capturar el ID que SQL le asignó
SET @ReservaCanceladaId = SCOPE_IDENTITY();

-- 3. Asignarle la habitación para mantener la integridad relacional
INSERT INTO ReservaHabitacion (ReservaId, HabitacionId, PrecioCobrado) 
VALUES (@ReservaCanceladaId, 8, 80.00);