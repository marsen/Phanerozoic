using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using Phanerozoic.Core.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Phanerozoic.Core.Test.Services
{
    public class GoogleSheetUpdaterTest
    {
        private readonly IFileHelper _stubFileHelper;
        private readonly IConfiguration _stubConfiguration;
        private readonly IGoogleSheetsService _stubGoogleSheetsService;
        private readonly IServiceProvider _stubServiceProvider;

        public GoogleSheetUpdaterTest()
        {
            this._stubFileHelper = Substitute.For<IFileHelper>();
            this._stubConfiguration = Substitute.For<IConfiguration>();
            this._stubGoogleSheetsService = Substitute.For<IGoogleSheetsService>();

            this._stubServiceProvider = Substitute.For<IServiceProvider>();
            this._stubServiceProvider.GetService<IFileHelper>().Returns(this._stubFileHelper);
            this._stubServiceProvider.GetService<IConfiguration>().Returns(this._stubConfiguration);
            this._stubServiceProvider.GetService<IGoogleSheetsService>().Returns(this._stubGoogleSheetsService);
        }

        [Fact]
        public void Test_取得目前的涵蓋率()
        {
            //// Arrange
            var coverageEntity = new CoverageEntity();
            var methodList = new List<MethodEntity>();

            this._stubConfiguration["Google.Sheets.SheetsId"].Returns("target Id");

            //// Act
            var target = new GoogleSheetsUpdater(this._stubServiceProvider, this._stubConfiguration);
            target.Update(coverageEntity, methodList);

            //// Assert
            this._stubGoogleSheetsService.Received(1).GetValues(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}