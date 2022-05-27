using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFinal.Models

{
    public class PackageDataModel
    {
        public int packageid { get; set; }

        public string duration { get; set; }

        public int amount { get; set; }

        //Constructor
        public PackageDataModel()
        {
            packageid = 0;
            duration = "";
            amount = 0;
        }

        public bool IsValid()
        {
            //if (Id <= 0)
            //{
            //    return false;
            //}

            if(duration == null)
            {
                return false;
            }

            if (amount == null)
            {
                return false;
            }

            return true;
        }

    }
}