using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //ORM framework presents the tables, columns, and rows of a relational database through regular C# objects
    //Entity Framework Core is object-relational mapping (ORM) framework
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext context;
        public EFEmployeeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        //load data from SQL sever and convert it into Product object hidden from Product Controller class
        //which just receives an object that implements the IProductRepository interface
        //and works with the data it provides
        public IQueryable<Employee> Employees => context.Employees;
        // adds a product to the repository if the ProductID is 0;
        //otherwise, it applies any changes to the existing entry in the database using Entity Framework
        public void SaveEmployee(Employee employee)
        {
            //EF create ProductID that is not 0
            if (employee.EmployeeID == 0)
            {
                context.Employees.Add(employee);
            }
            //locate the corresponding object that Entity Framework Core does know about and
            //update it explicitly
            else
            {
                Employee dbEntry = context.Employees
                    .FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
                if (dbEntry != null)
                {
                    dbEntry.Name = employee.Name;
                    dbEntry.JoinDate = employee.JoinDate;
                    dbEntry.Salary = employee.Salary;
                }
            }
            context.SaveChanges();
        }
        public Employee DeleteEmployee(int employeeID)
        {
            //Remove Product object from DB using EF
            Employee dbEntry = context.Employees
                .FirstOrDefault(e => e.EmployeeID == employeeID);
            if (dbEntry != null)
            {
                context.Employees.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
