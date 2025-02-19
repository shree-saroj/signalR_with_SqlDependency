using signalR_with_SqlDependency.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace signalR_with_SqlDependency.Repositories
{
	public class ProductRepository
	{
		string _conn;
		public ProductRepository(string conn)
		{
			this._conn = conn;
		}

		public List<Product> GetProducts()
		{
			List<Product> products = new List<Product>();
			Product prod;
			DataTable dt = GetProductDetailsFromDb();
			foreach(DataRow dr in dt.Rows)
			{
				prod = new Product
				{
					ProductId = Convert.ToInt32(dr["ProductId"]),
					ProductName = dr["ProductName"].ToString(),
					ProductCategory = dr["ProductCategory"].ToString(),
					ProductPrice = Convert.ToDecimal(dr["ProductPrice"])
				};
				products.Add(prod);
			}
			return products;
		}
		public DataTable GetProductDetailsFromDb()
		{
			string query = "usp_L_S_ProdcutInfo";
			DataTable dt = new DataTable();
			using (SqlConnection con = new SqlConnection(_conn))
			{
				try
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							dt.Load(reader);
						}
					}
					return dt;
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					con.Close();
				}
			}
		}
	}
}
