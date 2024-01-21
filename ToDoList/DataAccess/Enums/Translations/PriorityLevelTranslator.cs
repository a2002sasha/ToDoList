namespace ToDoList.DataAccess.Enums.Translations
{
	public static class PriorityLevelTranslator
	{
		private readonly static Dictionary<PriorityLevel, string> translations = new()
	{
		{ PriorityLevel.None, "Немає" },
		{ PriorityLevel.Low, "Низький" },
		{ PriorityLevel.Medium, "Середній" },
		{ PriorityLevel.High, "Високий" }
	};

		public static string Translate(PriorityLevel value)
		{
			return translations.ContainsKey(value) ? translations[value] : value.ToString();
		}
	}
}