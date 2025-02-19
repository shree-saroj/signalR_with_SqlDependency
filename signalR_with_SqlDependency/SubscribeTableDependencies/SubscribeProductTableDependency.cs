using signalR_with_SqlDependency.Hubs;
using signalR_with_SqlDependency.Models;
using TableDependency.SqlClient;

namespace signalR_with_SqlDependency.SubscribeTableDependencies
{
	public class SubscribeProductTableDependency : ISubscribeTableDependency
	{
		SqlTableDependency<Product> prodTableDependency;
		DashboardHub dbHub;
		public SubscribeProductTableDependency(DashboardHub _dbHub)
		{
			dbHub = _dbHub;
		}
		public void SubscribeTableDependency(string _conn)
		{
			prodTableDependency = new SqlTableDependency<Product>(_conn);
			prodTableDependency.OnChanged += TableDependency_OnChanged;
			prodTableDependency.OnError += TableDependency_OnError;
			prodTableDependency.Start();
		}
		public void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
		{
			if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
			{
				dbHub.sendProduct();
			}
		}
		public void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
		{
			Console.WriteLine($"{nameof(Product)} SqlTableDependency Error: {e.Error.Message}");
		}
	}
}
