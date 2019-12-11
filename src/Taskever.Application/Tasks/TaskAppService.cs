using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.ObjectMapping;
using Abp.UI;
using Taskever.Security.Users;
using Taskever.Tasks.Dto;
using Taskever.Tasks.Events;

namespace Taskever.Tasks
{
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        private readonly ITaskPolicy _taskPolicy;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskeverUserRepository _userRepository;
        private readonly IEventBus _eventBus;
        private readonly IObjectMapper _objectMapper;

        public TaskAppService(
            ITaskPolicy taskPolicy,
            ITaskRepository taskRepository,
            ITaskeverUserRepository userRepository,
            IEventBus eventBus,
            IObjectMapper objectMapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _eventBus = eventBus;
            _objectMapper = objectMapper;
            _taskPolicy = taskPolicy;
        }

        public GetTaskOutput GetTask(GetTaskInput input)
        {
            var currentUser = _userRepository.Load(AbpSession.UserId.Value);
            var task = _taskRepository.FirstOrDefault(input.Id);

            if (task == null)
            {
                throw new UserFriendlyException("Can not found the task!");
            }

            if (!_taskPolicy.CanSeeTasksOfUser(currentUser, task.AssignedUser))
            {
                throw new UserFriendlyException("Can not see tasks of " + task.AssignedUser.Name);
            }

            if (task.AssignedUser.Id != currentUser.Id && task.Privacy == TaskPrivacy.Private)
            {
                throw new UserFriendlyException("Can not see this task since it's private!");
            }

            return new GetTaskOutput
            {
                Task = _objectMapper.Map<TaskWithAssignedUserDto>(task),
                IsEditableByCurrentUser = _taskPolicy.CanUpdateTask(currentUser, task)
            };
        }

        public virtual GetTasksOutput GetTasks(GetTasksInput input)
        {
            var query = CreateQueryForAssignedTasksOfUser(input.AssignedUserId);
            if (!input.TaskStates.IsNullOrEmpty())
            {
                query = query.Where(task => input.TaskStates.Contains(task.State));
            }

            query = query
                .OrderByDescending(task => task.Priority)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            return new GetTasksOutput
            {
                Tasks = query.ToList().Select(x => _objectMapper.Map<TaskDto>(x)).ToList()
            };
        }

        public GetTasksByImportanceOutput GetTasksByImportance(GetTasksByImportanceInput input)
        {
            var query = CreateQueryForAssignedTasksOfUser(input.AssignedUserId);
            query = query
                .Where(task => task.State != TaskState.Completed)
                .OrderByDescending(task => task.Priority)
                .ThenByDescending(task => task.State)
                .ThenByDescending(task => task.CreationTime)
                .Take(input.MaxResultCount);

            return new GetTasksByImportanceOutput
            {
                Tasks = query.ToList().Select(x => _objectMapper.Map<TaskDto>(x)).ToList()
            };
        }

        //[AbpAuthorize(TaskeverPermissions.CreateTask)]
        public virtual CreateTaskOutput CreateTask(CreateTaskInput input)
        {
            //Get entities from database
            var creatorUser = _userRepository.Get(AbpSession.UserId.Value);
            var assignedUser = _userRepository.Get(input.Task.AssignedUserId);

            if (!_taskPolicy.CanAssignTask(creatorUser, assignedUser))
            {
                throw new UserFriendlyException("You can not assign task to this user!");
            }

            //Create the task
            var taskEntity = _objectMapper.Map<Task>(input.Task);

            taskEntity.CreatorUserId = creatorUser.Id;
            taskEntity.AssignedUser = _userRepository.Load(input.Task.AssignedUserId);
            taskEntity.State = TaskState.New;

            if (taskEntity.AssignedUser.Id != creatorUser.Id && taskEntity.Privacy == TaskPrivacy.Private)
            {
                throw new ApplicationException("A user can not assign a private task to another user!");
            }

            _taskRepository.Insert(taskEntity);

            Logger.Debug("Created " + taskEntity);

            _eventBus.Trigger(this, new EntityCreatedEventData<Task>(taskEntity));

            return new CreateTaskOutput
            {
                Task = _objectMapper.Map<TaskDto>(taskEntity)
            };
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            var task = _taskRepository.FirstOrDefault(input.Id);
            if (task == null)
            {
                throw new Exception("Can not found the task!");
            }

            var currentUser = _userRepository.Load(AbpSession.UserId.Value); //TODO: Add method LoadCurrentUser and GetCurrentUser ???
            if (!_taskPolicy.CanUpdateTask(currentUser, task))
            {
                throw new UserFriendlyException("You can not update this task!");
            }

            if (task.AssignedUser.Id != input.AssignedUserId)
            {
                var userToAssign = _userRepository.Load(input.AssignedUserId);

                if (!_taskPolicy.CanAssignTask(currentUser, userToAssign))
                {
                    throw new UserFriendlyException("You can not assign task to this user!");
                }

                task.AssignedUser = userToAssign;
            }

            var oldTaskState = task.State;

            //TODO: Can we use Auto mapper?

            task.Description = input.Description;
            task.Priority = (TaskPriority)input.Priority;
            task.State = (TaskState)input.State;
            task.Privacy = (TaskPrivacy)input.Privacy;
            task.Title = input.Title;

            if (oldTaskState != TaskState.Completed && task.State == TaskState.Completed)
            {
                _eventBus.Trigger(this, new TaskCompletedEventData(task));
            }
        }

        public DeleteTaskOutput DeleteTask(DeleteTaskInput input)
        {
            var task = _taskRepository.FirstOrDefault(input.Id);
            if (task == null)
            {
                throw new Exception("Can not found the task!");
            }

            var currentUser = _userRepository.Load(AbpSession.UserId.Value);
            if (!_taskPolicy.CanDeleteTask(currentUser, task))
            {
                throw new UserFriendlyException("You can not delete this task!");
            }

            _taskRepository.Delete(task);

            return new DeleteTaskOutput();
        }

        private IQueryable<Task> CreateQueryForAssignedTasksOfUser(long assignedUserId)
        {
            var currentUser = _userRepository.Load(AbpSession.UserId.Value);
            var userOfTasks = _userRepository.Load(assignedUserId);

            if (!_taskPolicy.CanSeeTasksOfUser(currentUser, userOfTasks))
            {
                throw new ApplicationException("Can not see tasks of user");
            }

            var query = _taskRepository
                .GetAll()
                .Where(task => task.AssignedUser.Id == assignedUserId);

            if (currentUser.Id != userOfTasks.Id)
            {
                query = query.Where(task => task.Privacy != TaskPrivacy.Private);
            }

            return query;
        }
    }
}