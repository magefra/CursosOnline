using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.src.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="cantidadElementos"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Task<PaginacionModel> devolverPaginacion(string storeProcedure,
                                                        int numeroPagina,
                                                        int cantidadElementos,
                                                        IDictionary<string, object> parametrosFiltro,
                                                        string ordernamientoColumna);



        

    }
}
