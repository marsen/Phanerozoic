using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.IO;

namespace Phanerozoic.Core.Services
{
    public class CoverageProcessor : ICoverageProcessor
    {
        private readonly IFileHelper _fileHelper;
        private readonly IReportParser _reportParser;
        private readonly ICoverageUpdater _coverageUpdater;

        public CoverageProcessor(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();
            this._reportParser = serviceProvider.GetRequiredService<IReportParser>();
            this._coverageUpdater = serviceProvider.GetRequiredService<ICoverageUpdater>();
        }

        public void Process(ReportEntity reportEntity, CoverageEntity coverageEntity)
        {
            if (this._fileHelper.Exists(reportEntity.FilePath) == false)
            {
                Console.WriteLine("Not Found");
                throw new FileNotFoundException("File Not Found!", reportEntity.FilePath);
            }

            Console.WriteLine($"Run {reportEntity.FilePath}");

            var methodList = this._reportParser.Parser(reportEntity);

            this._coverageUpdater.Update(coverageEntity, methodList);

            //// Notify
        }
    }
}