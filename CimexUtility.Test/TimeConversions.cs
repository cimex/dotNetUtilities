using System;
using System.Diagnostics;
using NUnit.Framework;
using CimexUtility.Conversions;

namespace CimexUtility.Test
{
	[TestFixture]
	public class TimeConversions
	{
		[Test]
		public void should_convert_back_and_forth_from_UTC_to_UnixTimeStamp()
		{
			var date = new DateTime(1990, 01, 01);
			var unixTime = date.UnixTimeStampFromUtc();
			var convertedBackDate = unixTime.UtcFromUnixTimestamp();
			Assert.AreEqual(date, convertedBackDate);
		}


		[Test]
		public void should_convert_back_and_forth_from_UnixTimeStamp_to_UTC()
		{
			const double unixTime = 1305888564484;
			var date = unixTime.UtcFromUnixTimestamp();
			var convertedBackTime = date.UnixTimeStampFromUtc();
			Assert.AreEqual(unixTime, convertedBackTime);
		}
	}
}