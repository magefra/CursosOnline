﻿
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.src.DapperConexion.Instructores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.src.Instructores;

namespace WebAPI.Controllers
{
    public class InstructorController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores()
        {
            return await Mediator.Send(new Consulta.Lista());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>> ObtenerId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.Ejecuta { Id = id});
        }



        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Actualizar(Guid id, Editar.Ejecuta data)
        {
            data.InstructorId = id;

            return await Mediator.Send(data);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Elimina.Ejecuta{ Id = id});

        }




}
}
