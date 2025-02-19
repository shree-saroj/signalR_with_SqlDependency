using Microsoft.AspNetCore.SignalR;
using signalR_with_SqlDependency.Models;
using signalR_with_SqlDependency.Repositories;

namespace signalR_with_SqlDependency.Hubs
{
	public class DashboardHub : Hub
	{
		ProductRepository productRepo;
		SaleReporisotry salesRepo;

		public DashboardHub(IConfiguration configuration)
		{
			string? connectionString = configuration.GetConnectionString("DefaultConnection");
			productRepo = new ProductRepository(connectionString);
			salesRepo = new SaleReporisotry(connectionString);
		}

		public async Task sendProduct()
		{
			List<Product> products = productRepo.GetProducts();
			await Clients.All.SendAsync("ReceivedProducts", products);
		}
		public async Task sendSales()
		{
			List<Sale> sales = salesRepo.GetSales();
			await Clients.All.SendAsync("ReceivedSales", sales);
		}
	}
}
