using Phanerozoic.Core.Entities;

namespace Phanerozoic.Core.Services
{
    public interface ICoverageProcessor
    {
        bool Process(ReportEntity reportEntity);
    }
}