using FluentValidation;
using MediatR;
using Persistencia.src.DapperConexion.Instructores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Instructores
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid InstructorId { get; set; }

            public string Nombre { get; set; }

            public string Apellidos { get; set; }

            public string Grado { get; set; }
        }



        public class EjecutaValidar : AbstractValidator<Ejecuta>
        {
            public EjecutaValidar()
            {
              

                RuleFor(x => x.Nombre).NotEmpty();

                RuleFor(x => x.Apellidos).NotEmpty();


            }
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
                var result =await _instructor.Actualiza(request.InstructorId,
                                      request.Nombre,
                                      request.Apellidos,
                                      request.Grado);



                if(result > 0)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo actualizar la data del instructor");

            }
        }
    }
}
