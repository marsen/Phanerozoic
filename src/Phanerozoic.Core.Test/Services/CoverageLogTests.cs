using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Xunit;

namespace Phanerozoic.Core.Services.Tests
{
    public class CoverageLogTests
    {
        private readonly IGoogleSheetsService _stubGoogleSheetsService;
        private readonly IDateTimeHelper _stubDateTimeHelper;
        private readonly IServiceProvider _stubServiceProvider;
        private readonly IConfiguration _stubConfiguration;

        public CoverageLogTests()
        {
            this._stubGoogleSheetsService = Substitute.For<IGoogleSheetsService>();
            this._stubConfiguration = Substitute.For<IConfiguration>();
            this._stubDateTimeHelper = Substitute.For<IDateTimeHelper>();

            this._stubServiceProvider = Substitute.For<IServiceProvider>();
            this._stubServiceProvider.GetService<IGoogleSheetsService>().Returns(this._stubGoogleSheetsService);
            this._stubServiceProvider.GetService<IDateTimeHelper>().Returns(this._stubDateTimeHelper);
        }

        [Fact(DisplayName = "每周一欄")]
        public void LogTest()
        {
            this.SheetRangeAssert(new DateTime(2019, 1, 1), "2019!A1");
            this.SheetRangeAssert(new DateTime(2019, 12, 31), "2019!BA1");
            this.SheetRangeAssert(new DateTime(2020, 1, 1), "2020!A1");
        }

        private CoverageLog GetTarget()
        {
            return new CoverageLog(this._stubServiceProvider, this._stubConfiguration);
        }

        private void SheetRangeAssert(DateTime time, string range)
        {
            //// Arrange
            var methodList = new List<MethodEntity>
            {
                new MethodEntity()
            };
            this._stubDateTimeHelper.Now.Returns(time);

            //// Act
            GetTarget().Log(methodList);

            //// Assert
            this._stubGoogleSheetsService.Received(1).SetValue(Arg.Any<string>(), range, Arg.Any<IList<IList<object>>>());
        }
    }
}