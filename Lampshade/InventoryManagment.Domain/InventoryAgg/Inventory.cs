using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagment.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;           
        }
        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }
        public void Increase(long count, long opratorid, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InvetoryOperation(true, count, opratorid, currentCount, description, 0, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(long count, long opratorid, string description, long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InvetoryOperation(false, count, opratorid, currentCount, description, orderId, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }


        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InvetoryOperation> Operations { get; private set; }

    }
}



