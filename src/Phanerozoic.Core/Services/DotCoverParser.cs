using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Phanerozoic.Core.Services
{
    public class DotCoverParser : IReportParser
    {
        private readonly IFileHelper _fileHelper;

        public DotCoverParser(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();
        }

        public List<CoverageEntity> Parser(ReportEntity reportEntity)
        {
            var json = this._fileHelper.ReadAllText(reportEntity.FilePath);
            var report = JsonSerializer.Deserialize<DotCoverReport>(json);

            return null;
        }
    }
}