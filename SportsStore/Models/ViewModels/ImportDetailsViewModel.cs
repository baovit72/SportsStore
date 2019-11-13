using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    // Pass CartIndexViewModel object to the View that will display the contents of the cart
    public class ImportDetailsViewModel
    {
        public ImportItems ImportItems { get; set; }
        public ImportOrder ImportOrder { get; set; }
        public string ReturnUrl { get; set; }
    }
}
