﻿using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface IReportParser
    {
        List<CoverageEntity> Parser(ReportEntity reportEntity);
    }
}