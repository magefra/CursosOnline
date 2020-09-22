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
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Nombre { get; set; }

            public string Apellidos { get; set; }

            public string Titulo { get; set; }

        }



        public class EjecutaValida : AbstractValidator<Ejecuta>
        {
            public EjecutaValida()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Titulo).NotEmpty();
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

                var resultado = await _instructor.Nuevo(request.Nombre, request.Apellidos, request.Titulo);


                if (resultado >= 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el instructor");

            }
        }

    }
}
