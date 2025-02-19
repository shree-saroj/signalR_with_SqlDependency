using signalR_with_SqlDependency.Hubs;
using signalR_with_SqlDependency.Models;
using TableDependency.SqlClient;

namespace signalR_with_SqlDependency.SubscribeTableDependencies
{
	public class SubscribeSaleTableDependency : ISubscribeTableDependency
	{
		SqlTableDependency<Sale> saleTableDependency;
		DashboardHub dHub;
		public SubscribeSaleTableDependency(DashboardHub dhub)
		{
			this.dHub = dhub;
		}
		public void SubscribeTableDependency(string _conn)
		{
			saleTableDependency = new SqlTableDependency<Sale>(_conn);
			saleTableDependency.OnChanged += TableDependency_OnChanged;
			saleTableDependency.OnError += TableDependency_OnError;
			saleTableDependency.Start();
		}
		public void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Sale> e)
		{
			if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
			{
				dHub.sendSales();
			}
		}
		public void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
		{
			Console.WriteLine($"{nameof(Sale)} SqlTableDependency Error: {e.Error.ToString()}");
		}
	}
}
