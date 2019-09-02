namespace Taskever.Tasks.Dto
{
    public class GetTaskOutput 
    {
        public TaskWithAssignedUserDto Task { get; set; }

        public bool IsEditableByCurrentUser { get; set; }
        
        public bool IsDeletableByCurrentUser { get; set; }
    }
}