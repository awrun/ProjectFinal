using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinal.Models

{
    public class CustomerDataModel
    {
        public int customerid { get; set; }

        public string customername { get; set; }

        public string gender { get; set; }

        public DateTime dob { get; set; }

        public string mobilenumber { get; set; }

        public string address { get; set; }

        public string city { get; set; }


        public string state { get; set; }
        public string pincode { get; set; }





        //Constructor
        public CustomerDataModel()
        {
            customerid = 0;
            customername = "";
            gender = "";
            dob = DateTime.Now;
            mobilenumber = "";
            address = "";
            city = "";
            state = "";
            pincode = "";
        }

        public bool IsValid()
        {
            //if (Id <= 0)
            //{
            //    return false;
            //}

            if (customername == null || customername.Trim() == "" || customername.Trim().Length > 20)
            {
                return false;
            }
            if (gender == null)
            {
                return false;
            }
            if (dob == null)
            {
                return false;
            }
            if (mobilenumber == null)
            {
                return false;
            }
            if (address == null)
            {
                return false;
            }
            if (city == null)
            {
                return false;
            }
            if (state == null)
            {
                return false;
            }
            if (pincode == null)
            {
                return false;
            }

            return true;
        }

    }
}