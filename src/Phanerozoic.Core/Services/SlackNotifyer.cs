﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phanerozoic.Core.Services
{
    public class SlackNotifyer : INotifyer
    {
        private readonly ISlackService _slackService;

        private string _webHookUrl;

        public SlackNotifyer(IServiceProvider serviceProvider)
        {
            this._slackService = serviceProvider.GetService<ISlackService>();
            var configuration = serviceProvider.GetService<IConfiguration>();

            this._webHookUrl = configuration["Slack:WebHookUrl"];
        }

        public void Notify(CoverageEntity coverageEntity, IList<MethodEntity> methodList)
        {
            var message = new StringBuilder();
            message.AppendLine($"Phanerozoic Notify @{DateTime.Now.ToString(DateTimeHelper.Format)}");

            var downCount = methodList.Count(i => i.Status == CoverageStatus.Down);

            message.AppendLine($"> Repository: {coverageEntity.Repository}, 涵蓋率下降方法數量 {downCount}");

            foreach (var method in methodList)
            {
                if (method.Status == CoverageStatus.Down)
                {
                    message.AppendLine(method.ToString());
                }
            }

            if (message.Length <= 0)
            {
                return;
            }

            this._slackService.SendAsync(this._webHookUrl, message.ToString());
        }
    }
}