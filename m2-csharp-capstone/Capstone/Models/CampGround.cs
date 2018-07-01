using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CampGround
    {

        public int Campground_Id { get; set; }
        public int Park_Id { get; set; }
        public string Name { get; set; }
        public int Open_From_Int { get; set; }
        public int Open_To_Int { get; set; }
        public decimal Daily_Fee { get; set; }
        public string Open_From_Month
        {
            get
            {
                return MonthParse(Open_From_Int);
            }
        }
        public string Open_To_Month
        {
            get
            {
                return MonthParse(Open_To_Int);
            }
        }

        private string MonthParse(int open_From_Int)
        {
            string output = "";
            switch (open_From_Int)
            {
                case 1:
                    output = "January";
                    break;
                case 2:
                    output = "February";
                    break;
                case 3:
                    output = "March";
                    break;
                case 4:
                    output = "April";
                    break;
                case 5:
                    output = "May";
                    break;
                case 6:
                    output = "June";
                    break;
                case 7:
                    output = "July";
                    break;
                case 8:
                    output = "August";
                    break;
                case 9:
                    output = "September";
                    break;
                case 10:
                    output = "October";
                    break;
                case 11:
                    output = "November";
                    break;
                case 12:
                    output = "December";
                    break;
            }
            return output;
        }
    }
}