using Aplicacion.src.ManejadorErrores;
using Dominio.src;
using FluentValidation;
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
    public class Editar
    {

        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }

            public string Titulo { get; set; }

            public string Descripcion { get; set; }

            public DateTime? FechaPublicacion { get; set; }

            public List<Guid> ListaInstructor { get; set; }

            public decimal? Precio { get; set; }

            public decimal? Promocion { get; set; }
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


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
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
                curso.FechaCreacion = DateTime.UtcNow;



                //Precio
                var precioEntidad = _cursosContext.Precio.Where(x => x.CursoId == request.CursoId).FirstOrDefault();
                if(precioEntidad != null)
                {
                    precioEntidad.Promocion = request.Promocion ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                }
                else // Si no tiene un precio, se le agregamos 
                {
                    precioEntidad = new Precio
                    {
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.Promocion ?? 0,
                        CursoId = request.CursoId
                    };

                    _cursosContext.Precio.Add(precioEntidad);
                }




                //Instructor
                if(request.ListaInstructor != null)
                {
                    if(request.ListaInstructor.Count > 0)
                    {
                        var instructorBD = _cursosContext.CursoInstructor.Where(c => c.CursoId == request.CursoId);

                        foreach(var instructorEliminar in instructorBD)
                        {
                            _cursosContext.CursoInstructor.Remove(instructorEliminar);
                        }

                        foreach(var id in request.ListaInstructor)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = id
                            };

                            _cursosContext.CursoInstructor.Add(nuevoInstructor);
                        }

                    }
                }


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
