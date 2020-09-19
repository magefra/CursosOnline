using Aplicacion.src.ManejadorErrores;
using AutoMapper;
using MediatR;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Comentarios
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
                var comentario = await _cursosContext.Comentario.FindAsync(request.Id);


                if (comentario == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new
                    {
                        mensaje = "No se encontró el comentario"
                    });
                }

                _cursosContext.Remove(comentario);



                var result = await _cursosContext.SaveChangesAsync();


                if(result > 0)
                {
                    return Unit.Value;
                }



                throw new Exception("No se pudo eliminar el comentario");

            }
        }
    }
}
