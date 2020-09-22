using Aplicacion.src.ManejadorErrores;
using MediatR;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
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
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                //Instructores
                var instructoresBD = _cursosContext.CursoInstructor.Where(c => c.CursoId == request.Id);                
                foreach(var instructor in instructoresBD)
                {
                    _cursosContext.CursoInstructor.Remove(instructor);
                }


                //Comentarios
                var comentariosBD = _cursosContext.Comentario.Where(x => x.CursoId == request.Id);
                foreach(var comentario in comentariosBD)
                {
                    _cursosContext.Comentario.Remove(comentario);
                }


                //Precio
                var precioBD = _cursosContext.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if(precioBD != null)
                {
                    _cursosContext.Precio.Remove(precioBD);
                }


                //Cursos
                var curso = await _cursosContext.Curso.FindAsync(request.Id);
                if(curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontró el curso" });
                }

                _cursosContext.Remove(curso);

                var resultado = await _cursosContext.SaveChangesAsync();

                if(resultado >0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudieron guardar los cambios");

            }
        }
    }
}
