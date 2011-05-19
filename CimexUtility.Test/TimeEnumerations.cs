using System.Linq;
using CimexUtility.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CimexUtilityTest
{
	[TestFixture]
	public class TimeEnumerationsTest
	{
		[Test]
		public void should_get_hours_of_the_day()
		{
			var hours = TimeEnumerations.HoursInDay;
			Assert.That(hours.First(), Is.EqualTo("00"));
			Assert.That(hours.Last(), Is.EqualTo("23"));
		}

		[Test]
		public void should_get_minutes_in_hour()
		{
			var minutes = TimeEnumerations.MinutesInHour;
			Assert.That(minutes.First(), Is.EqualTo("00"));
			Assert.That(minutes.Last(), Is.EqualTo("59"));
		}

	}
}