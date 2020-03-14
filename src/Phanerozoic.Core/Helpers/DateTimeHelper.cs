using System;

namespace Phanerozoic.Core.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public static readonly string Format = "yyyy-MM-dd HH:mm:ss";

        public DateTime Now => DateTime.Now;
    }
}