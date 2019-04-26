using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateProject.Common
{
    public class DateHelper
    {
        public static long DateToTimestamp(string dateStr)
        {
            var date = Convert.ToDateTime(dateStr);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(date - startTime).TotalSeconds; // 相差秒数
        }

        public static long DateToTimestamp(DateTime date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(date - startTime).TotalSeconds; // 相差秒数
        }

        public static DateTime TimestampToDate(long date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddSeconds(date);
        }
    }
}
