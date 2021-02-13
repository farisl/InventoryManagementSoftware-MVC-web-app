using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Web.Helpers
{
    public static class Session
    {
        public static void Set<T>(this ISession session, string key, T entity) => session.SetString(key, JsonConvert.SerializeObject(entity));

        public static T Get<T>(this ISession session, string key) where T : class => !string.IsNullOrWhiteSpace(session.GetString(key)) ? JsonConvert.DeserializeObject<T>(session.GetString(key)) : null;

        public static class Keys
        {
            public static class Login
            {
                public static string User => nameof(User);
            }

        }
    }
}
