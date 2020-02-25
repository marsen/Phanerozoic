using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services.Notifications;

namespace Phanerozoic.Core.Services
{
    public class CoverageProcessor : ICoverageProcessor
    {
        private readonly IFileHelper _fileHelper;
        private readonly IReportParser _reportParser;
        private readonly ICoverageUpdater _coverageUpdater;
        private readonly INotifyer _slackNotifyer;
        private readonly INotifyer _emailNotifyer;
        private readonly ICoverageLogger _coverageLogger;

        public CoverageProcessor(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();
            this._reportParser = serviceProvider.GetRequiredService<IReportParser>();
            this._coverageUpdater = serviceProvider.GetRequiredService<ICoverageUpdater>();
            this._coverageLogger = serviceProvider.GetRequiredService<ICoverageLogger>();

            var notifyList = serviceProvider.GetServices<INotifyer>();
            this._slackNotifyer = notifyList.First(i => i is SlackNotifyer);
            this._emailNotifyer = notifyList.First(i => i is EmailNotifyer);
        }

        public void Process(ReportEntity reportEntity, CoverageEntity coverageEntity)
        {
            if (this._fileHelper.Exists(reportEntity.FilePath) == false)
            {
                Console.WriteLine($"File Not Found: {reportEntity.FilePath}");
                throw new FileNotFoundException("File Not Found!", reportEntity.FilePath);
            }

            //// Parser
            Console.WriteLine("* Parser");
            var methodList = this._reportParser.Parser(coverageEntity, reportEntity);

            //// Update
            Console.WriteLine("* Update");
            var updateMethodList = this._coverageUpdater.Update(coverageEntity, methodList);

            //// Notify
            Console.WriteLine("* Notify");
            this._slackNotifyer.Notify(coverageEntity, updateMethodList);
            this._emailNotifyer.Notify(coverageEntity, methodList);

            //// Log
            Console.WriteLine("* Log");
            this._coverageLogger.Log(updateMethodList);
        }
    }
}