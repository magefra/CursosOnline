﻿using Dominio.src;
using FluentValidation;
using MediatR;
using Persistencia.src.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.src.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            [Required(ErrorMessage ="Por favor ingrese el Título del curso")]
            public string Titulo { get; set; }


            public string Descripcion { get; set; }

            public DateTime FechaPublicacion { get; set; }
        }



        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo)
                    .NotEmpty();

                RuleFor(x => x.Descripcion)
                    .NotEmpty();

                RuleFor(x => x.FechaPublicacion)
                    .NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {

            /// <summary>
            /// 
            /// </summary>
            private readonly CursosContext _cursosContext;



            public Manejador(CursosContext cursosContext)
            {
                _cursosContext = cursosContext;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso()
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };


                _cursosContext.Curso.Add(curso);
                var valor =  await _cursosContext.SaveChangesAsync();



                if (valor > 0)
                    return Unit.Value;


                throw new Exception("No se puede insertar el curso");

            }
        }
    }
}
