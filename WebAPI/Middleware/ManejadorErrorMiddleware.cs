using Aplicacion.src.ManejadorErrores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<ManejadorErrorMiddleware> _logger;




        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await ManejadorExcepcionAsincrono(context, ex, _logger);
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;


            switch (ex)
            {
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";


            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }


        }

    }
}
