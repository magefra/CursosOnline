﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.src.DapperConexion.Instructores
{
    public class InstructorRepositorio : IInstructor
    {

        /// <summary>
        /// 
        /// </summary>
        private IFactoryConnection _factoryConnection;



        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public async Task<int> Actualiza(Guid instructorId,
                                   string nombre,
                                   string apellidos,
                                   string grado)
        {
            var storeProcedure = "usp_instructor_editar";

            try
            {
                var connection = _factoryConnection.GetConnection();

                var resultado = await connection.ExecuteAsync(
                      storeProcedure,
                      new
                      {
                          InstructorId = instructorId,
                          Nombre = nombre,
                          Apellidos = apellidos,
                          Grado = grado
                      },
                      commandType: System.Data.CommandType.StoredProcedure
                      );


                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo editar el  instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Eliminar(Guid id)
        {
            try
            {
                var storeProcedure = "usp_instructor_elimina";
                var connection = _factoryConnection.GetConnection();

                var result = await connection.ExecuteAsync(
                        storeProcedure,
                        new
                        {
                            InstructorId = id
                        },
                        commandType: System.Data.CommandType.StoredProcedure
                        );


                return result;

            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo eliminar el instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public async Task<int> Nuevo(string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_nuevo";

            try
            {
                var connection = _factoryConnection.GetConnection();

                var result = await connection.ExecuteAsync(storeProcedure,
                 new
                 {
                     InstructorId = Guid.NewGuid(),
                     Nombre = nombre,
                     Apellidos = apellidos,
                     Grado = titulo
                 },
                 commandType: System.Data.CommandType.StoredProcedure
                 );


                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar el nuevo instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.QueryAsync<InstructorModel>(
                                                              storeProcedure,
                                                              null,
                                                              commandType: System.Data.CommandType.StoredProcedure);


                if (result != null)
                {
                    instructorList = result;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Error en la consulta de datos", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return instructorList;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async  Task<InstructorModel> ObtenerPorId(Guid id)
        {
            try
            {
                var storeProcedure = "usp_obtener_instructor_por_id";
                var connection = _factoryConnection.GetConnection();

                var restult =await connection.QueryFirstOrDefaultAsync<InstructorModel>(
                    storeProcedure,
                    new
                    {
                        InstructorId = id
                    },
                    commandType : System.Data.CommandType.StoredProcedure
                    );

                return restult;
            }
            catch (Exception ex)
            {


                throw new Exception("No se encontró el instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }
    }
}
