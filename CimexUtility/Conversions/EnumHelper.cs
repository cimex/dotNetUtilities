using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CimexUtility.Conversions
{
	/// <summary>
	/// Provides a static utility object of methods and properties to interact
	/// with enumerated types.
	/// </summary>
	public static class EnumHelper
	{
		/// <summary>
		/// Gets the <see cref="DescriptionAttribute" /> of an <see cref="Enum" /> 
		/// type value.
		/// </summary>
		/// <param name="value">The <see cref="Enum" /> type value.</param>
		/// <returns>A string containing the text of the
		/// <see cref="DescriptionAttribute"/>.</returns>
		public static string GetDescription(this Enum value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var description = value.ToString();
			var fieldInfo = value.GetType().GetField(description);
			var attributes =
				(EnumDescriptionAttribute[])
				fieldInfo.GetCustomAttributes(typeof (EnumDescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
			{
				description = attributes[0].Description;
			}
			return description;
		}

		/// <summary>
		/// Converts the <see cref="Enum" /> type to an <see cref="IList{T}" /> 
		/// compatible object.
		/// </summary>
		/// <param name="type">The <see cref="Enum"/> type.</param>
		/// <returns>An <see cref="IList{T}"/> containing the enumerated
		/// type value and description.</returns>
		public static IList ToList(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			var list = new ArrayList();
			Array enumValues = Enum.GetValues(type);

			foreach (Enum value in enumValues)
			{
				list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
			}

			return list;
		}


		/// <summary>
		///  Converts the <see cref="Enum"/> type to an <see cref="IList"/> compatible object.
		/// </summary>
		/// <param name="type">The <see cref="Enum"/> type.</param>
		/// <returns>An <see cref="IList"/> containing the enumerated type value and description.</returns>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter",
			Justification =
				"This is a more advanced use of the ToList function; providing a type parameter has no semantic meaning for this function and would actually make the calling syntax more complicated."
			)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static IList ToExtendedList<T>(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			if (!type.IsEnum)
			{
				throw new ArgumentException("Must be an enumeration", "type");
			}

			var list = new ArrayList();
			Array enumValues = Enum.GetValues(type);

			foreach (Enum value in enumValues)
			{
				list.Add(new KeyValueTriplet<Enum, T, string>(value,
				                                              (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture),
				                                              GetDescription(value)));
			}

			return list;
		}


		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static List<KeyValueTriplet<Enum, T, string>> ToTripletList<T>(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			if (!type.IsEnum)
			{
				throw new ArgumentException("Must be an enumeration", "type");
			}

			var list = new List<KeyValueTriplet<Enum, T, string>>();

			Array enumValues = Enum.GetValues(type);

			foreach (Enum value in enumValues)
			{
				list.Add(new KeyValueTriplet<Enum, T, string>(value,
				                                              (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture),
				                                              GetDescription(value)));
			}

			return list;
		}
	}
}