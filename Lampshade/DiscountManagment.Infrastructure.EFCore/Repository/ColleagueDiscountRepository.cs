using _0_Framework.Infrastructure;
using DiscountManagment.Application.Contracts.ColeagueDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using DiscountManagment.Domain.ColeagureDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopConterxt _shopConterxt;
       

        public ColleagueDiscountRepository(DiscountContext context, ShopConterxt shopConterxt) : base(context)
        {
            _context = context;
            _shopConterxt= shopConterxt;
        }
        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.coleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel search)
        {
            var products = _shopConterxt.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.coleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToString(),
                IsRemoved = x.IsRemoved

            });
            if (search.ProductId > 0)
                query = query.Where(x => x.ProductId == search.ProductId);
            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discounts => discounts.Product = products.FirstOrDefault(x => x.Id
             == discounts.Id)?.Name);
            return discounts;
        }

    }
}
