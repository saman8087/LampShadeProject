using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagment.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exsists(x => x.Picture == command.Picture && x.ProductId == command.ProductId)) 
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var productpicture = new ProductPicture(command.ProductId, command.Picture,
                command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productpicture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(command.Id);
            if (productpicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_productPictureRepository.Exsists(x => x.Picture == command.Picture &&
            x.ProductId == command.ProductId && x.Id == command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            productpicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
           
            productpicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productpicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
