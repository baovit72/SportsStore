using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class ImportItems
    {
        private List<ImportItemsLine> lineCollection = new List<ImportItemsLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            ImportItemsLine line = lineCollection
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new ImportItemsLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<ImportItemsLine> Lines => lineCollection;
    }
    public class ImportItemsLine
    {
        public int ImportItemsLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
