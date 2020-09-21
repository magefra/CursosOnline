using Aplicacion.src.Contratos;
using Aplicacion.src.ManejadorErrores;
using Dominio.src;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }

            public string Passoword { get; set; }
        }


        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("El correo no debe estar vació");

                RuleFor(x => x.Passoword)
                    .NotEmpty()
                    .WithMessage("El password no debe estar vació");
            }
        }



        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly UserManager<Usuario> _userManager;

            /// <summary>
            /// 
            /// </summary>
            private readonly SignInManager<Usuario> _signInManager;

            /// <summary>
            /// 
            /// </summary>
            private readonly IJwtGenerador _jwtGenerador;



            public Manejador(UserManager<Usuario> userManager,
                             SignInManager<Usuario> signInManager,
                             IJwtGenerador jwtGenerador)
            {
                _userManager = userManager;
                _signInManager = signInManager;
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
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized);
                }


                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Passoword, false);
                var resultadoRoles =await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Email = usuario.Email,
                        Token = _jwtGenerador.crearToken(usuario, listaRoles),
                        UserName = usuario.UserName,
                        Imagen  = null
                    };
                }



                throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
