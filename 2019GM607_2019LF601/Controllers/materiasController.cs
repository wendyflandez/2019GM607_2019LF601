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
    public class materiasController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public materiasController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/materia")]
        public IActionResult Get()
        {
            IEnumerable<materias> notaList = from e in _contexto.materias select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/materias/{id}")]
        public IActionResult Join()
        {
            var insList = from e in _contexto.materias
                          join i in _contexto.inscripciones on e.id equals i.materiaId
                          select new
                          {
                              e.id,
                              e.facultadId,
                              e.materia,
                              e.unidades_valorativas,
                              e.estado,
                              id_not = i.id,
                              i.alumnoId,
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
        [Route("api/materias")]
        public IActionResult upDatemat([FromBody] materias modificar)
        {
            materias existemat = (from e in _contexto.materias
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existemat is null)
            {
                return NotFound();
            }
            existemat.facultadId = modificar.facultadId;
            existemat.materia = modificar.materia;
            existemat.unidades_valorativas = modificar.unidades_valorativas;
            existemat.estado = modificar.estado;
            _contexto.Entry(existemat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existemat);
        }

        [HttpGet]
        [Route("api/materias/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(string nombre)
        {
            IEnumerable<materias> nombrealum = from e in _contexto.materias
                                              where e.materia.Contains(nombre)
                                              select e;
            if (nombrealum.Count() > 0)
            {
                return Ok(nombrealum);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/materias/join")]
        public IActionResult GuardarTipo([FromBody] materias guardar)
        {
            try
            {
                IEnumerable<materias> equipoExiste = from e in _contexto.materias
                                                    where e.materia == guardar.materia
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.materias.Add(guardar);
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
    }
}
