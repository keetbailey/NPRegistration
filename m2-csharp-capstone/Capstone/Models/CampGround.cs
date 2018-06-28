using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class CampGround
    {
        public int Campground_Id {get; set; }
        public int Park_Id {get; set; }
        public string Name {get; set; }
        public int Open_From_mm {get; set; }
        public int Open_To_mm {get; set; }
        public decimal Daly_Fee {get; set; }
    }
}
