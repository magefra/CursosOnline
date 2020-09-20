using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.src.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<IDictionary<string, object>> ListaRecords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalRecords { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int NumeroPaginas { get; set; }


    }
}
