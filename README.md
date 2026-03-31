# Sistema de Gestión para Hotel Pequeño

## 1. Descripción General de la Solución
Este proyecto es un prototipo funcional de un sistema para la gestión de un hotel pequeño. Su objetivo es optimizar y digitalizar los procesos críticos de recepción, permitiendo administrar reservas, registrar ingresos y mantener un directorio de contactos de los servicios internos del hotel. El sistema está diseñado para prevenir inconsistencias, como el solapamiento de fechas, la sobreocupación de habitaciones o el ingreso de reservas canceladas.

---

## 2. Arquitectura Utilizada
El sistema implementa una arquitectura distribuida (Cliente-Servidor) y el backend sigue un patrón estricto de Arquitectura en N-Capas para garantizar la separación de responsabilidades.

### Frontend (Single Page Application)
* **Framework:** Angular 
* **Diseño:** Componentes modulares.
* **Comunicación:** Servicios inyectables (`@Injectable`) que consumen la API REST mediante `HttpClient`.

### Backend (API RESTful)
* **Framework:** ASP.NET Core (C#)
* **Estructura en Capas:**
  1. **API (`Hotel.API`):** Controladores REST que manejan las peticiones HTTP y devuelven respuestas estandarizadas.
  2. **Services (`Hotel.Services`):** Contiene la Lógica de Negocio pura.
  3. **Repository (`Hotel.Repository`):** Capa de acceso a datos.
  4. **Models (`Hotel.Models`):** Definición de DTOs (Data Transfer Objects) para desacoplar la base de datos de las respuestas del servidor.

## 3. Base de datos
<img width="1389" height="851" alt="image" src="https://github.com/user-attachments/assets/d332ce06-f000-4d1b-9be8-1d90f9b81d9f" />
* **Huesped:** Información personal y documentos.
* **Habitacion:** Definición de capacidad, tipo y precio base.
* **Reserva:** Cabecera de la reserva y estado actual (`Confirmada`, `Cancelada`, `Estadía en curso`, `Finalizada`).
* **ReservaHabitacion (N:M):** Tabla intermedia que detalla qué habitaciones componen una reserva y congela el precio pactado.
* **Estadia:** Registro físico del ingreso de una reserva al hotel.
* **EstadiaHuesped:** Detalle de las personas físicas que ocupan la habitación.
* **Departamento y Empleado:** Gestión del personal y directorio de servicios internos.

## 4. Estructura del proyecto

```
📁 Programa/
 ├── 📄 README.md
 ├── 📄 .gitignore
 ├── 📄 Hotel.sln
 ├── 📁 Base de datos/
 │    └── 📄 Script_Poblacion_y_Estructura.sql
 ├── 📁 Backend/
 │    ├── 📁 Hotel.API/         (Controladores y configuración Web)
 │    ├── 📁 Hotel.Models/      (DTOs y Entidades)
 │    ├── 📁 Hotel.Repository/  (Implementación de SQL)
 │    └── 📁 Hotel.Services/    (Reglas de Negocio)
 └── 📁 Frontend/               (Proyecto Angular CLI)
      ├── 📁 src/app/core/      (Servicios HTTP, Interfaces)
      └── 📁 src/app/components/    (Componentes, Vistas, Modales)
```

## 5. Funcionalidades implementadas
* **HU-1(Registrar-Huesped):** Formulario que toma los datos basicos del Huesped(Nombre, Fecha nacimiento, nro de documento) con validaciones de campos obligatorios e impedimento de duplicados
* **HU-2(Crear-Reserva-Habitacion):** Formulario que registra una reserva asociando huesped, habitacion y fechas de estadia, con validaciones de solapamiento y capacidad maxima
* **HU-3(Consultar-reservas-activas-y-futuras):** tabla en la que se vizualizan las reservas del hotel que se encuentran en estado confirmada o en curso, estas muestran datos principales y estan ordenadas cronologicamente
* **HU-4(Registrar-Check-in):** Formulario que permite registrar la entrada fisica de los huespedes a la habitacion
* **HU-5(Variacion-de-tipo-habitacion-en-reserva):** menu desplegable en el formulario de reserva que permite ofrecer distintos tipos de habitaciones (Se uson patron de diseno Factory para esta HU)
* **HU-6(Vizualizar-contactos-de-servicio-de-Hotel):** lista que muestra la informacion basica de los contactos clave para servicios del hotel
* **HU-9(Buscar-reservas-por-Huesped):** Buscador que permite buscar las reservas que tenga un Huesped mediante su nombre o nro de documento

## 6. Instrucciones minimas de ejecucion

### Requisitos previos
* **SQL Server**
* **.NET 8(o superior) SDK**
* **Node.js y Angular CLI instalado globalmente (npm install -g @angular/cli)**

### Paso 1
1. Abrir SQL Server Management Studio (SSMS).
2. Ejecutar el script SQL proporcionado en la carpeta Base de datos/ para crear la base de datos Hotel, sus tablas y poblar los datos de prueba iniciales.
### Paso 2
1. Abrir una terminal en la carpeta /Backend/Hotel.API/.
2. Verificar que la cadena de conexión en appsettings.json apunte a tu instancia local de SQL Server.
3. Ejecutar el comando: dotnet run
### Paso 3
1. Abrir una nueva terminal en la carpeta /Frontend/.
2. Instalar las dependencias de Node: npm install
3. Levantar el servidor: ng serve
4. Se abrirá automáticamente en http://localhost:4200 con el sistema listo para su uso.
