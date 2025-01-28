using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;
using System.Collections.Generic;

namespace ShopManagment.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productrepository;

        public ProductApplication(IProductRepository repository)
        {
            _productrepository = repository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productrepository.Exsists(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Slug, command.Keyword
                , command.MetaDescription, command.CategoryId);
            _productrepository.Create(product);
            _productrepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productrepository.Get(command.Id);
            if ((product == null))
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_productrepository.Exsists(x=>x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Slug, command.Keyword
                , command.MetaDescription, command.CategoryId);
            _productrepository.SaveChanges();
            return operation.Succedded();


        }


        public EditProduct GetDetails(long id)
        {
            return _productrepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productrepository.GetProducts();
        }

        public OperationResult IsInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productrepository.Get(id);
            if ((product == null))
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.InStock();
            _productrepository.SaveChanges();
            return operation.Succedded();
        }
        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productrepository.Get(id);
            if ((product == null))
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.NotInStock();
            _productrepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchmodel)
        {
            return _productrepository.Search(searchmodel);
        }
    }
}
