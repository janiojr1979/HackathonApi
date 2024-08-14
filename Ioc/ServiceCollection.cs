using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.RavenDBProvider.Lib;
using Domain.Interfaces.Mapping;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Configs;
using Domain.Services;
using Infra.Mapping;
using Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ioc
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBasicContractProvider(ServiceLifetime.Scoped);
            services.AddRavenDBProvider(configuration).AddRepositoryRavenDB().InitializeAll();
            services.AddMapper();
            services.AddServices();
            services.AddConfigurations(configuration);

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection builder)
        {
            builder.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.AddScoped<IMappingAdapter, MappingAdapter>();

            return builder;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(c => configuration.GetSection("ApiConfig").Get<ApiConfig>());

            return services;
        }

        private static IRavenDBBuilder AddRepositoryRavenDB(this IRavenDBBuilder builder)
        {
            builder.AddScopedRavenDBRepository<IRepositoryBase, RepositoryBase>(0, (builder, store) =>
            {
                return new RepositoryBase(store, builder.GetRequiredService<INotificationProvider>());
            });

            return builder;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(provider => provider.GetRequiredService<INotificationProvider>().CreateNotification());
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
