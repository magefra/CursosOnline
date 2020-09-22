using Aplicacion.src.ManejadorErrores;
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
    public class RolNuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
        }

        public class ValidaEjecuta : AbstractValidator<Ejecuta>
        {
            public ValidaEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly RoleManager<IdentityRole> _roleManager;


            public Manejador(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);

                if (role != null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest, new
                    {
                        Mensaje = "Ya éxiste el rol"
                    });
                }

                var result = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));


                if (result.Succeeded)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo guarda el rol");


            }
        }

    }


}
