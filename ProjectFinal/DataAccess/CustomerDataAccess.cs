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
    internal class CustomerDataAccess
    {
      

        public string ErrorMessage { get; private set; }
        public List<CustomerDataModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select customerid , customername, gender, dob, mobilenumber, address, city, state,pincode from customer order by customerid asc";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerDataModel customer = new CustomerDataModel();

                                customer.customerid = reader.GetInt32(0);
                                customer.customername = reader.GetString(1);
                                customer.gender = reader.GetString(2);
                                customer.dob = reader.GetDateTime(3);
                                customer.mobilenumber = reader.GetString(4);
                                customer.address = reader.GetString(5);
                                customer.city = reader.GetString(6);
                                customer.state = reader.GetString(7);
                                customer.pincode = reader.GetString(8);





                                customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }






        public CustomerDataModel GetCustomerById(int id)

        {
            try
            {

                ErrorMessage = "";

                CustomerDataModel customer = null;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select customerid , customername, gender, dob, mobilenumber, address, city, state,pincode from customer where customerid={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new CustomerDataModel();
                                customer.customerid = reader.GetInt32(0);
                                customer.customername = reader.GetString(1);
                                customer.gender = reader.GetString(2);
                                customer.dob = reader.GetDateTime(3);
                                customer.mobilenumber = reader.GetString(4);
                                customer.address = reader.GetString(5);
                                customer.city = reader.GetString(6);
                                customer.state = reader.GetString(7);
                                customer.pincode = reader.GetString(8);


                            }
                        }
                    }
                }
                return customer;

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
                return null;
            }
        }







        public List<CustomerDataModel> GetCustomerByName(string name)
        {
            try
            {
                ErrorMessage = "";

                CustomerDataModel customer = null;
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select customerid , customername, gender, dob, mobilenumber, address, city, state,pincode from customer where customername like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                customer = new CustomerDataModel();
                                customer.customerid = reader.GetInt32(0);
                                customer.customername = reader.GetString(1);
                                customer.gender = reader.GetString(2);
                                customer.dob = reader.GetDateTime(3);
                                customer.mobilenumber = reader.GetString(4);
                                customer.address = reader.GetString(5);
                                customer.city = reader.GetString(6);
                                customer.state = reader.GetString(7);
                                customer.pincode = reader.GetString(8);
                                customers.Add(customer);


                            }
                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }



        public CustomerDataModel Insert(CustomerDataModel newCustomer)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Insert into customer (customername, gender, dob, mobilenumber, address, city, state,pincode) values ('{newCustomer.customername}','{newCustomer.gender}','{newCustomer.dob.ToString("yyyy-MM-dd")}','{newCustomer.mobilenumber}','{newCustomer.address}','{newCustomer.city}','{newCustomer.state}','{newCustomer.pincode}'); select scope_identity()";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newCustomer.customerid = idInserted;
                            return newCustomer;
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





        public CustomerDataModel Update(CustomerDataModel updCustomer)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.customer SET customername = '{updCustomer.customername}', " +
                        $"gender = '{updCustomer.gender}'," +
                        $"dob='{updCustomer.dob.ToString("yyyy-MM-dd")}'," +
                         $"mobilenumber = '{updCustomer.mobilenumber}'," +
                         $"address='{updCustomer.address}'," +
                        $"city='{updCustomer.city}'," +
                        $"state='{updCustomer.state}'," +
                         $"pincode = '{updCustomer.pincode}' " +
                        $"where customerid = {updCustomer.customerid}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updCustomer;
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
                    string sqlStmt = $"DELETE FROM dbo.customer Where customerid={id}";
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





