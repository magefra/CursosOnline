using MediatR;
using Persistencia.src.DapperConexion.Paginacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Cursos
{
    public class PaginacionCurso
    {
        public class Ejecuta : IRequest<PaginacionModel>
        {
            public string Titulo { get; set; }

            public int NumeroPagina { get; set; }

            public int CantidadElementos { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {

            /// <summary>
            /// 
            /// </summary>
            private readonly IPaginacion _paginacionRepository;



            public Manejador(IPaginacion paginacionRepository)
            {
                _paginacionRepository = paginacionRepository;
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var storeProcedure = "usp_obtener_curso_paginacion";
                var ordenamiento = "Titulo";
                var parametros = new Dictionary<string, object>();
                parametros.Add("NombreCurso", request.Titulo);


                return await _paginacionRepository.devolverPaginacion(storeProcedure,
                                                                request.NumeroPagina,
                                                                request.CantidadElementos,
                                                                parametros,
                                                                ordenamiento);




            }
        }



    }
}
