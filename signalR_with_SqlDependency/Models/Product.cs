namespace signalR_with_SqlDependency.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductCategory { get; set; }
		public decimal ProductPrice { get; set; }
		public bool isActive { get; set; }
	}
}
