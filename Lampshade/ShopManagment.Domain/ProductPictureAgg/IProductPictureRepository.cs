using _0_Framework.Domain;
using ShopManagment.Application.Contracts.ProductPicture;
using System.Collections.Generic;

namespace ShopManagment.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long , ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchmodel);

    }
}
