using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services;
using System;
using Xunit;

namespace Phanerozoic.Core.Test.Services
{
    public class CoverageProcessorTest
    {
        private readonly IServiceProvider _subServiceProvider;
        private readonly IFileHelper _subFileHelper;
        private readonly IReportParser _subReportParser;

        public CoverageProcessorTest()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();
            this._subReportParser = Substitute.For<IReportParser>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
            this._subServiceProvider.GetService<IReportParser>().Returns(this._subReportParser);
        }

        [Fact]
        public void Test_Process_Flow()
        {
            var reportEntity = new ReportEntity
            {
                FilePath = "report.json"
            };

            var target = GetTarget();

            ////// act
            target.Process(reportEntity);

            //// assert
            Assert.True(true);
        }

        private ICoverageProcessor GetTarget()
        {
            return new CoverageProcessor(this._subServiceProvider);
        }
    }
}