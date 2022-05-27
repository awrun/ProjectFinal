using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinal.Models

{
    public class HolidayDataModel
    {   
        public int holidayid { get; set; }
        public string description { get; set; }

        public DateTime date { get; set; }


        //Constructor
        public HolidayDataModel()
        {
            holidayid = 0;
            description = "";
            date = DateTime.Now;
        }

        public bool IsValid()
        {
            //if (Id <= 0)
            //{
            //    return false;
            //}

            if (description == null)
            {
                return false;
            }

            if (date == null)
            {
                return false;
            }

            return true;
        }

    }
}