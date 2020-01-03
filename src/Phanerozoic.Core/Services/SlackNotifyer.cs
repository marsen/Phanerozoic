using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;

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
            this._slackService.Send("");
        }
    }
}