using System.Linq;
using System.Web;

namespace CimexUtility.Web
{
	public class FilePathUtility
	{
		///<summary>
		/// Provides a way to access common variations of an Asp.Net virtual file path within the current HttpContext.
		/// </summary>
		/// <param name="applicationVirtualDirectory">The tilde ~/ form of relative paths in asp.net.</param>
		/// <param name="httpContext">The current HttpContext</param>
		public FilePathUtility(string applicationVirtualDirectory, HttpContext httpContext)
		{
			VirtualDirectory = VirtualPathUtility.ToAbsolute(applicationVirtualDirectory);
			HttpContext = httpContext;
		}
		public string FileName { get; set; }
		public string FilePath
		{
			get { return string.IsNullOrEmpty(VirtualFilePath) ? null : HttpContext.Server.MapPath(VirtualFilePath); ; }
		}
		public string VirtualFilePath
		{
			get { return VirtualDirectory + FileName; }
		}
		public string VirtualDirectory { set; get; }
		public HttpContext HttpContext { get; set; }
		public string AbsoluteUri
		{
			get { return GetWebApplicationRootUri() + VirtualFilePath; }
		}

		public static string GetWebApplicationRootUri()
		{
			if (HttpContext.Current == null) throw new HttpException(400, "This method cannot be used outside of an HttpContext.");
			return string.Format(
				(HttpContext.Current.Request.Url.Port != 80) ? "{0}://{1}:{2}" : "{0}://{1}",
				HttpContext.Current.Request.Url.Scheme,
				HttpContext.Current.Request.Url.Host,
				HttpContext.Current.Request.Url.Port);
		}
	}
}