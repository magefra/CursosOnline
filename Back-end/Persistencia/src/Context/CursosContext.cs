﻿using Dominio.src;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.src.Data
{
    public class CursosContext : IdentityDbContext<Usuario>
    {
        public CursosContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });
        }


        public DbSet<Comentario> Comentario { get; set; }

        public DbSet<Curso> Curso { get; set; }

        public DbSet<CursoInstructor> CursoInstructor { get; set; }

        public DbSet<Instructor> Instructor { get; set; }

        public DbSet<Precio> Precio { get; set; }
    }
}
