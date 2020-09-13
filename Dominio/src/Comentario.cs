using System;

namespace Dominio.src
{
    public class Comentario
    {
        public Guid ComentarioId { get; set; }

        public string Alumna { get; set; }

        public int Puntaje { get; set; }

        public string ComentarioTexto { get; set; }

        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}
