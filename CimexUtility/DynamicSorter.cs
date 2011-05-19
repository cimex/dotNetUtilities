using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CimexUtility
{
	/// <summary>
	/// Sorter which converts a string representation of a column name to 
	/// allow for dynamic sorting of a specified type. Use when calling the 'Sort' 
	/// command as a method group.
	/// </summary>
	/// <typeparam name="T">Object type to sort.</typeparam>
	public class DynamicSorter<T> : IComparer<T>
	{
		public DynamicSorter(bool isAscending, string propertyName)
		{
			this.isAscending = isAscending;
			this.propertyName = propertyName;
		}

		public int Compare(T x, T y)
		{
			if (propertyInfo == null) propertyInfo = getProperty();
			var xValue = propertyInfo.GetValue(x, null) as string;
			var yValue = propertyInfo.GetValue(y, null) as string;
			if (xValue == null || yValue == null) return 0;
			return xValue.CompareTo(yValue)*(isAscending ? 1 : -1);
		}

		private bool isAscending { get; set; }
		private string propertyName { get; set; }
		private PropertyInfo propertyInfo;

		private PropertyInfo getProperty()
		{
			var type = typeof (T);
			return type.GetProperty(propertyName);
		}
	}
}