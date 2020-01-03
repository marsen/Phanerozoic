using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Services;
using System.IO;

namespace Phanerozoic.Duplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Duplicate(serviceProvider);

            serviceProvider.Dispose();
        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGoogleSheetsService, GoogleSheetsService>();

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("AppSettings.json.user", true, true)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configurationRoot);
        }

        public static void Duplicate(ServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();

            var sourceId = configuration["Source:Google.Sheets.SheetsId"];
            var targetId = configuration["Target:Google.Sheets.SheetsId"];

            //// Read
            var sourceList = googleSheetsService.GetValues(sourceId, "Coverage!A2:F100");

            //// Write
            var targetRange = $"Coverage!A2:F{sourceList.Count + 1}";
            googleSheetsService.SetValue(targetId, targetRange, sourceList);
        }
    }
}