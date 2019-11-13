using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class SessionImportItems : ImportItems
    {
        public static ImportItems GetImportItems(IServiceProvider services)
        {
            //get current session
            ISession session = services
                .GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            //get import items from current session
            SessionImportItems importItems = session?
                .GetJson<SessionImportItems>("ImportItems")
                ?? new SessionImportItems();
            importItems.Session = session;
            return importItems;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("ImportItems", this);
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("ImportItems", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("ImportItems");
        }
    }
}
