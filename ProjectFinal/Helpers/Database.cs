using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectFinal.Helpers

{
    internal class Database
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=.\\SQLExpress; Initial Catalog=projectdb;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }
    }
}
