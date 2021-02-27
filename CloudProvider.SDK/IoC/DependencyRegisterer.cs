using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CloudProvider.SDK.IoC
{
    public static class DependencyRegisterer
    {
        public static void RegisterDependencies(this IServiceCollection services, Type objectType)
        {
            var query = 
                from 
                    t in objectType.Assembly.GetTypes()
                where 
                    t.GetTypeInfo().IsClass && 
                    !t.GetTypeInfo().IsAbstract && 
                    t.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(IInjectable))
                select t;

            var orderedQuery = query.OrderBy(p => p.Name).ToList();

            foreach (var type in orderedQuery)
            {
                Type interfaceType = null;

                foreach (var it in type.GetInterfaces())
                {
                    if (it.GetInterfaces().All(i => i != typeof(IInjectable))) 
                        continue;
                    
                    interfaceType = it;
                }

                services.AddScoped(interfaceType, type);
            }

        }
    }
}
