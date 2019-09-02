using System.Collections.Generic;

namespace Taskever.Tasks.Dto
{
    public class GetTasksByImportanceOutput 
    {
        public IList<TaskDto> Tasks { get; set; }
    }
}