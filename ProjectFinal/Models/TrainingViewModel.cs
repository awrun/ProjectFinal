using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinal.Models

{
    public class TrainingViewModel
    {
        public int trainingid { get; set; }
        public int trainerid { get; set; }
        public string trainername { get; set; }

        public int customerid { get; set; }

        public string customername { get; set; }

        public int packageid { get; set; }
        public int amount { get; set; }

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string status { get; set; }







        //Constructor
        public TrainingViewModel()
        {
            trainingid = 0;
            trainerid = 0;
            trainername = "";
            customerid = 0;
            customername = "";
            packageid = 0;
            amount = 0;
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
            if (trainerid == null)
            {
                return false;
            }
            if (trainername == null)
            {
                return false;
            }
            if (customerid == null)
            {
                return false;
            }
            if (customername == null)
            {
                return false;
            }
            if (packageid == null)
            {
                return false;
            }
            if (amount == null)
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