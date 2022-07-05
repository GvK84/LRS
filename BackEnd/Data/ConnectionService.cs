using Microsoft.Extensions.Configuration;

namespace BackEnd.Data
{
    public class ConnectionService
    {
        public static string connstring = "";
        public static string Set(IConfiguration config)
        {
            return connstring = config.GetConnectionString("LRSDatabase");
        }
    }
}
