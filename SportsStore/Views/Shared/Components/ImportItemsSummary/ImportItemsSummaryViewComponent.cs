using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class ImportItemsSummaryViewComponent : ViewComponent
    {
        private ImportItems importItems;
        public ImportItemsSummaryViewComponent(ImportItems importItemsService)
        {
            importItems = importItemsService;
        }
        public IViewComponentResult Invoke()
        {
            return View(importItems);
        }
    }
}
