using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace CimexUtility.StringManipulation
{
	public static class TextManipulation
	{

		/// <summary>
		/// Wraps text that matches a string provided with the specified Html tag.
		/// </summary>
		public static string HtmlTagWrap(this string input, string match, HtmlTextWriterTag tag)
		{
			if (string.IsNullOrEmpty(input)) return null;
			var tagWriter = new StringWriter();
			using (var w = new HtmlTextWriter(tagWriter))
			{
				w.RenderBeginTag(tag);
				w.Write(match);
				w.RenderEndTag();
			}
			return Regex.Replace(input, match, tagWriter.ToString(), RegexOptions.IgnoreCase);
		}


		/// <summary>
		/// Gets the lede from a block of text as per the length required.
		/// </summary>
		public static string GetLede(this string input, int wordCount)
		{
			if (input == null) return string.Empty;

			var regex = new Regex(@"\s+");
			var words = regex.Split(input);
			if (words.Length <= wordCount) return input;
			var newString = new StringBuilder();

			for (var i = 0; i < wordCount; i++)
			{
				newString.Append(words[i] + " ");
			}
			var lede = newString.ToString();
			lede = cleanStringEndForEllipsis(lede);
			return lede + "...";
		}

		/// <summary>
		/// Limits a block of text to 
		/// </summary>
		[Obsolete("Use: CharLimit(this string input, int maxCharCount, bool addEllipsis)")]
		public static string CharLimit(this string input, int maxCharCount)
		{
			if (input == null) return string.Empty;
			var chars = input.ToCharArray();
			return chars.Count() > maxCharCount ? input.Substring(0, maxCharCount) : input;
		}


		/// <summary>
		/// Limits a block of text to specified chars, optionally adding ellipsis
		/// </summary>
		public static string CharLimit(this string input, int maxCharCount, bool addEllipsis)
		{
			
			if (input == null) return string.Empty;
			var chars = input.ToCharArray();
			
			if(chars.Count() > maxCharCount)
			{
				var shortenedString = input.Substring(0, maxCharCount);
				var lastSpaceIndex = shortenedString.LastIndexOf(" ");
				var stringWithFullLastWord = 
					lastSpaceIndex != -1 ? shortenedString.Substring(0, lastSpaceIndex) :  shortenedString;
				stringWithFullLastWord = cleanStringEndForEllipsis(stringWithFullLastWord);
				var elipsis = addEllipsis ? "..." : null;
				return stringWithFullLastWord + elipsis;
			}

			return input;
		}

		private static string cleanStringEndForEllipsis(string text)
		{
			return string.IsNullOrEmpty(text) ? text : text.TrimEnd(".;, ".ToCharArray());
		}


		/// <summary>
		/// Converts a delimited string into a Generic list.
		/// </summary>
		/// <param name="delimitedstring">e.g. "apple,orange,banana"</param>
		/// <param name="delimiter">"e.g. ","</param>
		/// <returns></returns>
		public static List<string> ToGenericList(this string delimitedstring, string delimiter)
		{
			string[] array = null;
			if (!string.IsNullOrEmpty(delimiter) & !string.IsNullOrEmpty(delimitedstring))
			{
				array = delimitedstring.Split(delimiter.ToCharArray());
			}

			var list = new List<string>();
			if (array != null)
			{
				foreach (var item in array)
				{
					list.Add(item);
				}
			}
			return list;
		}

		/// <summary>
		/// Converts a list to a delimited string.
		/// </summary>
		/// <returns>e.g. "apple, orange, banana"</returns>
		public static string ToDelimitedString(this IEnumerable<string> collection, string delimiter)
		{
			if (collection == null || collection.Count() < 1) return null;
			var list = collection.ToList();
			var sb = new StringBuilder();

			foreach (var item in list)
			{
				sb.Append(item);
				if (list.IndexOf(item) != (list.Count - 1))
				{
					sb.Append(delimiter);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts a list to a comma delimited string with an 'and'.
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static string ToEnglishLanguageList(this IEnumerable<string> collection)
		{

			var list = collection.ToList();
			var sb = new StringBuilder();

			foreach (var item in list)
			{
				var currentIndex = list.IndexOf(item);
				var secondLastIndex = list.Count - 2;

				sb.Append(item);
				
				if (currentIndex < secondLastIndex)
				{
					sb.Append(", ");
				}

				if (currentIndex == secondLastIndex)
				{
					sb.Append(" and ");
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Removes a specified character from a string.
		/// </summary>
		public static string RemoveString(this string body, char charToRemove)
		{
			var cleanString = string.Empty;
			if (!string.IsNullOrEmpty(body))
				cleanString = body.Replace(charToRemove.ToString(), string.Empty);
			return cleanString;
		}


		/// <summary>
		/// Strips a text to a given length without splitting the last word.
		/// </summary>
		/// <param name="previewText">The string to shorten</param>
		/// <param name="maxLength">Length of the returned string</param>
		/// <returns>A shortened version of the given string</returns>
		[Obsolete("Use GetLede - which has identical function instead")]
		public static string TrimText(this string previewText, int maxLength)
		{
			if (previewText == null || previewText.Length <= maxLength)
				return previewText;

			previewText = previewText.Substring(0, maxLength);
			// The maximum number of characters to cut from the end of the string.
			var maxCharCut = (previewText.Length > 15 ? 15 : previewText.Length - 1);
			var previousWord = previewText.LastIndexOfAny(new[] { ' ', '.', ',', '!', '?' }, previewText.Length - 1, maxCharCut);
			if (previousWord <= 0)
			{
				previewText = previewText.Substring(0, previousWord);
			}
			return previewText + " ...";
		}

		public static string StripHtml(this string htmlString)
		{
			if (htmlString == null) return string.Empty;
			var regEx = new Regex(@"<!--.*?-->|<[^>]*>|<[a-z]*>|<\/[a-z]*>|<\/[a-z]*$|^[a-z]*>|^\/[a-z]*>|<[a-z]*$|[a-z]*>");
			var noHtmlString = regEx.Replace(htmlString, " ");
			return noHtmlString;
		}


		public static string GetTimeString(int? minutes)
		{
			if (minutes == null) return "Not specified.";
			var timeSpan = new TimeSpan(0, 0, (int)minutes, 0);
			if (timeSpan.Days > 0) return Convert.ToInt32(timeSpan.TotalHours) + " hours";
			if (timeSpan.Hours > 1 && timeSpan.Minutes > 0) return String.Format("{0} hours {1} mins", timeSpan.Hours, timeSpan.Minutes);
			if (timeSpan.Hours > 1) return String.Format("{0} hours", timeSpan.Hours);
			if (timeSpan.Hours == 1 && timeSpan.Minutes > 0) return String.Format("1 hour {0} mins", timeSpan.Minutes);
			if (timeSpan.Hours == 1) return "1 hour";
			return timeSpan.Minutes + " mins";
		}

		public static string AddSpace(this string input)
		{
			if (string.IsNullOrEmpty(input)) return " ";
			return input + " ";
		}

		public static string AddString(this string input, string @string)
		{
			if (string.IsNullOrEmpty(input)) return null;
			return input + @string;
		}

		public static string GetLede(this string input, int wordCount, int maxChars)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;
			var lede = input.GetLede(wordCount);
			return lede.Length <= maxChars ? lede : lede.Substring(0, maxChars);
		}
	}
}
