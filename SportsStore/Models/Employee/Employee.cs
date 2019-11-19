using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace SportsStore.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        //SQL constraints is not enough protection for the db
        //Validation using MVC is needed
        [Required(ErrorMessage = "Please enter the employee's name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the date the employee starts working")]
        public DateTime JoinDate { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive salary")]
        public decimal Salary { get; set; }

    }
}
