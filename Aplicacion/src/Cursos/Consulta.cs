using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.src;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.src.Data;

namespace Aplicacion.src.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>>
        {


        }


        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
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
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos =  await _cursosContext.Curso
                                                .Include(x => x.Comentarios)
                                                .Include(x =>x.PrecioPromocion)
                                                .Include(x => x.CursosInstructores)
                                                .ThenInclude(x => x.Instructor)
                                                .ToListAsync();



                var cursoDTO = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);


                return cursoDTO;

            }
        }
    }
}