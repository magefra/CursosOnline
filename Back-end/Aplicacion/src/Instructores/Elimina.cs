using MediatR;
using Persistencia.src.DapperConexion.Instructores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Instructores
{
    public class Elimina
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
            private readonly IInstructor _instructor;



            public Manejador(IInstructor instructor)
            {
                _instructor = instructor;
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructor.Eliminar(request.Id);
            
                if(result > 0)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo eliminar el instructor");
            
            }
        }



    }
}
