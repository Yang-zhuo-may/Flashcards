using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Data
{
    internal class ConfigHelper
    {
        private static IConfiguration config;

        static ConfigHelper()
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        }

        public static string GetConnectionString()
        {
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
