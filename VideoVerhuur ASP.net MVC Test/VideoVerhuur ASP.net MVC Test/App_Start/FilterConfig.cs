using System.Web;
using System.Web.Mvc;

namespace VideoVerhuur_ASP.net_MVC_Test
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
