using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace WyBlog.Web
{
    public class Program
    {
        //public static async Task Main(string[] args)
        //{
        //    await WebHost.CreateDefaultBuilder(args)
        //                 .UseStartup<Startup>()
        //                 .UseNLog()
        //                 .Build()
        //                 .RunAsync();
        //}

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseNLog();

    }
}
