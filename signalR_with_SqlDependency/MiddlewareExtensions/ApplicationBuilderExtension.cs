using signalR_with_SqlDependency.SubscribeTableDependencies;

namespace signalR_with_SqlDependency.MiddlewareExtensions
{
	public static class ApplicationBuilderExtension
	{
		public static void UseSqlTableDependency<T>(this IApplicationBuilder app, string _conn) where T : ISubscribeTableDependency
		{
			var serviceProvider = app.ApplicationServices;
			var serivce = serviceProvider.GetService<T>();
			serivce.SubscribeTableDependency(_conn);
		}
	}
}
