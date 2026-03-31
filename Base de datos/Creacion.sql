CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Huesped (
    HuespedId INT IDENTITY(1,1) CONSTRAINT PK_Huesped PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    NroDocumentoIdentidad VARCHAR(50) NOT NULL CONSTRAINT UQ_Huesped_Doc UNIQUE
);

CREATE TABLE Habitacion (
    HabitacionId INT IDENTITY(1,1) CONSTRAINT PK_Habitacion PRIMARY KEY,
    NroHabitacion VARCHAR(10) NOT NULL CONSTRAINT UQ_Habitacion_Nro UNIQUE,
    Capacidad INT NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    Estado VARCHAR(20) NOT NULL,   --Activo, Inactivo, Mantenimiento
    PrecioBase DECIMAL(10,2) NOT NULL 
);

CREATE TABLE Empleado (
    EmpleadoId INT IDENTITY(1,1) CONSTRAINT PK_Empleado PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Rol VARCHAR(50) NOT NULL 
);

CREATE TABLE Departamento (
    DepartamentoId INT IDENTITY(1,1) CONSTRAINT PK_Departamento PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    EncargadoId INT NULL, 
    CONSTRAINT FK_Departamento_Encargado FOREIGN KEY (EncargadoId) 
        REFERENCES Empleado(EmpleadoId)
);

CREATE TABLE Reserva (
    ReservaId INT IDENTITY(1,1) CONSTRAINT PK_Reserva PRIMARY KEY,
    TitularId INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Reserva_Titular FOREIGN KEY (TitularId) 
        REFERENCES Huesped(HuespedId)
);

CREATE TABLE DepartamentoEmpleado (
    DepartamentoId INT NOT NULL,
    EmpleadoId INT NOT NULL,
    CONSTRAINT PK_DepartamentoEmpleado PRIMARY KEY (DepartamentoId, EmpleadoId),
    CONSTRAINT FK_DeptoEmp_Departamento FOREIGN KEY (DepartamentoId) 
        REFERENCES Departamento(DepartamentoId),
    CONSTRAINT FK_DeptoEmp_Empleado FOREIGN KEY (EmpleadoId) 
        REFERENCES Empleado(EmpleadoId)
);

CREATE TABLE ReservaHabitacion (
    ReservaId INT NOT NULL,
    HabitacionId INT NOT NULL,
    PrecioCobrado DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_ReservaHabitacion PRIMARY KEY (ReservaId, HabitacionId),
    CONSTRAINT FK_ResHab_Reserva FOREIGN KEY (ReservaId) 
        REFERENCES Reserva(ReservaId),
    CONSTRAINT FK_ResHab_Habitacion FOREIGN KEY (HabitacionId) 
        REFERENCES Habitacion(HabitacionId)
);

CREATE TABLE Estadia (
    EstadiaId INT IDENTITY(1,1) CONSTRAINT PK_Estadia PRIMARY KEY,
    ReservaId INT NOT NULL,
    HabitacionId INT NOT NULL,
    FechaLlegada DATETIME NOT NULL, 
    FechaSalida DATETIME NULL,
    CONSTRAINT FK_Estadia_ReservaHabitacion FOREIGN KEY (ReservaId, HabitacionId) 
        REFERENCES ReservaHabitacion(ReservaId, HabitacionId)
);

CREATE TABLE EstadiaHuesped (
    EstadiaId INT NOT NULL,
    HuespedId INT NOT NULL,
    CONSTRAINT PK_EstadiaHuesped PRIMARY KEY (EstadiaId, HuespedId),
    CONSTRAINT FK_EstHue_Estadia FOREIGN KEY (EstadiaId) 
        REFERENCES Estadia(EstadiaId),
    CONSTRAINT FK_EstHue_Huesped FOREIGN KEY (HuespedId) 
        REFERENCES Huesped(HuespedId)
);

