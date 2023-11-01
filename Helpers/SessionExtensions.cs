using System.Text.Json;

namespace ASP_MVC.Helpers
{
    public static class SessionExtensions
    {
        /// HttpContext.Session.Set<T>(key,value)
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
