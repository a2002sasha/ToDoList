using ToDoList.Enums;
using ToDoList.ViewModel;

namespace ToDoList.Service
{
    public interface ITaskService
    {
        IQueryable<DataAccess.Model.Task> GetTasksByUser(string username);
        DataAccess.Model.Task GetTaskById(string username, Guid? id);
        void Create(DataAccess.Model.Task task, string username);
        void Edit(DataAccess.Model.Task task);
        void Remove(Guid? taskId);
        IQueryable<DataAccess.Model.Task> GetFilteredTasks(string username, FilterViewModel filterViewModel);
		IQueryable<DataAccess.Model.Task> GetSortedTasks(IQueryable<DataAccess.Model.Task> tasks, SortState sortOrder = SortState.NameAsc);
	}
}
