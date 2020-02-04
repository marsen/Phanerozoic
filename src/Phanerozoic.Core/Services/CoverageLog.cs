using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;

namespace Phanerozoic.Core.Services
{
    public class CoverageLog
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly IConfiguration _configuration;
        private string _sheetsId;

        public CoverageLog(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._dateTimeHelper = serviceProvider.GetService<IDateTimeHelper>();
            this._googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();

            this._sheetsId = this._configuration["Google:Sheets:Id"];
        }

        public void Log(IList<MethodEntity> methodList)
        {
            var now = this._dateTimeHelper.Now;
            int week = this.GetWeek(now);
            string column = SheetHelper.ColumnToLetter(week);
            var range = $"{now.Year}!{column}1";

            this._googleSheetsService.SetValue(this._sheetsId, range, null);
        }

        private int GetWeek(DateTime now)
        {
            var week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return week;
        }
    }
}