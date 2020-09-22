using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.src.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {

        /// <summary>
        /// 
        /// </summary>
        private IDbConnection _connection;


        /// <summary>
        /// 
        /// </summary>
        private readonly IOptions<ConexionConfiguracion> _configs;


        public FactoryConnection(IOptions<ConexionConfiguracion> configs)
        {
            _configs = configs;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configs.Value.DefaultConnection);
            }


            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }


            return _connection;
        }
    }
}
