using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phanerozoic.Core.Test.Services
{
    public class SlackNotifyerTest
    {
        private readonly ISlackService _stubSlackService;
        private readonly IServiceProvider _stubServiceProvider;

        public SlackNotifyerTest()
        {
            this._stubSlackService = Substitute.For<ISlackService>();

            this._stubServiceProvider = Substitute.For<IServiceProvider>();
            this._stubServiceProvider.GetService<ISlackService>().Returns(this._stubSlackService);
        }

        [Fact]
        public void Test涵蓋率未下降則不發通知()
        {
            //// Arrange
            var methodUnchange = new MethodEntity
            {
                Class = "AClass",
                Method = "AMethod",
                Status = CoverageStatus.Unchange,
                Coverage = 23,
            };
            var methodUp = new MethodEntity
            {
                Class = "AClass",
                Method = "AMethod",
                Status = CoverageStatus.Up,
                Coverage = 23,
            };
            var methodList = new List<MethodEntity>
            {
                methodUnchange,
                methodUp,
            };

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(methodUnchange.ToString());
            var expectedMessage = stringBuilder.ToString();

            //// Act
            var target = GetTarget();
            target.Notify(methodList);

            //// Assert
            this._stubSlackService.DidNotReceiveWithAnyArgs().Send(Arg.Any<string>());
        }

        [Fact]
        public void Test涵蓋率下降的方法發出通知()
        {
            //// Arrange
            var method = new MethodEntity
            {
                Class = "AClass",
                Method = "AMethod",
                Status = CoverageStatus.Down,
                Coverage = 23,
            };
            var methodList = new List<MethodEntity>
            {
                method,
            };

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(method.ToString());
            var expectedMessage = stringBuilder.ToString();

            //// Act
            var target = GetTarget();
            target.Notify(methodList);

            //// Assert
            this._stubSlackService.Received(1).Send(expectedMessage);
        }

        private SlackNotifyer GetTarget()
        {
            return new SlackNotifyer(this._stubServiceProvider);
        }
    }
}