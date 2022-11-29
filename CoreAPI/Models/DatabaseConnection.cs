using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models
{
	public class DatabaseConnection
	{
		public async Task<DataTable> GetTableAsync()
		{
			string connectionString = @"Server=tcp:clineassetsqlpool.sql.azuresynapse.net,1433;Initial Catalog=clinededicatedsqlpool;Persist Security Info=False;User ID=sqladminuser;Password=cline@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				string cmdTxt = @"select * from SwitchMeachineLandingPage";
				using (SqlCommand cmd = new SqlCommand(cmdTxt, con))
				{
					con.Open();
					DataTable dataTable = new DataTable();
					dataTable.Load(await cmd.ExecuteReaderAsync());
					return dataTable;
				}
			}
		}
	}
}
