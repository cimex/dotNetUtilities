using System.Diagnostics;
using CimexUtility.StringManipulation;
using NUnit.Framework;

namespace CimexUtilityTest
{
	[TestFixture]
	public class StringManipulation
	{

		[Test]
		public void should_not_convert_apostrophe_to_html_encoding_when_strippingHtml()
		{
			var text = "Hello there, I'm very happy.";
			var notHtml = text.StripHtml();
			Assert.That(notHtml.Contains("'"));
		}

		[Test]
		public void should_strip_angle_bracke_when_strippingHtml()
		{
			var text = @"asdfasdf<asdfsf>";
			var notHtml = text.StripHtml();
			StringAssert.AreEqualIgnoringCase("Hello there , I'm very happy. ", notHtml);
		}


		[Test]
		public void should_trim_characters_to_specified_length_and_also_orphan_characters()
		{
			var longText = "When a user submits a response to a partners brief";
			var expected = "When a user...";
			var actual = longText.CharLimit(16, true);
			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void should_trim_characters_to_specified_length_and_also_orphan_characters_and_remove_fullStop()
		{
			var longText = "When a user. submits. a response to a partners brief";
			var expected = "When a user...";
			var actual = longText.CharLimit(16, true);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void should_get_lede_and_remove_fullStop()
		{
			var longText = "When a user. submits. a response to a partners brief";
			var expected = "When a user...";
			var actual = longText.GetLede(3);
			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void should_get_lede_and_and_x_amount_of_chars()
		{
			var longText = "SomeReallyMegaLongNameICantPronounce@Somestupidlylongdomain.co.uk";
			var expected = "SomeReallyMega";

			var actual = longText.GetLede(6, 14);

			Debug.WriteLine(actual);
			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void should_create_English_language_list()
		{
			StringAssert.AreEqualIgnoringCase("Apples", new[] { "Apples"}.ToEnglishLanguageList());
			StringAssert.AreEqualIgnoringCase("Apples and Bananas", new[] { "Apples", "Bananas"}.ToEnglishLanguageList());
			StringAssert.AreEqualIgnoringCase("Apples, Bananas and Monkeys", new[] { "Apples", "Bananas", "Monkeys" }.ToEnglishLanguageList());
		}

		[Test]
		public void should_add_space_to_string()
		{
			var word = "The quick";
			var words = word.AddSpace().AddString("brown");
			StringAssert.AreEqualIgnoringCase(words, "The quick brown");
		}
		


	}
}