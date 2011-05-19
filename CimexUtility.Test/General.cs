using System;
using System.Diagnostics;
using System.Linq;
using CimexUtility.Miscellaneous;
using CimexUtility.Validation;
using NUnit.Framework;

namespace CimexUtilityTest
{
	[TestFixture]
	public class ESkillsUtilityTest
	{
		[Test]
		public void Should_flip_strings()
		{
			var animalA = "Dog";
			var animalB = "Cat";

			Debug.WriteLine(animalA, animalB);
			VariableManipulation.FlipVariables(ref animalA, ref animalB);
			Debug.WriteLine(animalA, animalB);

			Assert.AreEqual("Dog", animalB);
			Assert.AreEqual("Cat", animalA);
		}

		[Test]
		public void Should_flip_ints()
		{
			var animalA = 1;
			var animalB = 2;

			Debug.WriteLine(animalA + " " + animalB);
			VariableManipulation.FlipVariables(ref animalA, ref animalB);
			Debug.WriteLine(animalA + " " + animalB);

			Assert.AreEqual(1, animalB);
			Assert.AreEqual(2, animalA);
		}

		[Test]
		public void Should_flip_Guids()
		{
			var animalA = new Guid("7C279641-C303-4416-928F-EFAA4BB5DA17");
			var animalB = new Guid("C323CF8C-0C4C-48AE-B05C-C033692002AD");

			Debug.WriteLine(animalA + " " + animalB);
			VariableManipulation.FlipVariables(ref animalA, ref animalB);
			Debug.WriteLine(animalA + " " + animalB);

			Assert.AreEqual("7C279641-C303-4416-928F-EFAA4BB5DA17".ToLower(), animalB.ToString());
			Assert.AreEqual("C323CF8C-0C4C-48AE-B05C-C033692002AD".ToLower(), animalA.ToString());
		}

		[Test]
		public void should_validate_guid()
		{
			var guid = Guid.NewGuid();
			var actual = guid.ToString();
			Debug.WriteLine(actual);
			Assert.IsTrue(actual.IsGuid());
		}

		[Test]
		public void should_take_10_from_a_shorter_list()
		{
			var sixItems = new[] { "apple", "pear", "monkey", "dog", "elephant", "donkey", "bird" };
			var items = sixItems.Take(10);
			Assert.AreEqual(7, items.Count());
		}

		[Test]
		public void should_replace_dollar_sign_with_dash()
		{
			const string text = "$ hello and how are you.";
			var newText = text.Replace("$", "-");
			Debug.WriteLine(newText);
			Assert.IsTrue(newText.Contains("-"));
		}
	}
}
