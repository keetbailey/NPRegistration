using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CampSite
    {
        public int Site_Id { get; set; }
        public int Campground_Id { get; set; }
        public int Site_Number { get; set; }
        public int Max_Occupancy { get; set; }
        public bool AccessibleBool { get; set; }
        public int Max_RV_Length { get; set; }
        public bool UtilitiesBool { get; set; }
        public string AccessibleString
        {
            get
            {
                if (AccessibleBool)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                };
            }
        }
        public string UtilitiesString
        {
            get
            {
                if (UtilitiesBool)
                {
                    return "Yes";
                }
                else
                {
                    return "N/A";
                };
            }
        }
    }
}
