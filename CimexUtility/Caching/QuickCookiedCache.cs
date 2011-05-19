using System;
using System.Web;

namespace CimexUtility.Caching
{
	public class QuickCookiedCache<T> : ICookieCache<T> where T : class
	{
		public QuickCookiedCache(string cookieName, TimeSpan cacheDuration)
		{
			CookieName = cookieName;
			CacheDuration = cacheDuration;
		}

		public string CookieName { get; set; }
		public TimeSpan CacheDuration { get; set; }

		public void SaveObjectToCache(T objectToCache)
		{
			if (objectToCache == null) return;
			var key = getCacheKey(CookieName);
			if (key == null)
			{
				key = Guid.NewGuid();
				saveCacheKey(CookieName, (Guid)key);
			}
			HttpContext.Current.Cache.Insert(key.ToString(),objectToCache, null, DateTime.MaxValue, CacheDuration);
		}
		public T Object
		{
			get
			{
				var key = getCacheKey(CookieName);
				var obj = HttpContext.Current.Cache[key.ToString()];
				return obj as T;
			}
		}
		public void Dispose()
		{
			HttpContext.Current.Response.Cookies.Remove(CookieName);
		}

		private Guid? getCacheKey(string name)
		{
			var cookie = HttpContext.Current.Request.Cookies[name];
			return cookie == null ? new Guid?() : new Guid(cookie.Value);
		}
		private void saveCacheKey(string name, Guid guid)
		{
			var cookie = new HttpCookie(name, guid.ToString());
			HttpContext.Current.Response.Cookies.Add(cookie);
		}
	}

	public interface ICookieCache<T> : IDisposable
	{
		string CookieName { get; set; }
		T Object { get; }
		void SaveObjectToCache(T objectToCache);
	}
}