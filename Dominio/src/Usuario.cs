using Microsoft.AspNetCore.Identity;

namespace Dominio.src
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
