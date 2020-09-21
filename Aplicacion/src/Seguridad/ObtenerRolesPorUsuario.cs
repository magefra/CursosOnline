using Aplicacion.src.ManejadorErrores;
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
    public class ObtenerRolesPorUsuario
    {
        public class Ejecuta : IRequest<List<string>>
        {
            public string UserName { get; set; }
        }



        public class Manejador : IRequestHandler<Ejecuta, List<string>>
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
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var usuario = await _userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new
                    {
                        Mensaje = "Usuario no existe"
                    });
                }


                var result = await _userManager.GetRolesAsync(usuario);

                return new List<string>(result);


            }
        }


    }
}
