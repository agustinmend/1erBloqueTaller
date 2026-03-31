using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Entidades
{
    public class Huesped
    {
        public int HuespedId { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string NroDocumentoIdentidad { get; set; } = string.Empty;
    }
}
