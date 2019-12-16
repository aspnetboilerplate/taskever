using System.Web.Mvc;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Extensions;

namespace Taskever.Web.Mvc.Controllers
{
    public class TaskeverController : AbpController
    {
        public TaskeverController()
        {
            LocalizationSourceName = "Taskever";
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.ActionDescriptor.GetMethodInfoOrNull();
            base.OnActionExecuting(filterContext);
        }
    }
}