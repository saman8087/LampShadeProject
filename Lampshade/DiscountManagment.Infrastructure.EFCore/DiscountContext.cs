using DiscountManagment.Domain.ColeagureDiscountAgg;
using DiscountManagment.Domain.CustomerDiscountAgg;
using DiscountManagment.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagment.Infrastructure.EFCore
{
    public class DiscountContext: DbContext
    {
        public DbSet<CustomerDiscount>  CustomerDiscounts {  get; set; }
        public DbSet<ColleagueDiscount>  coleagueDiscounts { get; set; }
        public DiscountContext(DbContextOptions<DiscountContext> options): base(options)
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }


    }
}
