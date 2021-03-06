﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.src.Comentarios;

namespace WebAPI.Controllers
{
    public class ComentarioController : MiControllerBase
    {


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data); 
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Elimintar(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
