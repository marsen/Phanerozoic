﻿using System;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly IFileHelper _fileHelper;
        private readonly IReportParser _reportParser;
        private readonly ICoverageUpdater _coverageUpdater;
        private readonly INotifyer _slackNotifyer;
        private readonly INotifyer _emailNotifyer;
        private readonly ICoverageLogger _coverageLogger;

        public CoverageProcessor(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();
            this._reportParser = serviceProvider.GetRequiredService<IReportParser>();
            this._coverageUpdater = serviceProvider.GetRequiredService<ICoverageUpdater>();
            this._coverageLogger = serviceProvider.GetRequiredService<ICoverageLogger>();
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
            this.GetSlackNotifyer().Notify(coverageEntity, updateMethodList);
            this.GetEmailNotifyer().Notify(coverageEntity, updateMethodList);

            //// Log
            Console.WriteLine("* Log");
            this._coverageLogger.Log(updateMethodList);
        }

        protected virtual INotifyer GetSlackNotifyer()
        {
            var notifyList = this._serviceProvider.GetServices<INotifyer>();
            return notifyList.FirstOrDefault(i => i is SlackNotifyer);
        }

        protected virtual INotifyer GetEmailNotifyer()
        {
            var notifyList = this._serviceProvider.GetServices<INotifyer>();
            return notifyList.FirstOrDefault(i => i is EmailNotifyer);
        }
    }
}