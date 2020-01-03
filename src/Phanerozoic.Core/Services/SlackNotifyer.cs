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
        public SlackNotifyer(IServiceProvider serviceProvider)
        {
            this._slackService = serviceProvider.GetService<ISlackService>();
        }

        public void Notify(List<MethodEntity> methodList)
        {
            var message = new StringBuilder();

            foreach(var method in methodList)
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

            this._slackService.Send(message.ToString());
        }
    }
}