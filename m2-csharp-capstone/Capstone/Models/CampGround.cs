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
        public string Open_From_Month { get; set; }
        public string Open_To_Month { get; set; }

        public void FromMonthParse()
        {
            int monthInt = Open_From_Int;
            switch (monthInt)
            {
                case 1:
                    Open_From_Month = "January";
                    break;
                case 2:
                    Open_From_Month = "February";
                    break;
                case 3:
                    Open_From_Month = "March";
                    break;
                case 4:
                    Open_From_Month = "April";
                    break;
                case 5:
                    Open_From_Month = "May";
                    break;
                case 6:
                    Open_From_Month = "June";
                    break;
                case 7:
                    Open_From_Month = "July";
                    break;
                case 8:
                    Open_From_Month = "August";
                    break;
                case 9:
                    Open_From_Month = "September";
                    break;
                case 10:
                    Open_From_Month = "October";
                    break;
                case 11:
                    Open_From_Month = "November";
                    break;
                case 12:
                    Open_From_Month = "December";
                    break;
            }
        }
        public void ToMonthParse()
        {
            int monthInt = Open_To_Int;
            switch (monthInt)
            {
                case 1:
                    Open_From_Month = "January";
                    break;
                case 2:
                    Open_From_Month = "February";
                    break;
                case 3:
                    Open_From_Month = "March";
                    break;
                case 4:
                    Open_From_Month = "April";
                    break;
                case 5:
                    Open_From_Month = "May";
                    break;
                case 6:
                    Open_From_Month = "June";
                    break;
                case 7:
                    Open_From_Month = "July";
                    break;
                case 8:
                    Open_From_Month = "August";
                    break;
                case 9:
                    Open_From_Month = "September";
                    break;
                case 10:
                    Open_From_Month = "October";
                    break;
                case 11:
                    Open_From_Month = "November";
                    break;
                case 12:
                    Open_From_Month = "December";
                    break;
            }
        }
    }
}