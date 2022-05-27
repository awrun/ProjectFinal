using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinal.Models

{
    public class ActivateDataModel
    {
        public int activateid { get; set; }

        public int customerid { get; set; }

        public int packageid { get; set; }

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string status { get; set; }







        //Constructor
        public ActivateDataModel()
        {
            activateid = 0;
            customerid = 0;
            packageid = 0;
            startdate = DateTime.Now;
            enddate = DateTime.Now;
            status = "";
        }

        public bool IsValid()
        {
            //if (Id <= 0)
            //{
            //    return false;
            //}

            if (customerid == null)
            {
                return false;
            }
            if (packageid == null)
            {
                return false;
            }
            if (startdate == null)
            {
                return false;
            }
            if (enddate == null)
            {
                return false;
            }
            if (status == null)
            {
                return false;
            }



            return true;
        }

    }
}