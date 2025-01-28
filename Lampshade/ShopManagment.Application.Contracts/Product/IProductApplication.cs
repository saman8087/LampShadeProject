using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);

        OperationResult IsInStock(long id);
        OperationResult NotInStock(long id);

        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();

        List<ProductViewModel> Search(ProductSearchModel searchmodel);


    }
}
