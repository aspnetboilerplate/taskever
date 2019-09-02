using System.Web.Mvc;
using Abp.Authorization;

namespace Taskever.Web.Mvc.Controllers
{
    [AbpAuthorize]
    public class HomeController : TaskeverController
    {
        public ActionResult Index()
        {
            //var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //var tenantId = prinicpal.Claims.Where(c => c.Type == "TenantId").Select(c => c.Value).SingleOrDefault();

            return View("Index");
        }
    }
}
