using _0_Framework.Infrastructure;
using InventoryManagment.Application.Contract.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagment.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopConterxt _shopContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopConterxt shopContext) : base(inventoryContext) 
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }


        public Inventory GetBy(long ProductId)
        {
            return _inventoryContext.inventory.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public EditInventory GetDatails(long id)
        {
            return _inventoryContext.inventory.Select(x => new EditInventory
            {
                Id = id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount()
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
           
            if (!searchModel.InStock)
                query = query.Where(x => !x.InStock);
            var inventory = query.OrderByDescending(x=>x.Id).ToList();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);


            return inventory;
                 
        }
    }
}
