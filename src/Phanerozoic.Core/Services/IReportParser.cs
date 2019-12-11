using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    internal interface IReportParser
    {
        List<CoverageEntity> Parser(ReportEntity reportEntity);
    }
}