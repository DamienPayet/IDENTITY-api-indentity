using Di2P1G3.Authentication.Core.Interfaces;
using Di2P1G3.Authentication.Core.Services;
using Di2P1G3.Authentication.Infrastructure;
using Di2P1G3.Authentication.Infrastructure.Data;
using Di2P1G3.Authentication.SharedKernel.Interfaces;
using Di2P1G3.Dependency.Injection;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Di2P1G3.Authentication.Core
{
    public static class DiContainer
    {
        public static void BuildContainer()
        {
            var logger = new LoggerConfiguration().Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .Enrich.WithMemoryUsage().CreateLogger();

            ServiceContainerBuilder.Instance.RegisterTransient<IUserRepository, UserRepository>();
            ServiceContainerBuilder.Instance.RegisterTransient<IUserService, UserService>();
            ServiceContainerBuilder.Instance.RegisterTransient<IClaimService, ClaimService>();
            ServiceContainerBuilder.Instance.RegisterTransient<ITokenRepository, TokenRepository>();
            ServiceContainerBuilder.Instance.RegisterTransient<ITokenService, TokenService>();
            ServiceContainerBuilder.Instance.RegisterTransient<IClientApplicationRepository, ClientApplicationRepository>();
            ServiceContainerBuilder.Instance.RegisterTransient<IClientApplicationService, ClientApplicationService>();
            ServiceContainerBuilder.Instance.RegisterTransient<DbContext, AppDbContext>();
            ServiceContainerBuilder.Instance.RegisterSingleton<ILogger>(logger);
            ServiceContainerBuilder.Instance.RegisterSingleton<IMapper>(new Mapper(TypeAdapterConfig.GlobalSettings));
        }
    }
}