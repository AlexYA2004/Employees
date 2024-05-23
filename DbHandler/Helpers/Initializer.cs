using DbHandler.Services.ConcrateServices;
using DbHandler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Helpers
{
    public static class Initializer
    {
        public static IServiceProvider InitializeApp(this IServiceCollection services, string DbConnection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(DbConnection));

            services.AddScoped<IEmployeeService, EmployeeService>();

            return services.BuildServiceProvider();
        }
    }
}
