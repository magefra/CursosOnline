using Aplicacion.src.Cursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.src.DapperConexion.Instructores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class InstructorController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores()
        {
            return await Mediator.Send(new Aplicacion.src.Instructores.Consulta.Lista());
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Aplicacion.src.Instructores.Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
