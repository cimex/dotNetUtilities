using System;

namespace CimexUtility.Conversions
{
	/// <summary>
	/// An attribute which provides a description for an enumerated type.
	/// </summary>
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class EnumDescriptionAttribute : Attribute
	{
		/// <summary>
		/// Gets the description stored in this attribute.
		/// </summary>
		/// <value>The description stored in the attribute.</value>
		public string Description { get; private set; }

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="EnumDescriptionAttribute"/> class.
		/// </summary>
		/// <param name="description">The description to store in this attribute.
		/// </param>
		public EnumDescriptionAttribute(string description)
		{
			Description = description;
		}
	}
}