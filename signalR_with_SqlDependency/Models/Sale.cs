namespace signalR_with_SqlDependency.Models
{
	public class Sale
	{
		public int SaleId { get; set; }
		public string? Customer { get; set; }
		public decimal SaleAmount { get; set; }
		public string? SaleDate { get; set; }
		public bool isActive { get; set; }
	}
}
