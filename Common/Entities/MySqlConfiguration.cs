using System;
using System.Data.Entity;

namespace HelloHome.Common.Entities
{
	public class MySqlConfiguration : DbConfiguration
	{
		public MySqlConfiguration ()
		{
			SetHistoryContext ("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext (conn, schema));
		}
	}
}
