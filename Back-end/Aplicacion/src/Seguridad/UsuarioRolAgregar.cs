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
    public class UsuarioRolAgregar
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }

            public string RolNombre { get; set; }
        }


        public class EjecutaValidar : AbstractValidator<Ejecuta>
        {
            public EjecutaValidar()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {

            /// <summary>
            /// 
            /// </summary>
            private readonly RoleManager<IdentityRole> _roleManager;

            /// <summary>
            /// 
            /// </summary>
            private readonly UserManager<Usuario> _userManager;


            public Manejador(RoleManager<IdentityRole> roleManager,
                             UserManager<Usuario> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);

                if (role == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new
                    {
                        Mensaje = "El rol no existe"
                    });
                }


                var usuario = await _userManager.FindByNameAsync(request.UserName);

                if (usuario == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new
                    {
                        Mensaje = "Usuario no existe"
                    });
                }


                var result = await _userManager.AddToRoleAsync(usuario, request.RolNombre);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo agregar el Rol al usuario");


            }
        }


    }
}
