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
    public class notasController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public notasController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/notas")]
        public IActionResult Get()
        {
            IEnumerable<notas> notaList = from e in _contexto.notas select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/notas")]
        public IActionResult upDateNot([FromBody] notas modificar)
        {
            notas existenot = (from e in _contexto.notas
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existenot is null)
            {
                return NotFound();
            }
            existenot.inscripcionId = modificar.inscripcionId;
            existenot.evaluacion = modificar.evaluacion;
            existenot.nota = modificar.nota;
            existenot.porcentaje = modificar.porcentaje;
            existenot.fecha = modificar.fecha;
            _contexto.Entry(existenot).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existenot);
        }

        [HttpGet]
        [Route("api/notas/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(string nombre)
        {
            IEnumerable<notas> nombrealum = from e in _contexto.notas
                                              where e.evaluacion.Contains(nombre)
                                              select e;
            if (nombrealum.Count() > 0)
            {
                return Ok(nombrealum);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/nota/join")]
        public IActionResult GuardarTipo([FromBody] notas guardar)
        {
            try
            {
                IEnumerable<notas> equipoExiste = from e in _contexto.notas
                                                    where e.evaluacion == guardar.evaluacion
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.notas.Add(guardar);
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
