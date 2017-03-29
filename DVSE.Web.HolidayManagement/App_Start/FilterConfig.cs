using System.Web;
using System.Web.Mvc;

namespace DVSE.Web.HolidayManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}