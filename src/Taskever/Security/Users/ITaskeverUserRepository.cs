using Abp.Domain.Repositories;
namespace Taskever.Security.Users
{
    public interface ITaskeverUserRepository : IRepository<TaskeverUser, long>
    {

    }
}
