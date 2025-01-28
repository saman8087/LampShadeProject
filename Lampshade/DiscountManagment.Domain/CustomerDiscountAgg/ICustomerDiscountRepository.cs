using _0_Framework.Domain;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using System.Collections.Generic;

namespace DiscountManagment.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);
    }
}
