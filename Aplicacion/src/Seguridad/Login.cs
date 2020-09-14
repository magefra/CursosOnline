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
        public class Ejecuta : IRequest<Usuario>
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



        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly UserManager<Usuario> _userManager;

            /// <summary>
            /// 
            /// </summary>
            private readonly SignInManager<Usuario> _signInManager;


            public Manejador(UserManager<Usuario> userManager,
                             SignInManager<Usuario> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized);
                }


                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Passoword, false);

                if (resultado.Succeeded)
                {
                    return usuario;
                }



                throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
