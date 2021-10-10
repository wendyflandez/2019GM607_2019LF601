using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2019GM607_2019LF601.Models
{
    public class departamentos
    {
        [Key]
        public int id { get; set; }
        public string departamento { get; set; }
    }
}
