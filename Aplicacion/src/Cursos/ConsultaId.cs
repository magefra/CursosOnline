using Aplicacion.src.ManejadorErrores;
using Dominio.src;
using MediatR;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, Curso>
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
            public  async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _cursosContext.Curso.FindAsync(request.Id);



                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontró el curso" });
                }


                return curso;
            }
        }

    }
}
