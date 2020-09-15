using Aplicacion.src.Contratos;
using Aplicacion.src.ManejadorErrores;
using Dominio.src;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }

            public string Apellidos { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string UserName { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _cursosContext;

            /// <summary>
            /// 
            /// </summary>
            private readonly UserManager<Usuario> _userManager;

            /// <summary>
            /// 
            /// </summary>
            private readonly IJwtGenerador _jwtGenerador;



            public Manejador(CursosContext cursosContext,
                             UserManager<Usuario> userManager,
                             IJwtGenerador jwtGenerador)
            {
                _cursosContext = cursosContext;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _cursosContext.Users.Where(
                                    x => x.Email == request.Email).AnyAsync();


                if (existe)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest,
                                                new { mensaje = "Ya éxiste un usuario registrado con ese Email" });
                }


                var existeUserName = await _cursosContext.Users.Where(x => x.UserName == request.UserName).AnyAsync();



                if (existeUserName)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest, new
                    {
                        mensaje = "Existe ya un usuario con este userName"
                    });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.UserName

                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);



                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.crearToken(usuario),
                        UserName = usuario.UserName
                    };
                }


                throw new Exception("No se pudo agregar al nuevo usuario");



            }
        }
    }
}
