using Aplicacion.src.Cursos;
using AutoMapper;
using Dominio.src;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.src.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Curso, CursoDto>().ReverseMap();

            CreateMap<CursoInstructor, CursoInstructorDto>().ReverseMap();

            CreateMap<Instructor, InstructorDto>().ReverseMap();
        }
    }
}
