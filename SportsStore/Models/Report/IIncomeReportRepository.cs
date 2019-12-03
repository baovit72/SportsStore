using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    ///*An interface to manage Order, allow Dependency Injection
    public interface IIncomeReportRepository
    {
        IQueryable<IncomeReport> IncomeReports { get; }
        void SaveReport(IncomeReport incomeReport);
    }
}
