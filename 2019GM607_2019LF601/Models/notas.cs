using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2019GM607_2019LF601.Models
{
    public class notas
    {
        [Key]
        public int id { get; set; }
        public int inscripcionId { get; set; }
        public string evaluacion { get; set; }
        public decimal nota { get; set; }
        public decimal porcentaje { get; set; }
        public DateTime fecha { get; set; }
    }
}
