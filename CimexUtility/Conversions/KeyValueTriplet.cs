using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace CimexUtility.Conversions
{
	/// <summary>
	/// Defines a key/numeric key/value triplet that can be set or retrieved.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <typeparam name="TDescription"></typeparam>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
	public struct KeyValueTriplet<TKey, TValue, TDescription>
	{
		#region class-wide fields
		private readonly TKey key;
		private readonly TDescription description;
		private readonly TValue value;
		#endregion

		#region public properties and methods

		#region properties

		#region Key
		/// <summary>
		/// Gets the key in the key/numeric key/value triplet.
		/// </summary>
		/// <value>A <typeparamref name="TKey"/> that is the key of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>.</value>
		public TKey Key
		{
			get
			{
				return key;
			}
		}
		#endregion

		#region Value
		/// <summary>
		/// Gets the numeric representation of the <see cref="Key"/> in the key/numeric key/value triplet.
		/// </summary>
		/// <value>A <typeparamref name="TValue"/> that is the numeric key of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>.</value>
		public TValue Value
		{
			get
			{
				return value;
			}
		}
		#endregion

		#region Description
		/// <summary>
		/// Gets the value in the key/numeric key/value triplet.
		/// </summary>
		/// <value>A <typeparamref name="TValue"/> that is the value of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>.</value>
		public TDescription Description
		{
			get
			{
				return description;
			}
		}
		#endregion

		#endregion

		#region methods

		#region constructor
		/// <summary>
		/// Inititalizes a new instance of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>
		/// structure with the specified key, numeric key, and value.
		/// </summary>
		/// <param name="key">The object defined in each key/numeric key/value triplet.</param>
		/// <param name="numericKey">The numeric representation of each <paramref name="key"/> 
		/// defined in each key/numeric key/value triplet.</param>
		/// <param name="value">The definition associate with <paramref name="key"/>.</param>
		public KeyValueTriplet(TKey key, TValue numericKey, TDescription value)
		{
			this.key = key;
			description = value;
			this.value = numericKey;
		}
		#endregion

		#region ToString
		/// <summary>
		/// Returns a string representation of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>,
		/// using the string representations of the key, numeric key, and value.
		/// </summary>
		/// <returns>A string representation of the <see cref="KeyValueTriplet{TKey, TNumericKey, TValue}"/>,
		/// which includes the string representations of the key, numeric key, and value.</returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append('[');
			if (Key != null)
			{
				builder.Append(Key.ToString());
			}
			builder.Append(", ");
			if (Value != null)
			{
				builder.Append(Value.ToString());
			}
			builder.Append(", ");
			if (Description != null)
			{
				builder.Append(Description.ToString());
			}
			builder.Append(']');
			return builder.ToString();
		}
		#endregion

		#endregion

		#endregion
	}
}