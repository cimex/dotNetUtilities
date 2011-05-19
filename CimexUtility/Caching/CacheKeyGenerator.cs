using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using CimexUtility.Conversions;

namespace CimexUtility.Caching
{
	public class CacheKeyGenerator
	{
		private List<object> level2Objects = new List<object>();
		public readonly object Input;

		public CacheKeyGenerator(object input)
		{
			Input = input;
		}

		public string GetLevel2Key()
		{
			if (Input == null) return string.Empty;

			var keys = new StringBuilder();

			var level1Key = getObjectProperties(Input, 1);
			keys.Append(level1Key);

			foreach (var obj in level2Objects)
			{
				var level2Key = getObjectProperties(obj, 2);
				keys.Append(level2Key);
			}

			return keys.ToString();
		}

		public string GetHashedLevel2Key()
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(GetLevel2Key(), FormsAuthPasswordFormat.MD5.GetEnumString());
		}

		private string getObjectProperties(object input, int level)
		{
			var type = input.GetType();
			var builder = new StringBuilder();

			var properties = type.GetProperties();
			foreach (var info in properties)
			{
				var value = info.GetValue(input, null);
				if (isComplexProperty(info))
				{
					if(string.IsNullOrEmpty(info.Name)) continue;
					switch (level)
					{
						case 1: level2Objects.Add(value);
							break;
						case 2: continue;
						default:
							return builder.ToString();
					}
					continue;
				}
				builder.Append(info.Name).Append(":").Append(value).Append("|");
			}

			var fields = type.GetFields();
			foreach (var info in fields)
			{
				var value = info.GetValue(input);

				if (isComplexField(info))
				{
					if (string.IsNullOrEmpty(info.Name)) continue;
					switch (level)
					{
						case 1: level2Objects.Add(value);
							break;
						case 2: continue;
						default:
							return builder.ToString();
					}
					continue;
				}
				builder.Append(info.Name).Append(":").Append(value).Append("|");
			}

			return builder.ToString();
		}



		private bool isComplexProperty(PropertyInfo info)
		{
			return info.PropertyType.IsClass &&
				   !info.PropertyType.IsValueType &&
				   !info.PropertyType.IsPrimitive
				   && info.PropertyType.FullName != "System.String";
		}

		private bool isComplexField(FieldInfo info)
		{
			return info.FieldType.IsClass &&
				   !info.FieldType.IsValueType &&
				   !info.FieldType.IsPrimitive
				   && info.FieldType.FullName != "System.String";
		}
	}
}