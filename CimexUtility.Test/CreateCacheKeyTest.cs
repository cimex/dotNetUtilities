using CimexUtility.Caching;
using NUnit.Framework;
using System.Diagnostics;
using NUnit.Framework.SyntaxHelpers;

namespace CimexUtilityTest
{
	[TestFixture]
	public class CreateCacheKeyTest
	{

		[Test]
		public void should_create_cache_key_from_simple_object()
		{
			var cacheKey = new CacheKeyGenerator(simpleObject1).GetLevel2Key();
			Debug.WriteLine(cacheKey);
			Debug.WriteLine("Variable1:apple|Variable2:banana|Variable3:45|Field1:Giraffe|Variable1:brick|Variable2:wall|Variable3:35|Field1:Giraffe|");
		}


		[Test]
		public void should_get_different_keys_for_different_object_values()
		{

			var cacheKey1 = new CacheKeyGenerator(simpleObject1).GetLevel2Key();
			var cacheKey2 = new CacheKeyGenerator(simpleObject2).GetLevel2Key();
			
			Debug.WriteLine(cacheKey1);
			Debug.WriteLine(cacheKey2);
			
			Assert.That(cacheKey1,Is.Not.EqualTo(cacheKey2));
		}



		[Test]
		public void should_get_same_hash_keys_for_different_object_with_same_values()
		{

			var cacheKey1 = new CacheKeyGenerator(simpleObject1).GetHashedLevel2Key();
			var cacheKey2 = new CacheKeyGenerator(simpleObject1Clone).GetHashedLevel2Key();

			Debug.WriteLine(cacheKey1);
			Debug.WriteLine(cacheKey2);

			Assert.That(cacheKey1, Is.EqualTo(cacheKey2));
		}


		[Test]
		public void should_get_different_hash_keys_for_objects_with_different_values()
		{

			var cacheKey1 = new CacheKeyGenerator(simpleObject1).GetHashedLevel2Key();
			var cacheKey2 = new CacheKeyGenerator(simpleObject2).GetHashedLevel2Key();

			Debug.WriteLine(cacheKey1);
			Debug.WriteLine(cacheKey2);

			Assert.That(cacheKey1, Is.Not.EqualTo(cacheKey2));
		}

		[Test]
		public void should_get_same_key_for_different_object_with_same_values()
		{
			
			var cacheKey1 = new CacheKeyGenerator(simpleObject1).GetLevel2Key();
			var cacheKey2 = new CacheKeyGenerator(simpleObject1Clone).GetLevel2Key();

			Debug.WriteLine(cacheKey1);
			Debug.WriteLine(cacheKey2);

			Assert.That(cacheKey1, Is.EqualTo(cacheKey2));
		}


		private readonly SimpleObject simpleObject1 = new SimpleObject
		{
			Variable1 = "apple",
			Variable2 = "banana",
			Variable3 = 45
			,
			Simple =
				new SimpleObject { Variable1 = "brick", Variable2 = "wall", Variable3 = 35 }
		};

		private readonly SimpleObject simpleObject1Clone = new SimpleObject
		{
			Variable1 = "apple",
			Variable2 = "banana",
			Variable3 = 45
			,
			Simple =
				new SimpleObject { Variable1 = "brick", Variable2 = "wall", Variable3 = 35 }
		};

		private readonly SimpleObject simpleObject2 = new SimpleObject
		{
			Variable1 = "apples",
			Variable2 = "banana",
			Variable3 = 45
			,
			Simple =
				new SimpleObject { Variable1 = "bricks", Variable2 = "wall", Variable3 = 35 }
		};

		public class SimpleObject
		{
			private string Field1 = "Monkey";
			public string Variable1 { get; set; }
			public string Variable2 { get; set; }
			public int Variable3 { get; set; }
			public SimpleObject Simple { get; set; }
		}

	}


}