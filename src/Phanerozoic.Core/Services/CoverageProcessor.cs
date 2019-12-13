using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;

namespace Phanerozoic.Core.Services
{
    public class CoverageProcessor : ICoverageProcessor
    {
        private readonly IFileHelper _fileHelper;
        private readonly IReportParser _reportParser;

        public CoverageProcessor(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();
            this._reportParser = serviceProvider.GetRequiredService<IReportParser>();
        }

        public void Process(ReportEntity reportEntity)
        {
            if (this._fileHelper.Exists(reportEntity.FilePath))
            {
                Console.WriteLine("Not Found");
            }
            Console.WriteLine($"Run {reportEntity.FilePath}");
        }
    }
}