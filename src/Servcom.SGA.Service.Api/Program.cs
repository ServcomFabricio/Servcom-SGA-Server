using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Servcom.SGA.Service.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }



        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                               //TODO:Liberar para produção    
                               //N.UseKestrel(opts => { opts.Listen(System.Net.IPAddress.Parse("0.0.0.0"), 5000); })
                               .UseKestrel()
                               .UseContentRoot(Directory.GetCurrentDirectory())
                               .UseIISIntegration()
                               .UseStartup<Startup>();

    }
}
