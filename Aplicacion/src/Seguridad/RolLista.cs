using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Seguridad
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>>
        {

        }


        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {

            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _context;


            public Manejador(CursosContext context)
            {
                _context = context;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }
    }
}
