using System;
using System.Globalization;
using System.Linq;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public enum DateTimeTypesOfCompare
	{
		DateTime = 1,
		DateOnly = 2,
		TimeOnly = 3
	}

	public enum DateTimeUnit
	{
		Year, Month, Day, Hour, Minute, Second
	}

	public enum RelationType
	{
		EarlierThan, Equal, LaterThan
	}

	public static class CommonExtensions
	{
		public static string ConvertTimeSpanTimeString(this TimeSpan timeSpan)
		{
			string hours = "";
			string minutes = "";
			string time = "";

			hours = timeSpan.Hours.ToString();
			minutes = timeSpan.Minutes.ToString();
			time = hours + " : " + minutes;

			return time;
		}

		public static string ConvertDateTimeToString(this DateTime dateTime, bool isTimeReversed, bool showTime, bool reverseDateVSTime = false)
		{
			string dateTimeToString;
			string hours = "";
			string minutes = "";
			string day = "";
			string month = "";
			string year = "";

			hours = dateTime.Hour.ToString();
			minutes = dateTime.Minute.ToString();
			day = dateTime.Day.ToString();
			month = dateTime.Month.ToString();
			year = dateTime.Year.ToString();

			if (showTime)
				if (isTimeReversed)
					if (reverseDateVSTime)
						dateTimeToString = "( " + minutes + " : " + hours + " ) " + day + "-" + month + "-" + year;
					else
						dateTimeToString = day + "-" + month + "-" + year + " ( " + minutes + " : " + hours + " )";
				else
					if (reverseDateVSTime)
						dateTimeToString = "( " + hours + " : " + minutes + " ) " + day + "-" + month + "-" + year;
					else
						dateTimeToString = day + "-" + month + "-" + year + " ( " + hours + " : " + minutes + " )";
			else
				dateTimeToString = day + "-" + month + "-" + year;

			return dateTimeToString;
		}

		public static Object StartOfDayDateTime(this Object obj)
		{
			if (!(obj is DateTime)) return null;
			return StartOfDayDateTime((DateTime)obj);
		}

		public static Object EndOfDayDateTime(this Object obj)
		{
			if (!(obj is DateTime)) return null;
			return EndOfDayDateTime((DateTime)obj);
		}

		public static DateTime StartOfDayDateTime(this DateTime dateTime)
		{
			return dateTime.Date;
		}

		public static DateTime EndOfDayDateTime(this DateTime dateTime)
		{
			DateTime endOfDay = dateTime.Date;
			return endOfDay.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
		}

		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = dt.DayOfWeek - startOfWeek;
			if (diff < 0)
			{
				diff += 7;
			}

			return dt.AddDays(-1 * diff).Date;
		}

		public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			return StartOfWeek(dt, startOfWeek).AddDays(7).Date;
		}

		public static DateTime Min(params DateTime[] dts)
		{
			if (dts == null || dts.Count() == 0) return default(DateTime);
			DateTime result = dts.First();

			for (int i = 1; i < dts.Count(); i++) result = new DateTime(Math.Min(result.Ticks, dts[i].Ticks));
			return result;
		}

		public static DateTime Max(params DateTime[] dts)
		{
			if (dts == null || dts.Count() == 0) return default(DateTime);
			DateTime result = dts.First();

			for (int i = 1; i < dts.Count(); i++) result = new DateTime(Math.Max(result.Ticks, dts[i].Ticks));
			return result;
		}

		public static TimeSpan Min(params TimeSpan[] spans)
		{
			if (spans == null || spans.Count() == 0) return default(TimeSpan);
			TimeSpan result = spans.First();

			for (int i = 1; i < spans.Count(); i++) result = new TimeSpan(Math.Min(result.Ticks, spans[i].Ticks));
			return result;
		}

		public static TimeSpan Max(params TimeSpan[] spans)
		{
			if (spans == null || spans.Count() == 0) return default(TimeSpan);
			TimeSpan result = spans.First();

			for (int i = 1; i < spans.Count(); i++) result = new TimeSpan(Math.Max(result.Ticks, spans[i].Ticks));
			return result;
		}

		public static DateTime Floor(this DateTime dtTm, TimeSpan toTheNearestSpan)
		{
			return dtTm - (TimeSpan.FromTicks((dtTm - DateTime.MinValue).Ticks % toTheNearestSpan.Ticks));
		}

		public static DateTime Ceiling(this DateTime dtTm, TimeSpan toTheNearestSpan)
		{
			return dtTm.Floor(toTheNearestSpan).Add(toTheNearestSpan);
		}

		public static DateTime Round(this DateTime dtTm, TimeSpan toTheNearestSpan)
		{
			DateTime floor = dtTm.Floor(toTheNearestSpan);
			DateTime cieling = dtTm.Ceiling(toTheNearestSpan);

			return dtTm - floor < cieling - dtTm ? floor : cieling;
		}

		public static DateTime GetMidPoint(DateTime rangeStart, DateTime rangeEnd)
		{
			return rangeStart.AddTicks((rangeEnd.Ticks - rangeStart.Ticks) / 2);
		}

		public static int GetIso8601WeekOfYear(this DateTime date)
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
	}
}
