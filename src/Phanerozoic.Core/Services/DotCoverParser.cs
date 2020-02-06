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

        public IList<MethodEntity> Parser(ReportEntity reportEntity)
        {
            var json = this._fileHelper.ReadAllText(reportEntity.FilePath);
            var report = JsonSerializer.Deserialize<DotCoverReport>(json);

            var result = new List<MethodEntity>();
            FindMethod(result, string.Empty, report.Children);

            //// Method without argument
            result.ForEach(i => i.Method = i.Method.Substring(0, i.Method.IndexOf('(')));

            //// Print Method
            result.ForEach(i => Console.WriteLine(i.ToString()));

            Console.WriteLine($"Report Method Count: {result.Count}");

            return result;
        }

        /// <summary>
        /// 尋找 Kind = Method 的各階層的項目
        /// </summary>
        /// <param name="result">回傳值</param>
        /// <param name="source">目前的階層</param>
        private void FindMethod(List<MethodEntity> result, string parentName, List<DotCoverReportChild> source)
        {
            if (source == null)
            {
                return;
            }

            foreach (var item in source)
            {
                var iName = parentName;
                if (item.Kind == Kind.Method)
                {
                    var covearge = new MethodEntity
                    {
                        Class = iName.Remove(iName.Length - 1, 1),
                        Method = item.Name,
                        Coverage = (int)item.CoveragePercent,
                    };
                    result.Add(covearge);
                }
                else if (item.Kind == Kind.Assembly)
                {
                    FindMethod(result, iName, item.Children);
                }
                else
                {
                    iName += $"{item.Name}.";
                    FindMethod(result, iName, item.Children);
                }
            }
        }
    }
}