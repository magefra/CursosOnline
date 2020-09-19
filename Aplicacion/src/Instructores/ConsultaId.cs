using Aplicacion.src.ManejadorErrores;
using MediatR;
using Persistencia.src.DapperConexion.Instructores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Instructores
{
    public class ConsultaId
    {
        public class Ejecuta : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta, InstructorModel>
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
            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructor.ObtenerPorId(request.Id);



                if(result == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,
                        new
                        {
                            mensaje = "No se encontró el instructor"
                        });
                }




                return result;



            }
        }

    }
}
