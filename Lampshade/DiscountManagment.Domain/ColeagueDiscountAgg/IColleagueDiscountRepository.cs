using _0_Framework.Domain;
using DiscountManagment.Application.Contracts.ColeagueDiscount;
using DiscountManagment.Domain.ColeagureDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagment.Domain.ColeagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel search);
    }
}
