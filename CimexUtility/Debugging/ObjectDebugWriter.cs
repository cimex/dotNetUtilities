using System;
using System.Diagnostics;
using System.Linq;

namespace CimexUtility.Debugging
{
	public static class ObjectDebugWriter
	{
		/// <summary>
		/// Cycles through an objects properties and prints out their values to the console.
		/// </summary>
		public static void PrintObjectProperties(this object input)
		{
			if (input == null) return;
			writeObjectTierProperties(input);
		}

		private static void writeObjectTierProperties(object instance)
		{
			Debug.WriteLine(string.Format("****** {0}", instance.GetType().Name.ToUpper()));
			Object[] obj = null;
			foreach (var info1 in instance.GetType().GetProperties())
			{
				if (info1 == null) return;
				var name = info1.Name;
				try
				{
					var value = info1.GetValue(instance, obj) ?? string.Empty;
					writeMemberMessage(name, value);
				}
				catch (Exception ex)
				{
				 	writeMemberMessage(info1.Name, ex.Message);
					throw;
				}
			}
			Debug.WriteLine(string.Format("****** {0}", instance.GetType().Name.ToUpper()));
			Debug.WriteLine("\n");
		}

		private static void writeMemberMessage(string name, object value)
		{
			Debug.WriteLine(string.Format("{0} : ", name) + value);
		}


		/// <summary>
		/// Writes out a list of chars with their associated int representation.
		/// </summary>
		/// <param name="input"></param>
		public static void CharNumericDebugWriter(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				Debug.WriteLine("String is null or empty.");
				return;
			}
			var charArray = input.ToCharArray();
			foreach (var character in charArray)
			{
				Debug.WriteLine(string.Format("Character :{0} Value: {1}", character, (int)character));
			}
		}
	}
}
