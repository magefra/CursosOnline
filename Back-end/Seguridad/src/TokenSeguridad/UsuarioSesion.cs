using Aplicacion.src.Contratos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Seguridad.src.TokenSeguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;




        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string obtenerUsuarioSesion()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?
                            .FirstOrDefault(x => x.Type== ClaimTypes.NameIdentifier)?.Value;

            return userName;
        }
    }
}
