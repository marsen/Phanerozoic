using System.Threading.Tasks;

namespace Phanerozoic.Core.Services
{
    public interface ISlackService
    {
        Task SendAsync(string webHookUrl, string slackMessageJson);
    }
}