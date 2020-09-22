using AutoMapper;
using Dominio.src;
using FluentValidation;
using MediatR;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Alumno { get; set; }

            public int? Puntaje { get; set; }

            public string Comentario { get; set; }

            public Guid CursoId { get; set; }
        }


        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.Comentario).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();

            }
        }



        public class Manejador : IRequestHandler<Ejecuta>
        {

            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _cursosContext;

            /// <summary>
            /// 
            /// </summary>
            private readonly IMapper _mapper;



            public Manejador(CursosContext cursosContext, IMapper mapper)
            {
                _cursosContext = cursosContext;
                _mapper = mapper;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var comentario = new Comentario
                {
                    ComentarioId = Guid.NewGuid(),
                    Alumna = request.Alumno,
                    ComentarioTexto = request.Comentario,
                    CursoId = request.CursoId
                };



                _cursosContext.Comentario.Add(comentario);



                var result = await _cursosContext.SaveChangesAsync();

                if(result> 0)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo insertar el comentario");


            }
        }

    }
}
