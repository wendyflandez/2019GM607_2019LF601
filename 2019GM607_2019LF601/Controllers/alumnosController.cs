using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2019GM607_2019LF601.Models;

namespace _2019GM607_2019LF601.Controllers
{
    [ApiController]
    public class alumnosController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public alumnosController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/alumnos")]
        public IActionResult Get()
        {
            IEnumerable<alumnos> notaList = from e in _contexto.alumnos select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/alumnos/{id}")]
        public IActionResult Join()
        {
            var insList = from e in _contexto.alumnos
                          join i in _contexto.inscripciones on e.id equals i.alumnoId
                          select new
                          {
                              e.id,
                              e.carnet,
                              e.nombre,
                              e.apellidos,
                              e.departamentoId,
                              e.estado,
                              id_ins = i.id,
                              i.materiaId,
                              i.inscripcion,
                              i.fecha,
                              estado_ins = i.estado
                          };
            if (insList.Count() > 0)
            {
                return Ok(insList);
            }
            return NotFound();
        }
        [HttpPut]
        [Route("api/alumnos")]
        public IActionResult upDateAlumnos([FromBody] alumnos modificar)
        {
            alumnos existealumno = (from e in _contexto.alumnos
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existealumno is null)
            {
                return NotFound();
            }
            existealumno.carnet = modificar.carnet;
            existealumno.nombre = modificar.nombre;
            existealumno.apellidos = modificar.apellidos;
            existealumno.dui = modificar.dui;
            existealumno.departamentoId = modificar.departamentoId;
            existealumno.estado = modificar.estado;
            _contexto.Entry(existealumno).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existealumno);
        }

        [HttpPost]
        [Route("api/alumnos/join")]
        public IActionResult GuardarTipo([FromBody] alumnos guardar)
        {
            try
            {
                IEnumerable<alumnos> equipoExiste = from e in _contexto.alumnos
                                                    where e.nombre == guardar.nombre
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.alumnos.Add(guardar);
                    _contexto.SaveChanges();
                    return Ok(guardar);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/alumnos/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(string nombre)
        {
            IEnumerable<alumnos> nombrealum = from e in _contexto.alumnos
                                                where e.nombre.Contains(nombre)
                                                select e;
            if (nombrealum.Count() > 0)
            {
                return Ok(nombrealum);
            }
            return NotFound();
        }
    }
}
