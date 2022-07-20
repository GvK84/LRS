using Microsoft.Extensions.Configuration;

namespace BackEnd.Data
{
    public class ConnectionService
    {
        // TODO is this needed? you already using services.AddDbContext below and could use Configuration.GetConnectionString("LRSDatabase") there
        public static string connstring = "";
        /// <summary>Sets the specified configuration.</summary>
        /// <param name="config">The configuration.</param>
        /// <returns>Database configuration string</returns>
        public static string Set(IConfiguration config)
        {
            return connstring = config.GetConnectionString("LRSDatabase");
        }
    }
}