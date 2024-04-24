using System;
using System.Collections.Generic;

namespace incomeExpensTrckAVALONIA.Helpers
{
    public class dateGenerator
    {
        public static List<string> GetYearList(int startYear)
        {
            var currentYear = DateTime.Now.Year;
            var years = new List<string>();

            for (int year = currentYear; year >= startYear; year--)
            {
                years.Add(year.ToString());
            }

            return years;
        }

        public static List<string> GetYearMonths(int startMonth)
        {
            var months = new List<string>();

            for (int month = startMonth; month <= 12; month++)
            {
                months.Add(month.ToString());
            }

            return months;
        }

        public static List<string> GetMonthDays(int startDay)
        {
            var days = new List<string>();

            for (int day = startDay; day <= 31; day++)
            {
                days.Add(day.ToString());
            }

            return days;
        }
    }
}
