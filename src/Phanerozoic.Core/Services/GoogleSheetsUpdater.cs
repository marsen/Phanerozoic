using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;

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

            this._sheetsId = this._configuration["Google:Sheets:Id"];

            Console.WriteLine($"Target Sheets ID: {this._sheetsId}");
        }

        public IList<MethodEntity> Update(CoverageEntity coverageEntity, IList<MethodEntity> reportMethodList)
        {
            var startIndex = 1;
            var maxRow = 100;
            List<MethodEntity> sheetMethodList = new List<MethodEntity>();
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
                        Coverage = SheetHelper.ObjectToInt(row[4]),
                        RawIndex = index,
                        RawData = row,
                    };

                    if (string.IsNullOrWhiteSpace(methodEntity.Method) == false)
                    {
                        sheetMethodList.Add(methodEntity);
                    }
                }
            }

            var repositoryMethodList = sheetMethodList.Where(i => i.Repository == coverageEntity.Repository).ToList();

            Console.WriteLine($"Repository: {coverageEntity.Repository}, Method Count: {repositoryMethodList.Count}");
            if (repositoryMethodList.Count <= 0)
            {
                return repositoryMethodList;
            }

            var updateCount = 0;
            foreach (var coreMethod in repositoryMethodList)
            {
                var reportMethod = reportMethodList.FirstOrDefault(i => i.Class == coreMethod.Class && i.Method == coreMethod.Method);

                if (reportMethod == null)
                {
                    continue;
                }
                updateCount++;

                coreMethod.UpdateCoverage(reportMethod);

                var symbolDictionary = new Dictionary<CoverageStatus, string>();
                symbolDictionary.Add(CoverageStatus.Unchange, "=");
                symbolDictionary.Add(CoverageStatus.Up, "▲");
                symbolDictionary.Add(CoverageStatus.Down, "▼");
                var symbol = symbolDictionary[coreMethod.Status];

                Console.WriteLine($"{coreMethod.Class}.{coreMethod.Method}: {coreMethod.LastCoverage} {symbol} {coreMethod.Coverage}");

                if (coreMethod.Status != CoverageStatus.Unchange || coreMethod.Coverage == 0)
                {
                    this.UpdateCell($"E{coreMethod.RawIndex}", coreMethod.Coverage);
                }
                this.UpdateCell($"J{coreMethod.RawIndex}", DateTime.Now.ToString(DateTimeHelper.Format));
            }
            Console.WriteLine($"Update Rate: {updateCount}/{repositoryMethodList.Count}");

            return repositoryMethodList;
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