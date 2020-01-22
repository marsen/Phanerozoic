using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Phanerozoic.Core.Services.Tests
{
    public class CoverageLogTests
    {
        private readonly IGoogleSheetsService _stubGoogleSheetsService;
        private readonly IServiceProvider _stubServiceProvider;

        public CoverageLogTests()
        {
            this._stubGoogleSheetsService = Substitute.For<IGoogleSheetsService>();

            this._stubServiceProvider = Substitute.For<IServiceProvider>();
            this._stubServiceProvider.GetService<IGoogleSheetsService>().Returns(this._stubGoogleSheetsService);
        }

        [Fact()]
        public void LogTest()
        {
            //// Arrange
            var methodList = new List<MethodEntity>
            {
                new MethodEntity()
            };

            //// Act
            var target = GetTarget();
            target.Log(methodList);

            //// Assert
            this._stubGoogleSheetsService.Received(1).SetValue(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IList<IList<object>>>());
        }

        private CoverageLog GetTarget()
        {
            return new CoverageLog(this._stubServiceProvider);
        }
    }
}