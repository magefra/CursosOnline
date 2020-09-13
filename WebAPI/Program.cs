using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.src;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia.src.Data;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var hostServer = CreateHostBuilder(args).Build();

            using(var ambiente = hostServer.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    var context = services.GetRequiredService<CursosContext>();
                    context.Database.Migrate();

                    DataPrueba.Insert(context, userManager).Wait();
                }
                catch(Exception ex)
                {
                    var loggin = services.GetRequiredService<ILogger<Program>>();
                    loggin.LogError(ex,"Ocurrio un error en la migración");
                }

            }


            hostServer.Run();



        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
