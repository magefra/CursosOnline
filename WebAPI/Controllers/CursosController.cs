using Aplicacion.src.Cursos;
using Dominio.src;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{

    //http://localhost:5000/api/cursos

    [Route("api/[Controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;


        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new Consulta.ListaCursos());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(Guid id)
        {
            return await _mediator.Send(new ConsultaId.CursoUnico { CursoId = id });
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta editar)
        {
            editar.CursoId = id;

            return await _mediator.Send(editar);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await _mediator.Send(new Eliminar.Ejecuta { Id = id});
        }




    }
}
