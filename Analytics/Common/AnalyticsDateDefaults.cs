using System;

namespace Analytics.Common
{
    // Analytics-only date defaults. These methods do not affect other reports.
    public static class AnalyticsDateDefaults
    {
        public static DateTime ThisMonday()
        {
            DateTime today = DateTime.Today;
            int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7;
            return today.AddDays(-daysSinceMonday);
        }

        public static DateTime PreviousMonday()
        {
            return ThisMonday().AddDays(-7);
        }

        public static DateTime PreviousMonthStart()
        {
            DateTime firstOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return firstOfThisMonth.AddMonths(-1);
        }

        public static DateTime PreviousMonthEnd()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
        }
    }
}
