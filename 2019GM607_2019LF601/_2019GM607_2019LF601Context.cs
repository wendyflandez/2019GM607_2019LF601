using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _2019GM607_2019LF601.Models;

namespace _2019GM607_2019LF601
{
    public class _2019GM607_2019LF601Context: DbContext
    {
        public _2019GM607_2019LF601Context(DbContextOptions<_2019GM607_2019LF601Context> options) : base(options)
        {
        }

        public DbSet<alumnos> alumnos { get; set; }
        public DbSet<departamentos> departamentos { get; set; }
        public DbSet<facultades> facultad { get; set; }
        public DbSet<inscripciones> inscripciones { get; set; }
        public DbSet<materias> materias { get; set; }
        public DbSet<notas> notas { get; set; }
    }
}
