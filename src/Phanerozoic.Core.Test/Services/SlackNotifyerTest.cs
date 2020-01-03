using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Services;
using System;
using System.Collections.Generic;
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
        public void TestNotify()
        {
            //// Arrange
            var methodList = new List<MethodEntity>
            {
            };

            //// Act
            var target = GetTarget();
            target.Notify(methodList);

            //// Assert
            this._stubSlackService.Received(1).Send(string.Empty);
        }

        private SlackNotifyer GetTarget()
        {
            return new SlackNotifyer(this._stubServiceProvider);
        }
    }
}