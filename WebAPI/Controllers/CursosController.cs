using Aplicacion.src.Cursos;
using Dominio.src;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{

    //http://localhost:5000/api/cursos
    public class CursosController : MiControllerBase
    {
        

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await Mediator.Send(new Consulta.ListaCursos());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(Guid id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { CursoId = id });
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta editar)
        {
            editar.CursoId = id;

            return await Mediator.Send(editar);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id});
        }




    }
}
