using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //Similar to EFProductRepository
    public class EFOrderRepository:IOrderRepository
    {

        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        //        specify that when an Order object is read from the
        //database, the collection associated with the Lines property should also be loaded along with each
        //Product object associated with each collection object
        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            //cart data is deserialied from sesssion=>create new cart object
            //=>not know to the EF, write all the objects into db=>alrealdy existed, causing error
            // notify EF that the objects exist and shouldn’t be stored in the database unless they are modified
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
