using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2019GM607_2019LF601.Models
{
    public class inscripciones
    {
        [Key]
        public int id { get; set; }
        public int alumnoId { get; set; }
        public int materiaId { get; set; }
        public int inscripcion { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}
