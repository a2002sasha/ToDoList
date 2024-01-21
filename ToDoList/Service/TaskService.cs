using Microsoft.EntityFrameworkCore;
using ToDoList.DataAccess.Context;
using ToDoList.DataAccess.Enums;
using ToDoList.Enums;
using ToDoList.ViewModel;

namespace ToDoList.Service
{
	public class TaskService : ITaskService
	{
		private readonly ToDoContext _context;

		public TaskService(ToDoContext context)
		{
			_context = context;
		}
		public IQueryable<DataAccess.Model.Task> GetTasksByUser(string username)
		{
			return _context.Tasks
				.Where(u => u.User.UserName == username)
				.Include(u => u.User);
		}

        public DataAccess.Model.Task GetTaskById(string username, Guid? id)
        {
            return GetTasksByUser(username).FirstOrDefault(u => u.Id == id);
        }

        public void Create(DataAccess.Model.Task task, string username)
		{

			var user = _context.Users.FirstOrDefault(t => t.UserName == username);

			_context.Tasks.Add(new DataAccess.Model.Task()
			{
				User = user,
				Name = task.Name,
				Description = task.Description,
				EndDate = task.EndDate,
				EndTime = task.EndTime,
				PriorityLevel = task.PriorityLevel,
				TaskState = task.TaskState,
			});

			_context.SaveChanges();
		}

		public void Edit(DataAccess.Model.Task task)
		{
			var userTask = _context.Tasks.FirstOrDefault(ut => ut.Id == task.Id);

			userTask.Name = task.Name;
			userTask.Description = task.Description;
			userTask.EndDate = task.EndDate;
			userTask.EndTime = task.EndTime;
			userTask.PriorityLevel = task.PriorityLevel;
			userTask.TaskState = task.TaskState;

			_context.SaveChanges();
		}

		public void Remove(Guid? taskId)
		{
			var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
			_context.Remove(task);

			_context.SaveChanges();
		}

		public IQueryable<DataAccess.Model.Task> GetFilteredTasks(string username, FilterViewModel filterViewModel)
		{
			var tasks = GetTasksByUser(username);

			if (filterViewModel.SelectedPriorityLevel != 0)
				tasks = tasks.Where(t => t.PriorityLevel == (PriorityLevel)filterViewModel.SelectedPriorityLevel);

			if (filterViewModel.SelectedTaskState != 0)
				tasks = tasks.Where(t => t.TaskState == (TaskState)filterViewModel.SelectedTaskState);

			if (!String.IsNullOrEmpty(filterViewModel.SelectedTaskName))
			{
				tasks = tasks.Where(p => p.Name.Contains(filterViewModel.SelectedTaskName));
			}

            return tasks;
		}

		public IQueryable<DataAccess.Model.Task> GetSortedTasks(IQueryable<DataAccess.Model.Task> tasks, SortState sortOrder = SortState.NameAsc)
		{
			tasks = sortOrder switch
			{
				SortState.NameDesc => tasks.OrderByDescending(s => s.Name),
				SortState.DescriptionAsc => tasks.OrderBy(s => s.Description),
				SortState.DescriptionDesc => tasks.OrderByDescending(s => s.Description),
				SortState.TaskLevelAsc => tasks.OrderBy(s => s.PriorityLevel),
				SortState.TaskLevelDesc => tasks.OrderByDescending(s => s.PriorityLevel),
				SortState.TaskStateAsc => tasks.OrderBy(s => s.TaskState),
				SortState.TaskStateDesc => tasks.OrderByDescending(s => s.TaskState),
				SortState.DateAsc => tasks.OrderBy(s => s.EndDate),
				SortState.DateDesc => tasks.OrderByDescending(s => s.EndDate),
				SortState.TimeAsc => tasks.OrderBy(s => s.EndTime),
				SortState.TimeDesc => tasks.OrderByDescending(s => s.EndTime),
				_ => tasks.OrderBy(s => s.Name)
			};

			return tasks;
		}
    }
}
