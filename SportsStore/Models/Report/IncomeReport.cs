using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class IncomeReport
    {
        [Key]
        public int ReportID { get; set; }
        //SQL constraints is not enough protection for the db
        //Validation using MVC is needed
        [Required(ErrorMessage = "Error report date")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Error products sale income")]
        public decimal SaleIncome { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Error imports cost")]
        public decimal ImportCost { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Error employees' salaries")]
        public decimal EmployeeSalaries { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Error profit")]
        public decimal Profit { get; set; }

        public decimal CalculateProfit()
        {
            Profit = SaleIncome - (ImportCost + EmployeeSalaries);
            return SaleIncome - (ImportCost + EmployeeSalaries);
        }
    }
}
