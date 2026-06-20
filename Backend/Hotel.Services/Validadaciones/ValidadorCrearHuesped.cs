using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Models.DTOs;

namespace Hotel.Services.Validadaciones
{
    public static class ValidadorCrearHuesped
    {
        public static void ValidarDatosObligatorios(CrearHuespedDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombres))
                throw new ArgumentException("El nombre del huésped es obligatorio.");
                
            if (string.IsNullOrWhiteSpace(dto.Apellidos))
                throw new ArgumentException("El apellido del huésped es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.NroDocumentoIdentidad))
                throw new ArgumentException("El número de documento es obligatorio.");
        }
    }
}


