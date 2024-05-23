using Microsoft.Extensions.Configuration;

namespace DbHandler.Helpers
{
    public static class Configurator
    {
        public static string ConfigureApp(this IConfiguration configuration)
        {
            return configuration.GetSection("ConnectionString").Value;
        }
    }
}
