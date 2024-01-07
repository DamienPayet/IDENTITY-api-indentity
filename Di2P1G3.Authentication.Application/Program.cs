using Di2P1G3.Authentication.Core;
using Di2P1G3.Authentication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Di2P1G3.Authentication.Api
{
    public class Program
    {
        public static void Main()
        {
            DiContainer.BuildContainer();

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}