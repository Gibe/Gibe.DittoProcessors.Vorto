using System.Web;

namespace Gibe.DittoProcessors.Vorto.Wrappers
{
	public class HttpContextWrapper : IHttpContextWrapper
	{
		public string AbsolutePath()
		{
			return HttpContext.Current.Request.Url.AbsolutePath;
		}
	}
}