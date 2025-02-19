using signalR_with_SqlDependency.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace signalR_with_SqlDependency.Repositories
{
	public class SaleReporisotry
	{
		string? _conn;
		public SaleReporisotry(string conn)
		{
			this._conn = conn;
		}
		public List<Sale> GetSales()
		{
			List<Sale> sales = new List<Sale>();
			Sale sale;
			DataTable dt = GetSalesFromDb();
			foreach (DataRow dr in dt.Rows)
			{
				sale = new Sale
				{
					SaleId = Convert.ToInt32(dr["SaleId"]),
					Customer = dr["Customer"].ToString(),
					SaleAmount = Convert.ToDecimal(dr["SaleAmount"]),
					SaleDate = Convert.ToDateTime(dr["SaleDate"]).ToString("yyyy-MM-dd"),
				};
				sales.Add(sale);
			}
			return sales;
		}
		public DataTable GetSalesFromDb()
		{
			string query = "Usp_L_S_Sales";
			DataTable dt = new DataTable();

			try
			{
				using (SqlConnection con = new SqlConnection(_conn))
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
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return dt;
		}

	}
}
