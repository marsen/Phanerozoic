using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //report.Children.Aggregate(new List<CoverageEntity>(), (coverageList, item) =>
            //{
            //    item.
            //});
            var result = new List<DotCoverReportChild>();
            FindMethod(result, report.Children);
            //// 傳入 CoverageEntity 取得結果

            return null;
        }

        /// <summary>
        /// 尋找 Kind = Method 的各階層的項目
        /// </summary>
        /// <param name="result">回傳值</param>
        /// <param name="source">目前的階層</param>
        private void FindMethod(List<DotCoverReportChild> result, List<DotCoverReportChild> source)
        {
            var item = source.FirstOrDefault();

            if (item == null)
            {
                return;
            }

            if (item.Kind == Kind.Method)
            {
                result.Add(item);
                return;
            }

            if (item.Children.Count == 0)
            {
                return;
            }
            else
            {
                result.Add(item);
                this.FindMethod(result, item.Children);
            }
        }
    }
}