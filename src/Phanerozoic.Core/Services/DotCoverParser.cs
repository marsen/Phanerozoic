﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;

namespace Phanerozoic.Core.Services
{
    public class DotCoverParser : IReportParser
    {
        private readonly IFileHelper _fileHelper;
        private readonly IConfiguration _configuration;

        private bool _printMethod = false;

        public DotCoverParser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._configuration = configuration;

            this._fileHelper = serviceProvider.GetRequiredService<IFileHelper>();

            bool.TryParse(this._configuration["Parser:PrintMethod"], out this._printMethod);
        }

        public IList<MethodEntity> Parser(CoverageEntity coverageEntity, ReportEntity reportEntity)
        {
            var json = this._fileHelper.ReadAllText(reportEntity.FilePath);
            var report = JsonSerializer.Deserialize<DotCoverReport>(json);

            var result = new List<MethodEntity>();
            FindMethod(result, string.Empty, string.Empty, report.Children);

            //// Method without argument
            //// Set Repository
            result.ForEach(i =>
            {
                if (i.Method.Contains('('))
                {
                    i.Method = i.Method.Substring(0, i.Method.IndexOf('('));
                }
                i.Repository = coverageEntity.Repository;
            });

            //// Print Method
            if (this._printMethod)
            {
                result.ForEach(i => Console.WriteLine(i.ToString()));
            }

            Console.WriteLine($"Report Method Count: {result.Count}");

            return result;
        }

        /// <summary>
        /// 尋找 Kind = Method 的各階層的項目
        /// </summary>
        /// <param name="result">回傳值</param>
        /// <param name="source">目前的階層</param>
        private void FindMethod(List<MethodEntity> result, string assembly, string parentName, List<DotCoverReportChild> source)
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
                        Project = assembly,
                        Class = iName.Remove(iName.Length - 1, 1),
                        Method = item.Name,
                        Coverage = (int)item.CoveragePercent,
                    };
                    result.Add(covearge);
                }
                else if (item.Kind == Kind.Type)
                {
                    var coverage = new MethodEntity
                    {
                        Project = assembly,
                        Class = iName + item.Name,
                        Method = "*",
                        Coverage = (int)item.CoveragePercent,
                    };
                    result.Add(coverage);
                    iName += $"{item.Name}.";
                    FindMethod(result, assembly, iName, item.Children);
                }
                else if (item.Kind == Kind.Assembly)
                {
                    assembly = item.Name;
                    FindMethod(result, assembly, iName, item.Children);
                }
                else
                {
                    iName += $"{item.Name}.";
                    FindMethod(result, assembly, iName, item.Children);
                }
            }
        }
    }
}