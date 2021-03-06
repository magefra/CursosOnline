﻿using System;
using System.Collections.Generic;

namespace Dominio.src
{
    public class Curso
    {
        public Guid CursoId { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public byte[] FotoPortada { get; set; }


        public Precio PrecioPromocion { get; set; }


        public DateTime? FechaCreacion { get; set; }


        public List<Comentario> Comentarios { get; set; }


        public ICollection<CursoInstructor> CursosInstructores { get; set; }
    }
}
