﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

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
            var slackMessageJson = this.GetSlackMessage(coverageEntity, methodList);

            if (slackMessageJson.Length <= 0)
            {
                return;
            }

            this._slackService.SendAsync(this._webHookUrl, slackMessageJson);
        }

        private string GetMessage(CoverageEntity coverageEntity, IList<MethodEntity> methodList)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Phanerozoic Notify @{DateTime.Now.ToString(DateTimeHelper.Format)}");

            var downCount = methodList.Count(i => i.Status == CoverageStatus.Down);

            stringBuilder.AppendLine($"> Repository: {coverageEntity.Repository}, 涵蓋率下降方法數量 {downCount}");

            foreach (var method in methodList)
            {
                if (method.Status == CoverageStatus.Down)
                {
                    var msg = $"{method.Class}.{method.Method}: {method.LastCoverage} → {method.Coverage}";
                    stringBuilder.AppendLine(msg);
                }
            }

            var data = new { text = stringBuilder.ToString() };
            var json = JsonSerializer.Serialize(data);

            return json;
        }

        private string GetSlackMessage(CoverageEntity coverageEntity, IList<MethodEntity> methodList)
        {
            var downCount = methodList.Count(i => i.Status == CoverageStatus.Down);
            var color = downCount > 0 ? "#FF0000" : "#00FF00";
            var attachment = new Attachment
            {
                Color = color
            };

            attachment.Pretext = $"Phanerozoic Notify @{DateTime.Now.ToString(DateTimeHelper.Format)}";

            attachment.Title = $"Repository: {coverageEntity.Repository}, 涵蓋率下降方法數量 {downCount}";

            var stringBuilder = new StringBuilder();
            foreach (var method in methodList)
            {
                if (method.Status == CoverageStatus.Down)
                {
                    var msg = $"{method.Class}.{method.Method}: {method.LastCoverage} → {method.Coverage}";
                    stringBuilder.AppendLine(msg);
                }
            }
            attachment.Text = stringBuilder.ToString();

            var slackMessage = new SlackMessage
            {
                Attachments = new List<Attachment>{
                    attachment
                }
            };

            return slackMessage.ToJson();
        }
    }
}