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
    internal class TrainingDataAccess
    {


        public string ErrorMessage { get; private set; }
        public List<TrainingViewModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<TrainingViewModel> trainings = new List<TrainingViewModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select a.trainingid,b.trainerid,b.trainername,c.customerid,c.customername,d.packageid,d.amount,a.startdate,a.enddate,a.status from dbo.training a INNER JOIN dbo.trainer b on a.trainerid = b.trainerid INNER JOIN dbo.customer c on c.customerid = a.customerid INNER JOIN dbo.package d on d.packageid = a.packageid";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainingViewModel training = new TrainingViewModel();

                                training.trainingid = reader.GetInt32(0);
                                training.trainerid = reader.GetInt32(1);
                                training.trainername = reader.GetString(2);
                                training.customerid = reader.GetInt32(3);
                                training.customername = reader.GetString(4);
                                training.packageid = reader.GetInt32(5);
                                training.amount=reader.GetInt32(6);
                                training.startdate = reader.GetDateTime(7); 
                                training.enddate = reader.GetDateTime(8); 
                                training.status = reader.GetString(9);
                               




                                trainings.Add(training);
                            }
                        }
                    }
                }
                return trainings;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }






        public TrainingDataModel GetTrainingById(int id)

        {
            try
            {

                ErrorMessage = "";

                TrainingDataModel training = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select trainingid , trainerid, customerid, packageid, startdate, enddate, status from training where trainingid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                training = new TrainingDataModel();
                                training.trainingid = reader.GetInt32(0);
                                training.trainerid = reader.GetInt32(1);
                                training.customerid = reader.GetInt32(2);
                                training.packageid = reader.GetInt32(3);
                                training.startdate = reader.GetDateTime(4);
                                training.enddate = reader.GetDateTime(5);
                                training.status = reader.GetString(6);
                                


                            }
                        }
                    }
                }
                return training;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }







      



        public TrainingDataModel Insert(TrainingDataModel newTraining)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Insert into training (trainerid, customerid, packageid, startdate, enddate, status) values ({newTraining.trainerid},{newTraining.customerid},{newTraining.packageid},'{newTraining.startdate.ToString("yyyy-MM-dd")}','{newTraining.enddate.ToString("yyyy-MM-dd")}''{newTraining.status}'); select scope_identity()";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newTraining.trainingid = idInserted;
                            return newTraining;
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





        public TrainingDataModel Update(TrainingDataModel updTraining)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.training SET trainerid = {updTraining.trainerid}, " +
                        $"customerid = {updTraining.customerid}," +
                        $"packageid = {updTraining.packageid}," +
                        $"startdate='{updTraining.startdate.ToString("yyyy-MM-dd")}'," +
                        $"enddate='{updTraining.enddate.ToString("yyyy-MM-dd")}'," +
                        $"status='{updTraining.status}'," +
                        $"where trainingid = {updTraining.trainingid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updTraining;
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
                    string sqlStmt = $"DELETE FROM dbo.training Where trainingid={id}";
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





