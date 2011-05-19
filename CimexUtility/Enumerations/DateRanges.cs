using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CimexUtility.Enumerations
{
	public class DateRanges
	{
		/// <summary>
		/// Returns the maximum number of days in any given month.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<int> MaximumDaysInMonth()
		{
			for (var i = 1; i < 32; i++)
			{
				yield return i;
			}
		}
		
		/// <summary>
		/// Returns the months of a year in the format Key = Month name, Value = Month number.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<KeyValuePair<string,int>> MonthsOfYear()
		{
			var counter = 1;
			foreach (var month in DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames.Where(m => !string.IsNullOrEmpty(m)))
			{
				yield return new KeyValuePair<string, int>(month, counter++);
			}
		}

		/// <summary>
		/// Returns a list of calendar years when people with the age ranges given could have been born.
		/// </summary>
		public static IEnumerable<int> AgeYearSpanRange(int lowerAge, int upperAge)
		{
			var thisYear = DateTime.Now.Year;
			var upperYear = (thisYear - (lowerAge - 1));
			var lowerYear = (thisYear - (upperAge + 1));

			for (var i = upperYear - 1; i >= lowerYear; i--)
			{
				yield return i;
			}
		}
	}
}