﻿using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface IGoogleSheetsService
    {
        IList<IList<object>> GetValues(string spreadsheetId, string range);
        void SetValue(string spreadsheetId, string range, IList<IList<object>> values);
    }
}