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
    public class departamentosController : ControllerBase
    {
        private readonly _2019GM607_2019LF601Context _contexto;
        public departamentosController(_2019GM607_2019LF601Context miContexto)
        {
            this._contexto = miContexto;

        }
        [HttpGet]
        [Route("api/departamentos")]
        public IActionResult Get()
        {
            IEnumerable<departamentos> notaList = from e in _contexto.departamentos select e;
            if (notaList.Count() > 0)
            {
                return Ok(notaList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/departamentos/{id}")]
        public IActionResult Join()
        {
            var insList = from e in _contexto.departamentos
                          join a in _contexto.alumnos on e.id equals a.departamentoId
                          select new
                          {
                              e.id,
                              e.departamento, 
                              id_alum = a.id,
                              a.carnet,
                              a.nombre,
                              a.apellidos,
                              a.dui,
                              a.estado
                          };
            if (insList.Count() > 0)
            {
                return Ok(insList);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/departamento")]
        public IActionResult upDateDep([FromBody] departamentos modificar)
        {
            departamentos existedep = (from e in _contexto.departamentos
                                    where e.id == modificar.id
                                    select e).FirstOrDefault();
            if (existedep is null)
            {
                return NotFound();
            }
            existedep.departamento = modificar.departamento;
            _contexto.Entry(existedep).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(existedep);
        }

        [HttpGet]
        [Route("api/departamentos/buscarNombre/{nombre}")]
        public IActionResult getbyNombre(string nombre)
        {
            IEnumerable<departamentos> nombredep = from e in _contexto.departamentos
                                              where e.departamento.Contains(nombre)
                                              select e;
            if (nombredep.Count() > 0)
            {
                return Ok(nombredep);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/depa/join")]
        public IActionResult GuardarTipo([FromBody] departamentos guardar)
        {
            try
            {
                IEnumerable<departamentos> equipoExiste = from e in _contexto.departamentos
                                                    where e.departamento == guardar.departamento
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.departamentos.Add(guardar);
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
