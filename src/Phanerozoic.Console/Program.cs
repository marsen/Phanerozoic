using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Services;

namespace Phanerozoic.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICoverageProcessor, CoverageProcessor>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var reportEntity = new ReportEntity
            {
                FilePath = args[0]
            };

            var coverageProcessor = serviceProvider.GetService<ICoverageProcessor>();
            coverageProcessor.Process(reportEntity);

            serviceProvider.Dispose();
        }
    }
}