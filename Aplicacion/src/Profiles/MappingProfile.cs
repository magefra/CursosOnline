using Aplicacion.src.Cursos;
using AutoMapper;
using Dominio.src;
using Persistencia.src.DapperConexion.Instructores;
using System.Linq;

namespace Aplicacion.src.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Curso, CursoDto>()
                        .ForMember(x => x.Instructores,
                                   y=> y.MapFrom(z => z.CursosInstructores
                                                 .Select(a => a.Instructor).ToList()))
                        .ForMember(x => x.Comentarios,
                                   y => y.MapFrom(z => z.Comentarios))
                        .ForMember(x => x.Precio,
                                   y => y.MapFrom(z => z.PrecioPromocion))
                        .ReverseMap();



            CreateMap<CursoInstructor, CursoInstructorDto>().ReverseMap();

            CreateMap<IInstructor, InstructorDto>().ReverseMap();

            CreateMap<Comentario, ComentarioDto>();

            CreateMap<Precio, PrecioDto>();
        }
    }
}
