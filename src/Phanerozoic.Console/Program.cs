﻿using Microsoft.Extensions.Configuration;
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
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
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

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICoverageProcessor, CoverageProcessor>();
            serviceCollection.AddScoped<IFileHelper, FileHelper>();
            serviceCollection.AddScoped<IReportParser, DotCoverParser>();
            serviceCollection.AddScoped<ICoverageUpdater, GoogleSheetsUpdater>();
            serviceCollection.AddScoped<IGoogleSheetsService, GoogleSheetsService>();

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configurationRoot);
        }
    }
}