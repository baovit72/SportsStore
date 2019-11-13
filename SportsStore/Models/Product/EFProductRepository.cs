using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //ORM framework presents the tables, columns, and rows of a relational database through regular C# objects
    //Entity Framework Core is object-relational mapping (ORM) framework
    public class EFProductRepository:IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        //load data from SQL sever and convert it into Product object hidden from Product Controller class
        //which just receives an object that implements the IProductRepository interface
        //and works with the data it provides
        public IQueryable<Product> Products => context.Products;
        // adds a product to the repository if the ProductID is 0;
        //otherwise, it applies any changes to the existing entry in the database using Entity Framework
        public void SaveProduct(Product product)
        {
            //EF create ProductID that is not 0
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            //locate the corresponding object that Entity Framework Core does know about and
            //update it explicitly
            else
            {
                Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int productID)
        {
            //Remove Product object from DB using EF
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
