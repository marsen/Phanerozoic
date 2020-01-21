using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Services;
using System;
using System.Collections.Generic;
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

            var sourceId = configuration["Google:Sheets:SourceId"];
            var targetId = configuration["Google:Sheets:TargetId"];

            Console.WriteLine($"Source Sheets ID: {sourceId}");
            Console.WriteLine($"Target Sheets ID: {targetId}");
            var sheetName = "Coverage";

            //// Read
            var firstRow = 2;
            var maxRow = 100;
            var range = $"{sheetName}!A{firstRow}:G{maxRow}";
            var sourceList = googleSheetsService.GetValues(sourceId, range);
            var methodList = Program.SheetRangeToEntityList(sourceList);

            //// Write
            var targetRange = $"{sheetName}!A{firstRow}:G{sourceList.Count + 1}";
            var targetList = Program.EntityListToSheetRange(methodList);
            googleSheetsService.SetValue(targetId, targetRange, targetList);
        }

        private static List<MethodEntity> SheetRangeToEntityList(IList<IList<object>> sheetRange)
        {
            List<MethodEntity> methodList = new List<MethodEntity>();
            if (sheetRange != null && sheetRange.Count > 0)
            {
                foreach (var row in sheetRange)
                {
                    var methodEntity = new MethodEntity
                    {
                        Repository = row[0].ToString().Trim(),
                        Project = row[1].ToString().Trim(),
                        Class = row[2].ToString().Trim(),
                        Method = row[3].ToString().Trim(),
                        Coverage = int.Parse(row[4].ToString()),
                        Team = row[6].ToString().Trim(),
                        RawData = row,
                    };

                    if (string.IsNullOrWhiteSpace(methodEntity.Method) == false)
                    {
                        methodList.Add(methodEntity);
                    }
                }
            }

            return methodList;
        }

        private static IList<IList<object>> EntityListToSheetRange(List<MethodEntity> methodList)
        {
            var rowList = new List<IList<object>>();
            foreach (var method in methodList)
            {
                var row = method.RawData;
                rowList.Add(row);
            }

            return rowList;
        }
    }
}