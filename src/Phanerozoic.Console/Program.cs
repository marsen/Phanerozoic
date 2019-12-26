using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services;
using System.IO;

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
            serviceCollection.AddScoped<ICoverageUpdater, FileUpdater>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var reportEntity = new ReportEntity
            {
                FilePath = args[0]
            };

            var file = new FileInfo(reportEntity.FilePath);
            var fileName = file.Name;
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            var coverageEntity = new CoverageEntity
            {
                FilePath = Path.Combine(file.DirectoryName, $"{fileName}.csv")
            };

            var coverageProcessor = serviceProvider.GetService<ICoverageProcessor>();
            coverageProcessor.Process(reportEntity, coverageEntity);

            serviceProvider.Dispose();
        }
    }
}