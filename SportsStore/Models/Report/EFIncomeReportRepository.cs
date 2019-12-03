using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //Similar to EFProductRepository
    public class EFIncomeReportRepository : IIncomeReportRepository
    {

        private ApplicationDbContext context;
        public EFIncomeReportRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        //        specify that when an Order object is read from the
        //database, the collection associated with the Lines property should also be loaded along with each
        //Product object associated with each collection object
        public IQueryable<IncomeReport> IncomeReports => context.IncomeReports;
        public void SaveReport(IncomeReport incomeReport)
        {
            //EF create ProductID that is not 0
            if (incomeReport.ReportID == 0)
            {
                context.IncomeReports.Add(incomeReport);
            }
            //locate the corresponding object that Entity Framework Core does know about and
            //update it explicitly
            else
            {
                IncomeReport dbEntry = context.IncomeReports
                    .FirstOrDefault(r => r.ReportID == incomeReport.ReportID);
                if (dbEntry != null)
                {
                    dbEntry.CreateDate = incomeReport.CreateDate;
                    dbEntry.EmployeeSalaries = incomeReport.EmployeeSalaries;
                    dbEntry.ImportCost = incomeReport.ImportCost;
                    dbEntry.SaleIncome = incomeReport.SaleIncome;
                    dbEntry.Profit = incomeReport.CalculateProfit();
                }
            }
            context.SaveChanges();
        }
    }
}
