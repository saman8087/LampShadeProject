using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Domain.ProductCategoryAgg;
using ShopManagment.Infrastructure.EFCore.Repository;
using System;
using System.Collections.Generic;

namespace ShopManagment.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exsists(x => x.Name == command.Name)) 
            {
                return operation.Failed("This record exists please try again");
            }
            var ProductCategory = new ProductCategory(command.Name, command.Title, command.Description, command.Picture,
               command.PictureAlt, command.PictureTitle, command.MetaDescription, command.Keywords, command.Slug);
            _productCategoryRepository.Create(ProductCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(Contracts.ProductCategory.EditProductCategory command)
        {
            var operation = new OperationResult();
            var productcategory = _productCategoryRepository.Get(command.Id);
            if (productcategory == null)
            {
                return operation.Failed("Record is empty try again");
            }
            if(_productCategoryRepository.Exsists(x=> x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed("This record exists please try again");
            }
            productcategory.Edit(command.Name, command.Title, command.Description, 
                command.Picture, command.PictureAlt, command.PictureTitle, command.MetaDescription, command.Keywords, command.Slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

    }
}
