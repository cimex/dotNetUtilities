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
		/// Converts a Unix timestamp (seconds from 1970) into a DateTime.
		/// </summary>
		public static DateTime ConvertFromUnixTimestamp(this int input)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return origin.Add(new TimeSpan(0, 0, input));
		}


		/// <summary>
		/// Converts to Unix timestamp (seconds from 1970) form a DateTime.
		/// </summary>
		/// <returns>int</returns>
		public static int ConvertToUnixTimestampInt(this DateTime input)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			var diff = input.Subtract(origin);
			return (int)diff.TotalSeconds;
		}

		/// <summary>
		/// Converts to Unix timestamp (seconds from 1970) form a DateTime.
		/// </summary>
		/// <returns>Double</returns>
		public static double ConvertToUnixTimestamp(this DateTime input)
		{
			var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			var diff = input.Subtract(origin);
			return Math.Floor(diff.TotalSeconds);
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