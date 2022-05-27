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
    public class PackageDataAccess
    {


        public string ErrorMessage { get; private set; }
        public List<PackageDataModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<PackageDataModel> packages = new List<PackageDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select packageid,duration,amount from package order by packageid asc";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PackageDataModel package = new PackageDataModel();

                                package.packageid = reader.GetInt32(0);
                                package.duration = reader.GetString(1);
                                package.amount = reader.GetInt32(2);






                                packages.Add(package);
                            }
                        }
                    }
                }
                return packages;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        public PackageDataModel GetPackageById(int id)

        {
            try
            {

                ErrorMessage = "";

                PackageDataModel package = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select packageid , duration, amount from package where packageid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                package = new PackageDataModel();
                                package.packageid = reader.GetInt32(0);
                                package.duration = reader.GetString(1);
                                package.amount = reader.GetInt32(2);



                            }
                        }
                    }
                }
                return package;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }

















        public PackageDataModel Insert(PackageDataModel newPackage)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Insert into package (duration, amount) values ('{newPackage.duration}',{newPackage.amount}); select scope_identity()";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newPackage.packageid = idInserted;
                            return newPackage;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return null;
            }
        }





        public PackageDataModel Update(PackageDataModel updPackage)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.package SET duration = '{updPackage.duration}', " +
                        $"amount={updPackage.amount}" +
                        $"where packageid = {updPackage.packageid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updPackage;
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



        public int Delete(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                int numOfRows = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM dbo.package Where packageid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();
                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }


        }
    }
}





