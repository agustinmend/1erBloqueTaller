using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DTOs
{
    public class ContactoServicioDto
    {
        public string NombreServicio { get; set; } = string.Empty;
        public string NombreEncargado { get; set; } = string.Empty;
        public string RolEncargado { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}
