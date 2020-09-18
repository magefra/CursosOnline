using MediatR;
using Persistencia.src.DapperConexion.Instructores;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>>
        {

        }


        public class Manejador : IRequestHandler<Lista, List<InstructorModel>>
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly IInstructor _instructorRepository;



            public Manejador(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                var result =  await _instructorRepository.ObtenerLista();


                return result.ToList();
            }


        }
    }
}
