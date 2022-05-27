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
    internal class TrainerDataAccess
    {


        public string ErrorMessage { get; private set; }
        public List<TrainerDataModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<TrainerDataModel> trainers = new List<TrainerDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select trainerid , trainername, gender, dob, mobilenumber, address, city, state,pincode,yoe from trainer order by trainerid asc";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainerDataModel trainer = new TrainerDataModel();

                                trainer.trainerid = reader.GetInt32(0);
                                trainer.trainername = reader.GetString(1);
                                trainer.gender = reader.GetString(2);
                                trainer.dob = reader.GetDateTime(3);
                                trainer.mobilenumber = reader.GetString(4);
                                trainer.address = reader.GetString(5);
                                trainer.city = reader.GetString(6);
                                trainer.state = reader.GetString(7);
                                trainer.pincode = reader.GetString(8);
                                trainer.yoe = reader.GetInt32(9);





                                trainers.Add(trainer);
                            }
                        }
                    }
                }
                return trainers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }






        public TrainerDataModel GetTrainerById(int id)

        {
            try
            {

                ErrorMessage = "";

                TrainerDataModel trainer= null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select trainerid , trainername, gender, dob, mobilenumber, address, city, state,pincode,yoe from trainer where trainerid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                trainer = new TrainerDataModel();
                                trainer.trainerid = reader.GetInt32(0);
                                trainer.trainername = reader.GetString(1);
                                trainer.gender = reader.GetString(2);
                                trainer.dob = reader.GetDateTime(3);
                                trainer.mobilenumber = reader.GetString(4);
                                trainer.address = reader.GetString(5);
                                trainer.city = reader.GetString(6);
                                trainer.state = reader.GetString(7);
                                trainer.pincode = reader.GetString(8);
                                trainer.yoe = reader.GetInt32(9);


                            }
                        }
                    }
                }
                return trainer;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }







        public List<TrainerDataModel> GetTrainerByName(string name)
        {
            try
            {
                ErrorMessage = "";

                TrainerDataModel trainer = null;
                List<TrainerDataModel> trainers = new List<TrainerDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select trainerid , trainername, gender, dob, mobilenumber, address, city, state,pincode,yoe from trainer where trainername like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                trainer = new TrainerDataModel();
                                trainer.trainerid = reader.GetInt32(0);
                                trainer.trainername = reader.GetString(1);
                                trainer.gender = reader.GetString(2);
                                trainer.dob = reader.GetDateTime(3);
                                trainer.mobilenumber = reader.GetString(4);
                                trainer.address = reader.GetString(5);
                                trainer.city = reader.GetString(6);
                                trainer.state = reader.GetString(7);
                                trainer.pincode = reader.GetString(8);
                                trainer.yoe = reader.GetInt32(9);
                                trainers.Add(trainer);


                            }
                        }
                    }
                }
                return trainers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }



        public TrainerDataModel Insert(TrainerDataModel newTrainer)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Insert into trainer (trainername, gender, dob, mobilenumber, address, city, state,pincode,yoe) values ('{newTrainer.trainername}','{newTrainer.gender}','{newTrainer.dob.ToString("yyyy-MM-dd")}','{newTrainer.mobilenumber}','{newTrainer.address}','{newTrainer.city}','{newTrainer.state}','{newTrainer.pincode}',{newTrainer.yoe}); select scope_identity()";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newTrainer.trainerid = idInserted;
                            return newTrainer;
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





        public TrainerDataModel Update(TrainerDataModel updTrainer)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.trainer SET trainername = '{updTrainer.trainername}', " +
                        $"gender = '{updTrainer.gender}'," +
                        $"dob='{updTrainer.dob.ToString("yyyy-MM-dd")}'," +
                         $"mobilenumber = '{updTrainer.mobilenumber}'," +
                         $"address='{updTrainer.address}'," +
                        $"city='{updTrainer.city}'," +
                        $"state='{updTrainer.state}'," +
                         $"pincode = '{updTrainer.pincode}'," +
                         $"yoe = {updTrainer.yoe}" +

                        $"where trainerid = {updTrainer.trainerid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updTrainer;
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
                    string sqlStmt = $"DELETE FROM dbo.trainer Where trainerid={id}";
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





