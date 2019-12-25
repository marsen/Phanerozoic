using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services;

namespace Phanerozoic.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICoverageProcessor, CoverageProcessor>();
            serviceCollection.AddScoped<IFileHelper, FileHelper>();
            serviceCollection.AddScoped<IReportParser, DotCoverParser>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var reportEntity = new ReportEntity
            {
                FilePath = args[0]
            };
            var coverageEntity = new CoverageEntity
            {
                FilePath = args[1]
            };

            var coverageProcessor = serviceProvider.GetService<ICoverageProcessor>();
            coverageProcessor.Process(reportEntity, coverageEntity);

            serviceProvider.Dispose();
        }
    }
}