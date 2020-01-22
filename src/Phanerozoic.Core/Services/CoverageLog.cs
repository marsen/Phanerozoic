using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Phanerozoic.Core.Services
{
    public class CoverageLog
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGoogleSheetsService _googleSheetsService;

        public CoverageLog(IServiceProvider serviceProvider)
        {
            this._dateTimeHelper = serviceProvider.GetService<IDateTimeHelper>();
            this._googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();
        }

        public void Log(IList<MethodEntity> methodList)
        {
            //// C is 3
            var firstColumn = 3;



            this._googleSheetsService.SetValue(null, null, null);
        }

        private int GetWeek()
        {
            var now = this._dateTimeHelper.Now;
            var week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return week;
        }
    }
}