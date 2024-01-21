using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Configuration;
using ToDoList.DataAccess.Enums.Translations;
using ToDoList.Enums;
using ToDoList.Service;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
            StyleConfig.UseBootstrap = false;
        }

        [HttpGet]
        public IActionResult Index(FilterViewModel filterViewModel, SortState sortOrder = SortState.NameAsc)
        {
            var username = HttpContext.User.Identity?.Name;

            var filteredTasks = _taskService.GetFilteredTasks(username!, filterViewModel);

            var sortedAndFilteredTasks = _taskService.GetSortedTasks(filteredTasks, sortOrder);

            var userTasks = _mapper.Map<List<TaskViewModel>>(sortedAndFilteredTasks);

            var viewModel = new TaskViewModel()
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = filterViewModel,
                UserTasks = userTasks
            };

			TempData["SortOrder"] = sortOrder;

			return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create(FilterViewModel filterViewModel)
        {
            var model = new TaskViewModel { FilterViewModel = filterViewModel };

			var sortOrder = (SortState)TempData["SortOrder"];

            ViewBag.SortOrder = sortOrder;

			return View(model);
        }

        [HttpPost]
        public ActionResult Create(TaskViewModel model, FilterViewModel filterViewModel, SortState sortOrder)
        {
            if (ModelState.IsValid)
            {
                var task = _mapper.Map<DataAccess.Model.Task>(model);

                var username = HttpContext.User.Identity?.Name;
                _taskService.Create(task, username!);

                return RedirectToAction("Index", new
                {
                    selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                    selectedTaskName = filterViewModel.SelectedTaskName,
                    selectedTaskState = filterViewModel.SelectedTaskState,
                    sortOrder = sortOrder
                });
            }

            model.FilterViewModel = filterViewModel;
            ViewBag.SortState = sortOrder;

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid? id, FilterViewModel filterViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var username = HttpContext.User.Identity?.Name;

            var userTask = _taskService.GetTaskById(username!, id);

            if (userTask == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskViewModel>(userTask);
            model.FilterViewModel = filterViewModel;

            var sortOrder = (SortState)TempData["SortOrder"];

            ViewBag.SortOrder = sortOrder;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, TaskViewModel model, FilterViewModel filterViewModel, SortState sortOrder)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var task = _mapper.Map<DataAccess.Model.Task>(model);

                _taskService.Edit(task);

                return RedirectToAction("Index", new
                {
                    selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                    selectedTaskName = filterViewModel.SelectedTaskName,
                    selectedTaskState = filterViewModel.SelectedTaskState,
                    sortOrder = sortOrder
                });
            }

            model.FilterViewModel = filterViewModel;
            ViewBag.SortState = sortOrder;

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(Guid? id, FilterViewModel filterViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var username = HttpContext.User.Identity?.Name;

            var userTask = _taskService.GetTaskById(username!, id);

            if (userTask == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskViewModel>(userTask);

            model.TranslatedPriorityLevel = PriorityLevelTranslator.Translate(model.PriorityLevel);
            model.TranslatedTaskState = TaskStateTranslator.Translate(model.TaskState);
            model.FilterViewModel = filterViewModel;

            var sortOrder = (SortState)TempData["SortOrder"];

            ViewBag.SortOrder = sortOrder;

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id, FilterViewModel filterViewModel, SortState sortOrder)
        {
            _taskService.Remove(id);

            return RedirectToAction("Index", new
            {
                selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                selectedTaskName = filterViewModel.SelectedTaskName,
                selectedTaskState = filterViewModel.SelectedTaskState,
                sortOrder = sortOrder
            });
        }
    }
}