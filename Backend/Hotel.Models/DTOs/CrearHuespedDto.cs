using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.DTOs
{
    public class CrearHuespedDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage ="La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El numero de documento es obligatorio")]
        [StringLength(20, ErrorMessage = "El documento no puede exceder los 20 caracteres")]
        public string NroDocumentoIdentidad { get; set; } = string.Empty;
    }
}
