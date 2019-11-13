using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    ///*An interface to manage Order, allow Dependency Injection
    public interface IImportOrderRepository
    {
        IQueryable<ImportOrder> ImportOrders { get; }
        void SaveOrder(ImportOrder importOrder);
    }
}
