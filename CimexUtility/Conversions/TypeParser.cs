using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace CimexUtility.Conversions
{
	public static class TypeParser
	{

		public static string ParseNewLineToBr(this string input)
		{
			if (String.IsNullOrEmpty(input)) return input;
			var newLineRegEx = new Regex(Environment.NewLine);
			return newLineRegEx.IsMatch(input) ? newLineRegEx.Replace(input, "<br />") : input;
		}

		public static string ParseBrToNewLine(this string input)
		{
			if (String.IsNullOrEmpty(input)) return input;
			var newLineRegEx = new Regex("<br />");
			return newLineRegEx.IsMatch(input) ? newLineRegEx.Replace(input, Environment.NewLine) : input;
		}

		/// <summary>
		/// Gets a string representation of an enumeration.
		/// </summary>
		public static string GetEnumString<T>(this T input)
		{
		   return Equals(input, default(T)) ? string.Empty : Enum.GetName(typeof(T), input);
		}

		/// <summary>
		/// Returns the integer equivalent of an enumeration from its string equivalent.
		/// </summary>
		public static int GetEnumInteger<T>(this string input)
		{
			return (int)Enum.Parse(typeof(T), input, true);
		}

		/// <summary>
		/// Returns the Enumeration equivalent of a string.
		/// </summary>
		/// <typeparam name="T">The enumeration.</typeparam>
		/// <param name="input">The matching string.</param>
		/// <returns></returns>
		public static T ParseStringToEnum<T>(this string input)
		{
			if (input == null) throw new ArgumentException("Cannot parse null string to enum.");
			input = input.Trim();
			if (input.Length == 0) throw new ArgumentException("Cannot parse empty string.");
			var t = typeof(T);
			if (!t.IsEnum) throw new ArgumentException("This method can only be used on an enumeration type.");

			return (T)Enum.Parse(t, input, true);
		}


		/// <summary>
		/// Converts a string to an int or null if not possible.
		/// </summary>
		/// <returns>Null or Int?</returns>
		public static int? TryParse(string data)
		{
			int value;
			return int.TryParse(data, out value) ? value : new int?();
		}

		/// <summary>
		/// Converts a string to a date or null if not possible.
		/// </summary>
		/// <param name="data"></param>
		/// <returns>Null or DateTime</returns>
		public static DateTime? DateTimeTryParse(string data)
		{
			DateTime value;
			return DateTime.TryParse(data, out value) ? value : new DateTime?();
		}

		/// <summary>
		/// Converts a string to a Guid if possible returns null if not.
		/// </summary>
		/// <param name="data"></param>
		/// <returns>Null or Guid?</returns>
		public static Guid? GuidTryParse(string data)
		{
			Guid? guid;
			try
			{
				guid = new Guid(data);
			}
			catch (Exception ex)
			{
				guid = null;
				Debug.WriteLine(ex.Message);
			}
			return guid;
		}
	}
}