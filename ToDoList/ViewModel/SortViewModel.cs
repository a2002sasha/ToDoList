using ToDoList.Enums;

namespace ToDoList.ViewModel
{
	public class SortViewModel
	{
		public SortState NameSort { get; private set; }
		public SortState DescriptionSort { get; private set; }
		public SortState PriorityLevelSort { get; private set; }
		public SortState TaskStateSort { get; private set; }
		public SortState DateSort { get; private set; }
		public SortState TimeSort { get; private set; }
		public SortState SelectedSortState { get; set; } = SortState.TaskStateAsc;

		public void InitializeSortProperties()
		{
			NameSort = SelectedSortState == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
			DescriptionSort = SelectedSortState == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
			PriorityLevelSort = SelectedSortState == SortState.TaskLevelAsc ? SortState.TaskLevelDesc : SortState.TaskLevelAsc;
			TaskStateSort = SelectedSortState == SortState.TaskStateAsc ? SortState.TaskStateDesc : SortState.TaskStateAsc;
			DateSort = SelectedSortState == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
			TimeSort = SelectedSortState == SortState.TimeAsc ? SortState.TimeDesc : SortState.TimeAsc;
		}
	}
}
