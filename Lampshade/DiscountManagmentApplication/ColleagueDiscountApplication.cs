using _0_Framework.Application;
using DiscountManagment.Application.Contracts.ColeagueDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using DiscountManagment.Domain.ColeagureDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagment.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _coleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository coleagueDiscountRepository)
        {
            _coleagueDiscountRepository = coleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();

            if (_coleagueDiscountRepository.Exsists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var colleagediscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _coleagueDiscountRepository.Create(colleagediscount);
            _coleagueDiscountRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleageDicount = _coleagueDiscountRepository.Get(command.Id);
            if (colleageDicount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_coleagueDiscountRepository.Exsists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && 
            x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            colleageDicount.Edit(command.ProductId, command.DiscountRate);
            _coleagueDiscountRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _coleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleageDicount = _coleagueDiscountRepository.Get(id);
            if (colleageDicount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleageDicount.Remove();
            _coleagueDiscountRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleageDicount = _coleagueDiscountRepository.Get(id);
            if (colleageDicount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleageDicount.Restore();
            _coleagueDiscountRepository.SaveChanges();
            return operation.Succedded();

        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel search)
        {
            return _coleagueDiscountRepository.Search(search);
        }
    }
}
