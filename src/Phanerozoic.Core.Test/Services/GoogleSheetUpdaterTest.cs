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
        private readonly IServiceProvider _subServiceProvider;

        public GoogleSheetUpdaterTest()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
        }

        [Fact]
        public void Test_取得目前的涵蓋率()
        {
            //// Arrange
            var coverageEntity = new CoverageEntity();
            var methodList = new List<MethodEntity>();
            //// Act
            var target = new GoogleSheetUpdater(this._subServiceProvider);
            target.Update(coverageEntity, methodList);

            //// Assert
        }
    }
}