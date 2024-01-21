using ToDoList.Enums;
using ToDoList.ViewModel;

namespace ToDoList.Service
{
    public interface ITaskService
    {
        IQueryable<DataAccess.Model.Task> GetTasksByUser(string username);
        IQueryable<DataAccess.Model.Task> GetTaskById(string username, Guid? id);
        Task<bool> CreateAsync(DataAccess.Model.Task task, string username);
		Task<bool> EditAsync(DataAccess.Model.Task task);
		Task<bool> RemoveAsync(Guid? taskId);
        IQueryable<DataAccess.Model.Task> GetFilteredTasks(string username, FilterViewModel filterViewModel);
		IQueryable<DataAccess.Model.Task> GetSortedTasks(IQueryable<DataAccess.Model.Task> tasks, SortViewModel sortViewModel);
	}
}
