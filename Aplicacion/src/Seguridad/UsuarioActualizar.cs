using Aplicacion.src.Contratos;
using Aplicacion.src.ManejadorErrores;
using Dominio.src;
using FluentValidation;
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
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }

            public string Apellidos { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }


            public string UserName { get; set; }
        }



        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _contex;


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
            private readonly IPasswordHasher<Usuario> _passwordHasher;



            public Manejador(CursosContext context,
                             UserManager<Usuario> userManager,
                             IJwtGenerador jwtGenerador,
                             IPasswordHasher<Usuario> passwordHasher)
            {
                _contex = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordHasher = passwordHasher;
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarioToken = await _userManager.FindByNameAsync(request.UserName);


                if (usuarioToken == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new
                    {
                        Mensaje = "No existe un usuario con este userName"
                    });
                }


                ///Evaluar si un usuario ya tiene ese email.
                var result = await _contex.Users.Where(x => x.Email == request.Email &&
                                                  x.UserName != request.UserName).AnyAsync();



                if (result)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError, new
                    {
                        Mensaje = "Este email pertenece a otro usuario"
                    });
                }



                usuarioToken.NombreCompleto = request.Nombre + " " + request.Apellidos;
                //Método para encriptar el password
                usuarioToken.PasswordHash = _passwordHasher.HashPassword(usuarioToken, request.Password);
                usuarioToken.Email = request.Email;



                var resultUpdate = await _userManager.UpdateAsync(usuarioToken);
                var resultRoles = await _userManager.GetRolesAsync(usuarioToken);
                var listRoles = new List<string>(resultRoles);


                if (resultUpdate.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuarioToken.NombreCompleto,
                        UserName = usuarioToken.UserName,
                        Imagen = usuarioToken.Email,
                        Token = _jwtGenerador.crearToken(usuarioToken, listRoles)

                    };
                }


                throw new Exception("No se pudo actualizar el usuario");

            }
        }
    }
}
