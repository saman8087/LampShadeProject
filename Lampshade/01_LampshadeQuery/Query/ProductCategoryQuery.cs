using _01_LampshadeQuery.Contracts.ProductCategory;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopConterxt;

        public ProductCategoryQuery(ShopContext shopConterxt)
        {
            _shopConterxt = shopConterxt;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopConterxt.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug= x.Slug
            }).ToList();
        }
    }
}
