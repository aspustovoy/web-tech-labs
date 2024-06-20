using Serilog;

namespace Pustovoy.UI.Middleware
{
	public class FileLogger
	{
		private readonly RequestDelegate _next;
		public FileLogger(RequestDelegate next)
		{
			_next = next;
		}
		public async Task Invoke(HttpContext httpContext)
		{
			await _next(httpContext);
			var code = httpContext.Response.StatusCode;
			var temp = code / 100;
			if (temp != 2)
				Log.Logger.Information($"-- Request {httpContext.Request.Path} returns {code}");
		}
	}
}
