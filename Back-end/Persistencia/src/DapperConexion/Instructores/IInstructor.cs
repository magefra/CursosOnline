using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.src.DapperConexion.Instructores
{
    public  interface IInstructor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<InstructorModel>> ObtenerLista();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InstructorModel> ObtenerPorId(Guid id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        Task<int> Nuevo(string nombre, string apellidos, string titulo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        Task<int> Actualiza(Guid instructorId,
                            string nombre,
                            string apellidos,
                            string grado);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Eliminar(Guid id);

    }
}
