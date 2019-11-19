using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository repository;
        public int PageSize = 4;
        public EmployeeController(IEmployeeRepository repo)
        {
            repository = repo;
        }
        //PAGINATION
        //public ViewResult List(int EmployeePage = 1)
        // => View(repository.Employees.OrderBy(p => p.EmployeeID)
        // .Skip((EmployeePage - 1) * PageSize).Take(PageSize));

        //Call the List.cshtl and passing Employee+Paging Info
        public ViewResult List() => View(repository.Employees);
        //View action for tag-helper asp-action="Edit"
        public ViewResult Edit(int employeeID) => View(repository.Employees
            .FirstOrDefault(e => e.EmployeeID == employeeID));
        //
        [HttpPost]
        public IActionResult Edit(Employee Employee)
        {
            //check validated
            if (ModelState.IsValid)
            {
                //if yes Save Employee and return View Index action
                repository.SaveEmployee(Employee);
                // TempDaya similar to Session and Viewbag but it is temporary, persists until is read by View
                //ViewBag only persists in the current HTTP request=>go to the new URL ViewBag will be lost
                //Session persists until explicit removed
                TempData["message"] = $"{Employee.Name} has been saved";
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(Employee);
            }
        }
        public ViewResult Create() => View("Edit", new Employee());
        [HttpPost]
        public IActionResult Delete(int EmployeeID)
        {
            Employee deletedEmployee = repository.DeleteEmployee(EmployeeID);
            if (deletedEmployee != null)
            {
                TempData["message"] = $"{deletedEmployee.Name} was deleted";
            }
            return RedirectToAction("List");
        }
    }
}