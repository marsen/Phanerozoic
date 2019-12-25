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
    public class FileUpdaterTests
    {
        private IServiceProvider _subServiceProvider;
        private IFileHelper _subFileHelper;

        public FileUpdaterTests()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
        }

        [Fact]
        public void Test_Update_Flow()
        {
            //// Arrange
            var methodList = new List<MethodEntity>();
            var coverageEntity = new CoverageEntity
            {
                FilePath = "a.txt"
            };

            //// Act
            var target = new FileUpdater(this._subServiceProvider);
            target.Update(coverageEntity, methodList);

            //// Assert
            this._subFileHelper.Received(1).WriteAllText(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}