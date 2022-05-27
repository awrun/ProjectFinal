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
    public class HolidayDataAccess
    {


        public string ErrorMessage { get; private set; }
        public List<HolidayDataModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<HolidayDataModel> holidays = new List<HolidayDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select holidayid,description,date from holiday order by holidayid asc";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HolidayDataModel holiday = new HolidayDataModel();

                                holiday.holidayid = reader.GetInt32(0);
                                holiday.description = reader.GetString(1);
                                holiday.date = reader.GetDateTime(2);
                               





                                holidays.Add(holiday);
                            }
                        }
                    }
                }
                return holidays;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }






        public HolidayDataModel GetHolidayById(int id)

        {
            try
            {

                ErrorMessage = "";

                HolidayDataModel holiday = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select holidayid , description, date from holiday where holidayid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                holiday = new HolidayDataModel();
                                holiday.holidayid = reader.GetInt32(0);
                                holiday.description = reader.GetString(1);
                                holiday.date = reader.GetDateTime(2);
                               


                            }
                        }
                    }
                }
                return holiday;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }







        public List<HolidayDataModel> GetHolidayByName(string name)
        {
            try
            {
                ErrorMessage = "";

                HolidayDataModel holiday = null;
                List<HolidayDataModel> holidays = new List<HolidayDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select holidayid , description, date from holiday where description like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                holiday = new HolidayDataModel();
                                holiday.holidayid = reader.GetInt32(0);
                                holiday.description = reader.GetString(1);
                                holiday.date = reader.GetDateTime(2);
                                holidays.Add(holiday);


                            }
                        }
                    }
                }
                return holidays;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }



        public HolidayDataModel Insert(HolidayDataModel newHoliday)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Insert into holiday (description, date) values ('{newHoliday.description}','{newHoliday.date.ToString("yyyy-MM-dd")}'); select scope_identity()";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newHoliday.holidayid = idInserted;
                            return newHoliday;
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





        public HolidayDataModel Update(HolidayDataModel updHoliday)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.holiday SET description = '{updHoliday.description}', " +
                        $"date='{updHoliday.date.ToString("yyyy-MM-dd")}'" +
                        $"where holidayid = {updHoliday.holidayid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updHoliday;
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
                    string sqlStmt = $"DELETE FROM dbo.holiday Where holidayid={id}";
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





