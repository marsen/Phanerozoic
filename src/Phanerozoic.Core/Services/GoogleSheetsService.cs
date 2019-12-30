using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Phanerozoic.Core.Services
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        private string[] _scopes = { SheetsService.Scope.SpreadsheetsReadonly };

        private string _applicationName = "Google Sheets API .NET Quickstart";
        private string _credentialsPath = "credentials.json";

        public GoogleSheetsService()
        {
        }

        private UserCredential GetCredential(string credentialsPath)
        {
            UserCredential credential;

            using (var stream =
                new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }

        private SheetsService GetSheets(UserCredential credential)
        {
            // Create Google Sheets API service.
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        public IList<IList<object>> GetValues(string spreadsheetId, string range)
        {
            var credential = this.GetCredential(this._credentialsPath);
            var service = this.GetSheets(credential);

            // Define request parameters.
            //String spreadsheetId = this._configuration["Google.Sheet.SheetId"];
            //String range = "Coverage!A2:I2";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            return values;
        }
    }
}