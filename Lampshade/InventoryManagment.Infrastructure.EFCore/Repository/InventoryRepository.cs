using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagment.Application.Contract.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using InventoryManagment.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagment.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext) 
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }


        public Inventory GetBy(long ProductId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public EditInventory GetDatails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x=>x.Id == inventoryId);
            return inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Count = x.Count,
                Description = x.Description,
                Operation = x.Operation,
                CurrentCount = x.CurrentCount,
                OperaionDate = x.OperaionDate.ToFarsi(),
                Operator = "System Admin",
                OperatorId = x.OperatorId,
                OrderId = x.OrderId
                
            }).OrderByDescending(x=>x.Id).ToList();

            
        }
      

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()

            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;

        }
     
    }
}
