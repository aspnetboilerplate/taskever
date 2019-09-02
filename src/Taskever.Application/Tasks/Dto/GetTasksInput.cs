using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Taskever.Tasks.Dto
{
    public class GetTasksInput : IPagedResultRequest, ICustomValidate
    {
        [Range(1, long.MaxValue)]
        public long AssignedUserId { get; set; }

        public List<TaskState> TaskStates { get; set; }

        public int SkipCount { get; set; }

        public int MaxResultCount { get; set; }

        public GetTasksInput()
        {
            MaxResultCount = int.MaxValue;
            TaskStates = new List<TaskState>();
        }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (AssignedUserId <= 0)
            {
                context.Results.Add(new ValidationResult("AssignedUserId must be a positive value!", new[] { "AssignedUserId" }));
            }
        }
    }
}