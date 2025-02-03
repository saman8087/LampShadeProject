using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagment.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {            
               Id = x.Id,
               Name = x.Name,        
                        
            }).ToList();
        }  
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });
            if( !string.IsNullOrWhiteSpace(searchModel.Name))
                query=query.Where( x=> x.Name.Contains(searchModel.Name) );
            return query.OrderByDescending(x => x.Id).ToList();
        }

    

        EditProductCategory IProductCategoryRepository.GetDetails(long id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Description = x.Description,
                Slug = x.Slug,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Name = x.Name,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
