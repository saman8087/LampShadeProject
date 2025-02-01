using InventoryManagment.Application;
using InventoryManagment.Application.Contract.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using InventoryManagment.Infrastructure.EFCore;
using InventoryManagment.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventoryManagment.Infrastructure.Configure
{
    public class InvantoryManagmentBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            



            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
