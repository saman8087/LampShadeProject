using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using DiscountManagment.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DiscountManagment.Infrastructure.EFCore.Repository
{

    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopConterxt _shopContext;
        public CustomerDiscountRepository(DiscountContext context, ShopConterxt shopcontext) : base(context)
        {
            _context = context;
            _shopContext = shopcontext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount{
             Id = x.Id,
             ProductId = x.ProductId,
             DiscountRate = x.DiscountRate,
             StartDate = x.StartDate.ToString() ,
             EndDate = x.EndDate.ToString() ,
             Reason = x.Reason         
                      
            }).FirstOrDefault( x => x.Id == id );
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                ProductId = x.ProductId,
                Reason = x.Reason

            });
            if (search.ProductId > 0)
                query = query.Where(x => x.ProductId == search.ProductId);
            if(!string.IsNullOrWhiteSpace(search.StartDate))
            {
                query = query.Where(x => x.StartDateGr > search.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(search.EndDate))
            {
                query = query.Where(x => x.EndDateGr > search.EndDate.ToGeorgianDateTime());
            }
            var discounts= query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount => discount.Product = 
            products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);
                
            return discounts;


        }
    }
}
