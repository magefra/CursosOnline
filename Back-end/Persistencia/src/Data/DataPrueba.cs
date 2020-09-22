using Dominio.src;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace Persistencia.src.Data
{
    public class DataPrueba
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="usuarioManager"></param>
        /// <returns></returns>
        public static async Task Insert(CursosContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario {
                    NombreCompleto = "Magdiel Efrain",
                    UserName = "Magdiel",
                    Email = "magefra9@hotmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}
