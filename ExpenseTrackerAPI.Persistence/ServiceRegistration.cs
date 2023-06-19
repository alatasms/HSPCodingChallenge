using ExpenseTrackerAPI.Application.Abstraction.Services.Identity;
using ExpenseTrackerAPI.Application.Repositories;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using ExpenseTrackerAPI.Persistence.Context;
using ExpenseTrackerAPI.Persistence.Repositories;
using ExpenseTrackerAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpenseTrackerAPIDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")), ServiceLifetime.Scoped);


                services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                    options.SignIn.RequireConfirmedEmail = false;

                })
                .AddEntityFrameworkStores<ExpenseTrackerAPIDBContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
