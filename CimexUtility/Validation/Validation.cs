using System.Linq;
using System.Text.RegularExpressions;

namespace CimexUtility.Validation
{
	public static class Validation
	{

		private static readonly Regex guidPattern = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
		public static bool IsGuid(this string input)
		{
			return !string.IsNullOrEmpty(input) && guidPattern.IsMatch(input);
		}

		/// <summary>
		/// Determines whether a particular string is an email.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsEmail(this string input)
		{
			return Regex.IsMatch(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
		}

	}
}
