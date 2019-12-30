using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Phanerozoic.Core.Services
{
    public class GoogleSheetUpdater : ICoverageUpdater
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        private static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };

        private static string ApplicationName = "Google Sheets API .NET Quickstart";
        private readonly IConfiguration _configuration;

        public GoogleSheetUpdater(IServiceProvider serviceProvider)
        {
            this._configuration = serviceProvider.GetService<IConfiguration>();
        }

        public void Update(CoverageEntity coverageEntity, List<MethodEntity> methodList)
        {
            UserCredential credential = GetCredential();

            SheetsService service = GetSheet(credential);

            IList<IList<object>> values = GetValues(service);
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

        private IList<IList<object>> GetValues(SheetsService service)
        {
            // Define request parameters.
            String spreadsheetId = this._configuration["Google.Sheet.SheetId"];
            String range = "Coverage!A2:I2";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            return values;
        }

        private static SheetsService GetSheet(UserCredential credential)
        {
            // Create Google Sheets API service.
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private static UserCredential GetCredential()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }
    }
}