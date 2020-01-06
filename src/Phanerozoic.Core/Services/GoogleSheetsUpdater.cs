﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phanerozoic.Core.Services
{
    public class GoogleSheetsUpdater : ICoverageUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly IGoogleSheetsService _googleSheetsService;
        private string _sheetsId;

        public GoogleSheetsUpdater(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();

            this._sheetsId = this._configuration["Google.Sheets.SheetsId"];
        }

        public IList<MethodEntity> Update(CoverageEntity coverageEntity, IList<MethodEntity> methodList)
        {
            var startIndex = 1;
            var maxRow = 100;
            List<MethodEntity> beforeMethodList = new List<MethodEntity>();
            IList<IList<object>> values = this._googleSheetsService.GetValues(this._sheetsId, $"Coverage!A{startIndex + 1}:I{maxRow}");

            var index = startIndex;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    index++;
                    var methodEntity = new MethodEntity
                    {
                        Repository = row[0].ToString().Trim(),
                        Project = row[1].ToString().Trim(),
                        Class = row[2].ToString().Trim(),
                        Method = row[3].ToString().Trim(),
                        Coverage = int.Parse(row[4].ToString()),
                        RawIndex = index,
                        RawData = row,
                    };

                    if (string.IsNullOrWhiteSpace(methodEntity.Method) == false)
                    {
                        beforeMethodList.Add(methodEntity);
                    }
                }
            }

            var beforeProjectMethodList = beforeMethodList.Where(i => i.Repository == coverageEntity.Repository).ToList();

            if (beforeProjectMethodList.Count <= 0)
            {
                Console.WriteLine($"專案 {coverageEntity.Repository}: 沒有對應的核心方法");
                return beforeProjectMethodList;
            }

            foreach (var method in beforeProjectMethodList)
            {
                var newMethod = methodList.FirstOrDefault(i => i.Class == method.Class && i.Method == method.Method);

                if (newMethod == null)
                {
                    continue;
                }

                method.UpdateCoverage(newMethod);

                if (method.Status != CoverageStatus.Unchange)
                {
                    this.UpdateCell($"E{method.RawIndex}", method.Coverage);
                    this.UpdateCell($"J{method.RawIndex}", DateTime.Now);
                }
            }

            return beforeProjectMethodList;
        }

        private void UpdateCell(string range, object value)
        {
            var updateValues = new List<IList<object>>
                {
                    new List<object>
                    {
                        value
                    }
                };

            this._googleSheetsService.SetValue(this._sheetsId, range, updateValues);
        }
    }
}