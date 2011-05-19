using System;
using System.Collections.Generic;
using System.Linq;

namespace CimexUtility.Enumerations
{
	public static class TimeEnumerations
	{
		public static IEnumerable<string> HoursInDay
		{
			get
			{
				var day = new TimeSpan(1,0,0,0);
				return Enumerable.Range(0, (int) day.TotalHours).Select(x => x.ToString("00"));
			}
		}

		public static IEnumerable<string> MinutesInHour
		{
			get
			{

				var day = new TimeSpan(0, 1, 0, 0);
				return Enumerable.Range(0, (int) day.TotalMinutes).Select(x => x.ToString("00"));
			}
		}
	}
}