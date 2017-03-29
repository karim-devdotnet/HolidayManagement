using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSE.Web.HolidayManagement.Models
{
    public class GridDataJson
    {
        //public int total { get; set; }

        //public int page { get; set; }

        public int records { get; set; }

        public List<GridRowJson> rows { get; set; }
    }
}