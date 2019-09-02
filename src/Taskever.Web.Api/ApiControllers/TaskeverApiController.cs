using Abp.WebApi.Controllers;

namespace Taskever.Web.ApiControllers
{
    public class TaskeverApiController : AbpApiController
    {
        public TaskeverApiController()
        {
            LocalizationSourceName = "Taskever"; //TODO: Make constant!
        }
    }
}
