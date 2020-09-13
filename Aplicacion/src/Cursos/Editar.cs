using Aplicacion.src.ManejadorErrores;
using FluentValidation;
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
    public class Editar
    {

        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }

            public string Titulo { get; set; }

            public string Descripcion { get; set; }

            public DateTime? FechaPublicacion { get; set; }
        }



        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo)
                    .NotEmpty();

                RuleFor(x => x.Descripcion)
                    .NotEmpty();

                RuleFor(x => x.FechaPublicacion)
                    .NotEmpty();
            }
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


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _cursosContext.Curso.FindAsync(request.CursoId);


                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontró el curso" });
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;


                var result = await _cursosContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se guardaron los cambios en el curso");

            }
        }
    }
}
