using System;
using System.Globalization;
using MerkDataBaseBusinessLogicProject;

namespace CommonActions
{
	public class CommonActions
	{
		public static object CalculateYears(object dateTime)
		{
			if (dateTime == null)
				return null;

			return DateTime.Now.Date.Year - Convert.ToDateTime(dateTime).Date.Year;
		}

		public static DateTime ConvertYearsToDate(int years, int month = 1, int day = 1)
		{
			return new DateTime(DateTime.Now.Year - years, month, day);
		}

		public static object GetMaxFinancialIntervalDate(int year)
		{
			FinancialInterval_cu financialInterval =
				FinancialInterval_cu.ItemsList.Find(item => Convert.ToDateTime(item.StartDate).Year.Equals(year));
			if (financialInterval != null)
				return Convert.ToDateTime(financialInterval.EdnDate);
			return null;
		}

		public static object GetMinFinancialIntervalDate(int year)
		{
			FinancialInterval_cu financialInterval =
				FinancialInterval_cu.ItemsList.Find(item => Convert.ToDateTime(item.StartDate).Year.Equals(year));
			if (financialInterval != null)
				return Convert.ToDateTime(financialInterval.StartDate);
			return null;
		}

		public static int GetIso8601WeekOfYear(DateTime date)
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
			{
				date = date.AddDays(3);
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

		public static Double GetShapeArea(Double width, Double height, int count = 1)
		{
			return width * height * count;
		}
	}
}
