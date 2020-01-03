using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Phanerozoic.Core.Services
{
    public class SlackService : ISlackService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SlackService(IServiceProvider serviceProvider)
        {
            this._httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        }

        public async Task SendAsync(string webHookUrl, string message)
        {
            var httpClient = this._httpClientFactory.CreateClient();
            var data = new { text = message };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, webHookUrl);
            request.Content = content;

            var response = await httpClient.SendAsync(request);

            Console.WriteLine(response.StatusCode.ToString());
        }
    }
}