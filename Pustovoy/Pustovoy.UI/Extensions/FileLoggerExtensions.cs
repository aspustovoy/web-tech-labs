using Pustovoy.UI.Middleware;

namespace Pustovoy.UI.Extensions
{
	public static class FileLoggerExtensions
	{
		public static IApplicationBuilder UseFileLogger(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<FileLogger>();
		}
	}
}
