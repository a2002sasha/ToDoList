namespace ToDoList.DataAccess.Enums.Translations
{
	public static class TaskStateTranslator
	{
		private readonly static Dictionary<TaskState, string> translations = new()
	{
		{ TaskState.Created, "Створено" },
		{ TaskState.InProcess, "В процесі" },
		{ TaskState.Completed, "Виконано" },
	};

		public static string Translate(TaskState value)
		{
			return translations.ContainsKey(value) ? translations[value] : value.ToString();
		}
	}
}
