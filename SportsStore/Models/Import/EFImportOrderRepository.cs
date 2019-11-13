using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //Similar to EFProductRepository
    public class EFImportOrderRepository : IImportOrderRepository
    {

        private ApplicationDbContext context;
        public EFImportOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        //        specify that when an Order object is read from the
        //database, the collection associated with the Lines property should also be loaded along with each
        //Product object associated with each collection object
        public IQueryable<ImportOrder> ImportOrders => context.ImportOrders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        public void SaveOrder(ImportOrder importOrder)
        {
            //cart data is deserialied from sesssion=>create new cart object
            //=>not know to the EF, write all the objects into db=>alrealdy existed, causing error
            // notify EF that the objects exist and shouldn’t be stored in the database unless they are modified
            context.AttachRange(importOrder.Lines.Select(l => l.Product));
            if (importOrder.ImportOrderID == 0)
            {
                context.ImportOrders.Add(importOrder);
            }
            context.SaveChanges();
        }
    }
}
