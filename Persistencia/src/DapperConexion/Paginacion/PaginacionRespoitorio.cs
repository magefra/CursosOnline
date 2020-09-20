using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Persistencia.src.DapperConexion.Paginacion
{
    public class PaginacionRespoitorio : IPaginacion
    {


        /// <summary>
        /// 
        /// </summary>
        private IFactoryConnection _factoryConnection;



        public PaginacionRespoitorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="cantidadElementos"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure,
                                                        int numeroPagina,
                                                        int cantidadElementos,
                                                        IDictionary<string, object> parametrosFiltro,
                                                        string ordernamientoColumna)
        {
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters dynamicParameters = new DynamicParameters();


                foreach(var param in parametrosFiltro)
                {
                    dynamicParameters.Add("@" + param.Key, param.Value);
                }


                dynamicParameters.Add("@NumeroPagina", numeroPagina);
                dynamicParameters.Add("@CantidadElementos", cantidadElementos);
                dynamicParameters.Add("@Ordenamiento", ordernamientoColumna);

                dynamicParameters.Add("@TotalRecords",totalRecords, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                dynamicParameters.Add("@TotalPaginas", totalPaginas, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                
                
                var result =await connection.QueryAsync(storeProcedure,
                                        dynamicParameters,
                                        commandType: System.Data.CommandType.StoredProcedure);

                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.NumeroPaginas = dynamicParameters.Get<int>("@TotalPaginas");
                paginacionModel.TotalRecords = dynamicParameters.Get<int>("@TotalRecords");

            }
            catch (Exception)
            {

                throw new Exception("No se pudo ejecutar el procedimiento almacenado");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return paginacionModel;
        }
    }
}
