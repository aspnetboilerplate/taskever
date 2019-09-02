using System.Collections.Generic;

namespace Taskever.Tasks.Dto
{
    public class GetTasksOutput 
    {
        public IList<TaskDto> Tasks { get; set; }
    }
}