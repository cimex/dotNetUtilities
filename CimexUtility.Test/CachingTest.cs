using System.Linq;
using NUnit.Framework;
using Web.Mocks;
using System.Web;
using System;
using CimexUtility.Caching;
using System.Diagnostics;

namespace CimexUtilityTest
{
	[TestFixture]
	public class CachingTest
	{
		readonly HttpContext context = (new MockHttpContext(true)).Context;

		[Test]
		public void ShouldClearCache()
		{
			var key = Guid.NewGuid().ToString();
			context.Cache.Insert(key,"hello");
			var cache = context.Cache[key];
			Debug.WriteLine(cache);
			Assert.IsNotNull(cache);
			
			context.Cache.ClearCache();
			cache = context.Cache[key];
			Debug.WriteLine(cache);
			Assert.IsNull(cache);
		}


	}
}
