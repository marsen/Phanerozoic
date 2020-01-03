using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phanerozoic.Core.Services
{
    public class SlackNotifyer
    {
        private readonly ISlackService _slackService;
        private readonly IConfiguration _configuration;

        public SlackNotifyer(IServiceProvider serviceProvider)
        {
            this._slackService = serviceProvider.GetService<ISlackService>();
            this._configuration = serviceProvider.GetService<IConfiguration>();
        }

        public void Notify(List<MethodEntity> methodList)
        {
            var message = new StringBuilder();

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

            var webHookUrl = this._configuration["Slack.WebHookUrl"];
            this._slackService.SendAsync(webHookUrl, message.ToString());
        }
    }
}