using System.ComponentModel.DataAnnotations;
using ToDoList.DataAccess.Enums;

namespace ToDoList.ViewModel
{
    public class TaskViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Назва завдання не вказана!")]
        [Display(Name = "Вкажіть назву завдання")]
        public string Name { get; set; } = null!;

        [Display(Name = "Опишіть завдання")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Пріоритет завдання не вказаний!")]
        [Display(Name = "Пріоритет завдання")]
        public PriorityLevel PriorityLevel { get; set; }

        [Required(ErrorMessage = "Статус завдання не вказаний!")]
        [Display(Name = "Статус завдання")]
        public TaskState TaskState { get; set; }

        public string? TranslatedPriorityLevel { get; set; }
        public string? TranslatedTaskState { get; set; }

        [Required(ErrorMessage = "Вкажіть дату виконання завдання!")]
        [Display(Name = "Дата")]
        [UIHint("Date")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Вкажіть час виконання завдання!")]
        [Display(Name = "Час")]
        [UIHint("Time")]
        public TimeOnly EndTime { get; set; }
        public List<TaskViewModel>? UserTasks { get; set; }
        public FilterViewModel? FilterViewModel { get; set; }
        public SortViewModel? SortViewModel { get; set; }
    }
}
