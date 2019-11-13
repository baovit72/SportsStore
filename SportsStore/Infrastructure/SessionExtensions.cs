using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportsStore.Infrastructure
{
    //Session State stores only int, string, and byte[]
    public static class SessionExtensions
    {
        //serialize Cart objects into JSON
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
            //HttpContext.Session.SetJson("Cart", cart);
            //HttpContext.Session property returns an object that implements the ISession interface
        }
        //convert back
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
            //Cart cart = HttpContext.Session.GetJson<Cart>("Cart");
        }
    }
}
