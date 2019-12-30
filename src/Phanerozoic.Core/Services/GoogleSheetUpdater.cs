using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public class GoogleSheetUpdater : ICoverageUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly IGoogleSheetsService _googleSheetsService;
        private string _sheetsId;

        public GoogleSheetUpdater(IServiceProvider serviceProvider)
        {
            this._configuration = serviceProvider.GetService<IConfiguration>();
            this._googleSheetsService = serviceProvider.GetService<IGoogleSheetsService>();

            this._sheetsId = this._configuration["Google.Sheets.SheetsId"];
        }

        public void Update(CoverageEntity coverageEntity, List<MethodEntity> methodList)
        {
            IList<IList<object>> values = this._googleSheetsService.GetValues(this._sheetsId, "Coverage!A2:I2");
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
    }
}