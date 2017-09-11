using System;
using CustomerInvitation.Service;
using CustomerInvitation.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInvitation
{
    class Program
    {
        /// <summary>
        /// Initialize the dependency injection container
        /// </summary>
        /// <returns>service providers</returns>
        static IServiceProvider InitializeDIContainer() {
            var services = new ServiceCollection();
            services.AddTransient<IGeographicDistance, GeographicDistance>();
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IInvitation, Invitation>();
            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var provider = InitializeDIContainer();
            var invitation = provider.GetService<IInvitation>();
            
            invitation.ListCustomersToInviteWithinDistance("./Resource/customerList.json", 100);
        }

        
    }
}
