using _0_Framework.Domain;
using InventoryManagment.Application.Contract.Inventory;
using System.Collections.Generic;


namespace InventoryManagment.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory >
    {
        EditInventory GetDatails(long id);
        Inventory GetBy(long ProductId);

        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    } 
}
