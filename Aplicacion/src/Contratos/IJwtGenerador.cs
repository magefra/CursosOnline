using Dominio.src;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.src.Contratos
{
    public interface IJwtGenerador
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string crearToken(Usuario usuario, List<string> roles);
    }
}
