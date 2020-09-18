using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.src.DapperConexion
{
    public interface IFactoryConnection
    {
        /// <summary>
        /// 
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
