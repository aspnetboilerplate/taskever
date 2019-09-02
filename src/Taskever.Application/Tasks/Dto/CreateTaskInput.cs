using System.Web;
using Abp.Runtime.Validation;

namespace Taskever.Tasks.Dto
{
    public class CreateTaskInput : IShouldNormalize
    {
        public TaskDto Task { get; set; }

        public void Normalize()
        {
            Task.Title = HttpUtility.HtmlEncode(Task.Title);
            Task.Description = HttpUtility.HtmlEncode(Task.Description);
        }
    }
}