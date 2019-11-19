using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /*An interface to manage Product, Queryable  
    allows a collection of objects to be queried efficiently*/
    //Allow Dependency Injection
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        void SaveEmployee(Employee employee);
        Employee DeleteEmployee(int employeeID);
    }
}
