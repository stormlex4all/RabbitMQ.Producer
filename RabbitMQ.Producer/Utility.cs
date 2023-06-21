using System.Reflection;
using System.Text.Json;

namespace RabbitMQ.Producer
{
    public static class Utility
    {
        private static string? ServiceName { get; set; }

        /// <summary>
        /// Gets the name of the containing assembly
        /// </summary>
        /// <returns></returns>
        public static string GetServiceName()
        {
            ServiceName ??= Assembly.GetExecutingAssembly().GetName().Name;
            return ServiceName;
        }

        /// <summary>
        /// Serialize json object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this object obj)
        {
            if(obj == null) { return null; }

            return JsonSerializer.Serialize(obj);
        }
    }
}
