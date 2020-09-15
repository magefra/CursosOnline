using Aplicacion.src.Contratos;
using Dominio.src;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData>
        {

        }



        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly UserManager<Usuario> _userManager;
            
            /// <summary>
            /// 
            /// </summary>
            private readonly IJwtGenerador _jwtGenerador;

            /// <summary>
            /// 
            /// </summary>
            private readonly IUsuarioSesion _usuarioSesion;



            public Manejador(UserManager<Usuario> userManager,
                             IJwtGenerador jwtGenerador,
                             IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.obtenerUsuarioSesion());



                return new UsuarioData
                {
                    UserName = usuario.UserName,
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.crearToken(usuario),
                    Email = usuario.Email,
                    Imagen =usuario.Email
                };

            }
        }
    }
}
