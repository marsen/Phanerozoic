using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;

namespace Phanerozoic.Core.Services
{
    public class GoogleSheetsLogger : ICoverageLogger
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IConfiguration _configuration;
        private string _sheetsId;

        public GoogleSheetsLogger(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._dateTimeHelper = serviceProvider.GetService<IDateTimeHelper>();
            this._googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();

            this._sheetsId = this._configuration["Google:Sheets:Id"];
        }

        public void Log(IList<MethodEntity> methodList)
        {
            //// Load Sheet Log Data
            var currentMethodList = GetCurrentMethodList();

            //// Sync Method and Coverage
            var newMethodList = new List<MethodEntity>();
            foreach (var method in methodList)
            {
                var methodLog = currentMethodList.FirstOrDefault(i => i.Equals(method));

                if (methodLog != null)
                {
                    methodLog.Coverage = method.Coverage;
                }
                else
                {
                    newMethodList.Add(method);
                }
            }

            //// Write Log Data
            int firstColumn = 3;
            var now = this._dateTimeHelper.Now;
            int week = this.GetWeek(now);
            int column = firstColumn + week;
            string columnLetter = SheetHelper.ColumnToLetter(column);

            //// Write Column Name
            var columnName = $"{week}({now.ToString("MM/dd")})";
            Console.WriteLine($"Write Column: {columnName}");
            var range = $"{now.Year}!{columnLetter}1";
            var values = SheetHelper.ObjectToValues(columnName);
            this._googleSheetsService.SetValue(this._sheetsId, range, values);

            //// Write Method
            foreach (var method in currentMethodList)
            {
                range = $"{now.Year}!{columnLetter}{method.RawIndex}";
                values = SheetHelper.ObjectToValues(method.Coverage);
                this._googleSheetsService.SetValue(this._sheetsId, range, values);
            }

            //// Write New Method
            var index = methodList.Count + 1;
            foreach (var method in newMethodList)
            {
                ++index;
                range = $"{now.Year}!A{index}:{columnLetter}{index}";

                var row = new object[column];
                row[0] = method.Repository;
                row[1] = method.Class;
                row[2] = method.Method;
                row[column -1] = method.Coverage;
                values = new List<IList<object>> { row };

                this._googleSheetsService.SetValue(this._sheetsId, range, values);
            }
        }

        private int GetWeek(DateTime now)
        {
            var week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return week;
        }

        private List<MethodEntity> GetCurrentMethodList()
        {
            var now = this._dateTimeHelper.Now;
            var startIndex = 1;
            var maxRow = 100;
            List<MethodEntity> methodLogList = new List<MethodEntity>();
            IList<IList<object>> values = this._googleSheetsService.GetValues(this._sheetsId, $"{now.Year}!A{startIndex + 1}:I{maxRow}");

            var index = startIndex;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row.Count < 3)
                    {
                        continue;
                    }

                    index++;
                    var methodEntity = new MethodEntity
                    {
                        Repository = row[0].ToString().Trim(),
                        Class = row[1].ToString().Trim(),
                        Method = row[2].ToString().Trim(),
                        RawIndex = index,
                        RawData = row,
                    };

                    if (string.IsNullOrWhiteSpace(methodEntity.Method) == false)
                    {
                        methodLogList.Add(methodEntity);
                    }
                }
            }
            return methodLogList;
        }
    }
}