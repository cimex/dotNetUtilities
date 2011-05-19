using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace CimexUtility.Caching
{
	/// <summary>
	/// Utility for managing HttpContext cache.
	/// </summary>
	public static class CacheUtility
	{
		/// <summary>
		/// Extension method for using a sql cache dependency for Linq-Sql on a static lookup table.
		/// Requires a timestamp to be present on a table plus SqlCache dependency enabled on the db in question.
		/// </summary>
		public static List<T> LinqCache<T>(this Table<T> query, HttpContext httpContext) where T : class
		{
			var tableName = query.Context.Mapping.GetTable(typeof(T)).TableName;
			var result = httpContext.Cache[tableName] as List<T>;

			if (result == null)
			{
				using (var cn = new SqlConnection(query.Context.Connection.ConnectionString))
				{
					cn.Open();
					var cmd = new SqlCommand(query.Context.GetCommand(query).CommandText, cn);
					cmd.Notification = null;
					cmd.NotificationAutoEnlist = true;
					SqlCacheDependencyAdmin.EnableNotifications(query.Context.Connection.ConnectionString);
					if (!SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(query.Context.Connection.ConnectionString).Contains(tableName))
					{
						SqlCacheDependencyAdmin.EnableTableForNotifications(query.Context.Connection.ConnectionString, tableName);
					}

					var dependency = new SqlCacheDependency(cmd);
					cmd.ExecuteNonQuery();

					result = query.ToList();
					httpContext.Cache.Insert(tableName, result, dependency);
				}
			}
			return result;
		}

		/// <summary>
		/// Clears the cache in from the injected HttpContext
		/// </summary>
		public static void ClearCache(this Cache input)
		{
			var keyList = new List<string>();
			var cacheEnum = input.GetEnumerator();

			while (cacheEnum.MoveNext())
			{
				keyList.Add(cacheEnum.Key.ToString());
			}
			foreach (var key in keyList)
			{
				input.Remove(key);
			}
		}
	}
}