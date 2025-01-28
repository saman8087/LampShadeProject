using DiscountManagement.Application;
using DiscountManagment.Application;
using DiscountManagment.Application.Contracts.ColeagueDiscount;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using DiscountManagment.Domain.CustomerDiscountAgg;
using DiscountManagment.Infrastructure.EFCore;
using DiscountManagment.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountApplication,ColleagueDiscountApplication>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();



            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
