using System;
using System.Text;

namespace CimexUtility.Conversions
{
	public static class UnitConverter
	{

		public static string ToYesNo(this bool thisBoolean)
		{
			return thisBoolean ? "Yes" : "No";
		}

		/// <summary>
		/// Calculates someones age today based on their birthday.
		/// </summary>
		/// <param name="birthDate"></param>
		/// <returns></returns>
		public static int CalculateAge(this DateTime birthDate)
		{
			var yearsPassed = DateTime.Now.Year - birthDate.Year;
			if (DateTime.Now.Month < birthDate.Month |
			    (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
			{
				yearsPassed--;
			}
			return yearsPassed;
		}

		/// <summary>
		/// Converts a Unix timestamp (milliseconds from 1970).
		/// </summary>
		public static DateTime UtcFromUnixTimestamp(this double milliseconds)
		{
			var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
			dateTime = dateTime.AddMilliseconds(Math.Round(milliseconds));
			return dateTime;
		}

		/// <summary>
		/// Converts to Unix time (milliseconds from 1970).
		/// </summary>
		public static double UnixTimeStampFromUtc(this DateTime time)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
			var diff = time.ToUniversalTime().Subtract(origin);
			return Math.Round(diff.TotalMilliseconds);
		}



		/// <summary>
		/// Encode a UTF8 string to Base64 encoded string.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string EncodeBase64(this string input)
		{
			var encbuff = Encoding.UTF8.GetBytes(input);
			return Convert.ToBase64String(encbuff);
		}

		/// <summary>
		/// Encode a byte array into a Base64 string.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string EncodeBase64(this byte[] input)
		{
			return Convert.ToBase64String(input);
		}
		
	}
}