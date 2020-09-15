using Aplicacion.src.Contratos;
using Dominio.src;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Seguridad.src.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string crearToken(Usuario usuario)
        {
            var claims = new List<Claim>() {
               new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));


            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var tokenDesripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();

            var token = tokenManejador.CreateToken(tokenDesripcion);

            return tokenManejador.WriteToken(token);

        }
    }
}
