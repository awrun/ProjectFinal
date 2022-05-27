using System.Data.SqlClient;
using ProjectFinal.Helpers;
using ProjectFinal.Models;

namespace ProjectFinal.DataAccess
{
    public class DashBoardDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DashBoardDataModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashBoardDataModel();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as TrainerCount from trainer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.TrainerCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as CustomerCount from customer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CustomerCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

    }
}
