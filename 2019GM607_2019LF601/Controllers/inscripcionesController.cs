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
    public class inscripcionesController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public inscripcionesController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/inscripciones")]
        public IActionResult Get()
        {
            IEnumerable<inscripciones> notaList = from e in _contexto.inscripciones select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }

        
        [HttpGet]
        [Route("api/inscripciones/{id}")]
        public IActionResult Join()
        {
            var insList = from e in _contexto.inscripciones
                          join n in _contexto.notas on e.id equals n.inscripcionId
                          select new
                          {
                              e.id,
                              e.alumnoId,
                              e.materiaId,
                              e.inscripcion,
                              e.fecha,
                              e.estado,
                              id_not = n.id,
                              n.evaluacion,
                              n.nota,
                              n.porcentaje,
                              fecha_ins = n.fecha
                          };
            if (insList.Count() > 0)
            {
                return Ok(insList);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/inscripciones")]
        public IActionResult upDateIns([FromBody] inscripciones modificar)
        {
            inscripciones existeins = (from e in _contexto.inscripciones
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existeins is null)
            {
                return NotFound();
            }
            existeins.alumnoId = modificar.alumnoId;
            existeins.materiaId = modificar.materiaId;
            existeins.inscripcion = modificar.inscripcion;
            existeins.fecha = modificar.fecha;
            existeins.estado = modificar.estado;
            _contexto.Entry(existeins).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existeins);
        }

        [HttpGet]
        [Route("api/inscripcion/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(int nombre)
        {
            IEnumerable<inscripciones> nombrealum = from e in _contexto.inscripciones
                                              where e.inscripcion == nombre
                                              select e;
            if (nombrealum.Count() > 0)
            {
                return Ok(nombrealum);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/ins/join")]
        public IActionResult GuardarTipo([FromBody] inscripciones guardar)
        {
            try
            {
                IEnumerable<inscripciones> equipoExiste = from e in _contexto.inscripciones
                                                    where e.estado == guardar.estado
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.inscripciones.Add(guardar);
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
