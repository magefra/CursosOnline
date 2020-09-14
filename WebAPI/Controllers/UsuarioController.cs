﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.src.Seguridad;
using Dominio.src;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
    public class UsuarioController : MiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(Login.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        } 
    }
}