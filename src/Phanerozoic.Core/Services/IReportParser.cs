using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface IReportParser
    {
        IList<MethodEntity> Parser(CoverageEntity coverageEntity, ReportEntity reportEntity);
    }
}