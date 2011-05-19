using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using CimexUtility.Miscellaneous;

namespace CimexUtility
{
	/// <summary>
	/// Maps a dictionary list of properties with string values to the
	/// appropriate field converted to the appropriate type for the class type given.
	/// Used specifically where an Asp.Net control returns a list of NewValues with its UpdateEventArgs.
	/// </summary>
	public class DynamicMapper<T> where T : new()
	{
		/// <param name="propertyValues">A list of property values in key value pairs</param>
		/// <param name="instance">An instance of the class of type T.</param>
		public DynamicMapper(IDictionary propertyValues, T instance)
		{
			this.propertyValues = propertyValues;
			this.instance = instance;
		}

		public DynamicMapper()
		{
		}

		/// <summary>
		/// Get the instance with updated fields according to the mapped propertyValues.
		/// </summary>
		/// <returns></returns>
		public T GetMappedInstance()
		{
			foreach (DictionaryEntry entry in propertyValues)
			{
				var property = getProperty(entry.Key.ToString());
				if (property == null) break;
				property.SetValue(instance, convertToType(entry.Value, property.PropertyType), null);
			}
			return instance;
		}

		private PropertyInfo getProperty(string propertyName)
		{
			var type = typeof (T);
			return type.GetProperty(propertyName);
		}

		private IDictionary propertyValues { get; set; }
		private T instance { get; set; }

		private object convertToType(object value, Type type)
		{
			switch (type.Name)
			{
				case "String":
					return value;
				case "Guid":
					return new Guid((string) value);
				case "Int32":
					return Convert.ToInt32(value);
				case "Boolean":
					return Convert.ToBoolean(value);
				case "Nullable`1":
					return getNullableType(type, value);
				case "DateTime":
					return DateTime.Parse(value as string);
				default:
					return value;
			}
		}

		private object getNullableType(Type type, object value)
		{
			var converter = new NullableConverter(type);
			var nullableType = converter.UnderlyingType;

			switch (nullableType.Name)
			{
				case "Int32":
					return  Conversions.TypeParser.TryParse((string) value);
				case "DateTime":
					return (DateTime?) value;
				case "Guid":
					return (Guid?) value;
				default:
					return (int?) value;
			}
		}
	}
}