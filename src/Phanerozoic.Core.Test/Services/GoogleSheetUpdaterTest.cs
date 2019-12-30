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
        private readonly IFileHelper _subFileHelper;
        private readonly IConfiguration _subConfiguration;
        private readonly IGoogleSheetsService _subGoogleSheetsService;
        private readonly IServiceProvider _subServiceProvider;

        public GoogleSheetUpdaterTest()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();
            this._subConfiguration = Substitute.For<IConfiguration>();
            this._subGoogleSheetsService = Substitute.For<IGoogleSheetsService>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
            this._subServiceProvider.GetService<IConfiguration>().Returns(this._subConfiguration);
            this._subServiceProvider.GetService<IGoogleSheetsService>().Returns(this._subGoogleSheetsService);

        }

        [Fact]
        public void Test_取得目前的涵蓋率()
        {
            //// Arrange
            var coverageEntity = new CoverageEntity();
            var methodList = new List<MethodEntity>();

            this._subConfiguration["Google.Sheets.SheetsId"].Returns("target Id");

            //// Act
            var target = new GoogleSheetUpdater(this._subServiceProvider);
            target.Update(coverageEntity, methodList);

            //// Assert
            this._subGoogleSheetsService.Received(1).GetValues(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}