using System;
using System.Collections.Generic;

#nullable disable

namespace ApiAlumnos.Services.Datos
{
    public partial class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
    }
}
