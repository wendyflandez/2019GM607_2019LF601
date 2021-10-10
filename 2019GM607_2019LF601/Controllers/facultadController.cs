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
    public class facultadController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public facultadController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/facultad")]
        public IActionResult Get()
        {
            IEnumerable<facultades> notaList = from e in _contexto.facultad select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/facultad/{id}")]
        public IActionResult Join()
        {
            var insList = from e in _contexto.facultad
                          join m in _contexto.materias on e.id equals m.facultadId
                          select new
                          {
                              e.id,
                              e.facultad,
                              id_mat = m.id,
                              m.materia,
                              m.unidades_valorativas,
                              m.estado
                          };
            if (insList.Count() > 0)
            {
                return Ok(insList);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/facultad")]
        public IActionResult upDateFacu([FromBody] facultades modificar)
        {
            facultades existefac = (from e in _contexto.facultad
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existefac is null)
            {
                return NotFound();
            }
            existefac.facultad = modificar.facultad;
            _contexto.Entry(existefac).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existefac);
        }

        [HttpGet]
        [Route("api/facultad/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(string nombre)
        {
            IEnumerable<facultades> nombrefac = from e in _contexto.facultad
                                              where e.facultad.Contains(nombre)
                                              select e;
            if (nombrefac.Count() > 0)
            {
                return Ok(nombrefac);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/facultad/join")]
        public IActionResult GuardarTipo([FromBody] facultades guardar)
        {
            try
            {
                IEnumerable<facultades> equipoExiste = from e in _contexto.facultad
                                                    where e.facultad == guardar.facultad
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.facultad.Add(guardar);
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
