using Aplicacion.src.ManejadorErrores;
using AutoMapper;
using Dominio.src;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid CursoId { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
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
            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {

                var curso = await _cursosContext.Curso
                                    .Include(x => x.Comentarios)
                                    .Include(x => x.PrecioPromocion)
                                    .Include(x => x.CursosInstructores)
                                    .ThenInclude(i => i.Instructor)
                                    .FirstOrDefaultAsync(a => a.CursoId == request.CursoId);



                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontró el curso" });
                }


                var cursoDto = _mapper.Map<Curso,CursoDto>(curso);


                return cursoDto;
            }
        }

    }
}
