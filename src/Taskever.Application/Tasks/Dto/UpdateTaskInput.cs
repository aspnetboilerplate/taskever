using System.Web;
using Abp.Runtime.Validation;

namespace Taskever.Tasks.Dto
{
    public class UpdateTaskInput :  ICustomValidate
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long AssignedUserId { get; set; }

        public byte Priority { get; set; }

        public byte State { get; set; }

        public byte Privacy { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            Title = HttpUtility.HtmlEncode(Title);
            Description = HttpUtility.HtmlEncode(Description);
        }
    }
}