using System;
using CloudProvider.SDK.Abstract;
using Microsoft.Extensions.DependencyInjection;
using CloudProvider.SDK.Service;
using CloudProvider.SDK.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CloudProvider.SDK.Common
{
    public static class SdkExtensions
    {
        public static void AddCloudProviderServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.Configure<CloudConfiguration>(configuration.GetSection("Cloud"));

            services.AddSingleton<CloudConfiguration>(instance => instance.GetRequiredService<IOptions<CloudConfiguration>>().Value);

            services.AddLogging();

            services.RegisterDependencies(typeof(IFileManager));
        }
    }
}
