using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio.src;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.src.Data;

namespace Aplicacion.src.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<Curso>>
        {


        }




        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _cursosContext;


            public Manejador(CursosContext cursosContext)
            {
                _cursosContext = cursosContext;
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                return await _cursosContext.Curso.ToListAsync();
            }
        }
    }
}