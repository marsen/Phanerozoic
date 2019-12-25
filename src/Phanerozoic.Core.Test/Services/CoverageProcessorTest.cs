using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Phanerozoic.Core.Test.Services
{
    public class CoverageProcessorTest
    {
        private readonly IServiceProvider _subServiceProvider;
        private readonly IFileHelper _subFileHelper;
        private readonly IReportParser _subReportParser;
        private readonly ICoverageUpdater _subCoverageUpdater;

        public CoverageProcessorTest()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();
            this._subReportParser = Substitute.For<IReportParser>();
            this._subCoverageUpdater = Substitute.For<ICoverageUpdater>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
            this._subServiceProvider.GetService<IReportParser>().Returns(this._subReportParser);
            this._subServiceProvider.GetService<ICoverageUpdater>().Returns(this._subCoverageUpdater);
        }

        [Fact(DisplayName = "Happy Path")]
        public void Test_Process_Flow()
        {
            //// arrange
            this._subFileHelper.Exists(Arg.Any<string>()).Returns(true);
            var reportEntity = new ReportEntity
            {
                FilePath = "report.json"
            };
            var coverageEntity = new CoverageEntity
            {
                FilePath = "coverage.csv"
            };

            var target = GetTarget();

            //// act
            target.Process(reportEntity, coverageEntity);

            //// assert
            this._subReportParser.Received(1).Parser(Arg.Any<ReportEntity>());
            this._subCoverageUpdater.Received(1).Update(Arg.Any<CoverageEntity>(), Arg.Any<List<MethodEntity>>());
        }

        [Fact(DisplayName = "檔案不存在")]
        public void Test_Process_Flow_FileNotFound()
        {
            //// arrange
            this._subFileHelper.Exists(Arg.Any<string>()).Returns(false);
            var reportEntity = new ReportEntity
            {
                FilePath = "report.json"
            };
            var coverageEntity = new CoverageEntity
            {
                FilePath = "coverage.csv"
            };

            var target = GetTarget();

            //// act
            Action action = () => target.Process(reportEntity, coverageEntity);

            //// assert
            action.Should()
                  .Throw<FileNotFoundException>()
                  .WithMessage("File Not Found!");
        }

        private CoverageProcessor GetTarget()
        {
            return new CoverageProcessor(this._subServiceProvider);
        }
    }
}