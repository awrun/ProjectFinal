using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectFinal.Helpers;
using ProjectFinal.Models;

namespace ProjectFinal.DataAccess
{
    internal class ActivateDataAccess
    {


        public string ErrorMessage { get; private set; }
        public List<ActivateViewModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<ActivateViewModel> activates = new List<ActivateViewModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select a.activateid,c.customerid,c.customername, p.packageid, p.amount, a.startdate,a.enddate,a.status from dbo.activate a inner join dbo.customer c on a.customerid = c.customerid inner join dbo.package p on p.packageid = a.packageid ";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivateViewModel activate = new ActivateViewModel();

                                activate.activateid = reader.GetInt32(0);
                                activate.customerid = reader.GetInt32(1);
                                activate.customername=reader.GetString(2);
                                activate.packageid = reader.GetInt32(3);
                                activate.amount=reader.GetInt32(4);
                                activate.startdate = reader.GetDateTime(5);
                                activate.enddate = reader.GetDateTime(6);
                                activate.status = reader.GetString(7);





                                activates.Add(activate);
                            }
                        }
                    }
                }
                return activates;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        public ActivateDataModel GetActivateById(int id)

        {
            try
            {

                ErrorMessage = "";

                ActivateDataModel activate = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select activateid , customerid, packageid,startdate, enddate, status from activate where activateid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                activate = new ActivateDataModel();
                                activate.activateid = reader.GetInt32(0);
                                activate.customerid = reader.GetInt32(1);
                                activate.packageid = reader.GetInt32(2);
                                activate.startdate = reader.GetDateTime(3);
                                activate.enddate = reader.GetDateTime(4);
                                activate.status = reader.GetString(5);



                            }
                        }
                    }
                }
                return activate;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }

        public ActivateDataModel Update(ActivateDataModel updActivate)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.activate SET customerid = {updActivate.customerid}, " +
                        $"packageid={updActivate.packageid}," +
                        $"startdate='{updActivate.startdate.ToString("yyyy-MM-dd")}'," +
                        $"enddate='{updActivate.enddate.ToString("yyyy-MM-dd")}'," +
                        $"status='{updActivate.status}' " +
                        $"where activateid = {updActivate.activateid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updActivate;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
            return null;
        }
    }
}





