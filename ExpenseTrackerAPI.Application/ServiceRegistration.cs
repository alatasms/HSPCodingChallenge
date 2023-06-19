using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(a =>
            {
                a.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });
            services.AddAutoMapper(typeof(ServiceRegistration));
        }
    }
}
