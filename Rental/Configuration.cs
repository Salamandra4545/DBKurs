using System;
using System.IO;
using Newtonsoft.Json;

namespace Rental
{
    public class Configuration
    {
        public static string GetConnectionString()
        {
            // Используем относительный путь
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("Файл appsettings.json не найден.");

            string jsonContent = File.ReadAllText(jsonPath);
            var config = JsonConvert.DeserializeObject<AppSettings>(jsonContent);

            return config?.ConnectionStrings?.DefaultConnection
                ?? throw new InvalidOperationException("Строка подключения не найдена.");
        }

        public class AppSettings
        {
            public ConnectionStrings ConnectionStrings { get; set; }
        }

        public class ConnectionStrings
        {
            public string DefaultConnection { get; set; }
        }
    }
}