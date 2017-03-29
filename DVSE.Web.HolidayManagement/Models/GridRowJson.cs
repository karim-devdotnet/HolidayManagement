using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSE.Web.HolidayManagement.Models
{
    public class GridRowJson
    {
        public int id { get; set; }

        public object[] cell { get; set; }
    }
}