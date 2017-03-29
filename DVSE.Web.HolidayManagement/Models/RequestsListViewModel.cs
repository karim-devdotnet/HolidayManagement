using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSE.Web.HolidayManagement.Models
{
    public class RequestsListViewModel
    {
        public IEnumerable<RequestViewModel> Requests { get; set; }
    }
}