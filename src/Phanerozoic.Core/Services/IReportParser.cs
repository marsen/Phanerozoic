using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface IReportParser
    {
        List<MethodEntity> Parser(ReportEntity reportEntity);
    }
}