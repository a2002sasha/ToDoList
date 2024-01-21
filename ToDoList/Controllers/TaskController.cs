using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(FilterViewModel filterViewModel, SortState sortOrder = SortState.NameAsc)
        {
            var username = HttpContext.User.Identity?.Name;

            var filteredTasks = _taskService.GetFilteredTasks(username!, filterViewModel);

            var sortedAndFilteredTasks = await _taskService.GetSortedTasks(filteredTasks, sortOrder).ToListAsync();

            var userTasks = _mapper.Map<List<TaskViewModel>>(sortedAndFilteredTasks);

            var viewModel = new TaskViewModel()
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = filterViewModel,
                UserTasks = userTasks
            };

            HttpContext.Session.SetString("SortOrder", ((int)sortOrder).ToString());

			return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(FilterViewModel filterViewModel)
        {
            var model = new TaskViewModel { FilterViewModel = filterViewModel };

			var sortOrder = (SortState)Convert.ToInt32(HttpContext.Session.GetString("SortOrder"));

            ViewBag.SortOrder = sortOrder;

			return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel model, FilterViewModel filterViewModel, SortState sortOrder)
        {
            if (ModelState.IsValid)
            {
                var task = _mapper.Map<DataAccess.Model.Task>(model);

                var username = HttpContext.User.Identity?.Name;

                var result = await _taskService.CreateAsync(task, username!);

                return Json(new
                {
                    Success = result,
                    Href = Url.Action("Index", new
                    {
                        selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                        selectedTaskName = filterViewModel.SelectedTaskName,
                        selectedTaskState = filterViewModel.SelectedTaskState,
                        sortOrder = sortOrder
                    })
                });
            }

            model.FilterViewModel = filterViewModel;
            ViewBag.SortState = sortOrder;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id, FilterViewModel filterViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var username = HttpContext.User.Identity?.Name;

            var userTask = await _taskService.GetTaskById(username!, id).FirstOrDefaultAsync();

            if (userTask == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskViewModel>(userTask);
            model.FilterViewModel = filterViewModel;

            var sortOrder = (SortState)Convert.ToInt32(HttpContext.Session.GetString("SortOrder"));

            ViewBag.SortOrder = sortOrder;

            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Guid id, TaskViewModel model, FilterViewModel filterViewModel, SortState sortOrder)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var task = _mapper.Map<DataAccess.Model.Task>(model);

                var result = await _taskService.EditAsync(task);

                return Json(new
                {
                    Success = result,
                    Href = Url.Action("Index", new
                    {
                        selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                        selectedTaskName = filterViewModel.SelectedTaskName,
                        selectedTaskState = filterViewModel.SelectedTaskState,
                        sortOrder = sortOrder
                    })
                });
            }

            model.FilterViewModel = filterViewModel;
            ViewBag.SortState = sortOrder;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id, FilterViewModel filterViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var username = HttpContext.User.Identity?.Name;

            var userTask = await _taskService.GetTaskById(username!, id).FirstOrDefaultAsync();

            if (userTask == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskViewModel>(userTask);

            model.TranslatedPriorityLevel = PriorityLevelTranslator.Translate(model.PriorityLevel);
            model.TranslatedTaskState = TaskStateTranslator.Translate(model.TaskState);
            model.FilterViewModel = filterViewModel;

            var sortOrder = (SortState)Convert.ToInt32(HttpContext.Session.GetString("SortOrder"));

            ViewBag.SortOrder = sortOrder;

            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, FilterViewModel filterViewModel, SortState sortOrder)
        {
            var result = await _taskService.RemoveAsync(id);

            return Json(new
            {
                Success = result,
                Href = Url.Action("Index", new
                {
                    selectedPriorityLevel = filterViewModel.SelectedPriorityLevel,
                    selectedTaskName = filterViewModel.SelectedTaskName,
                    selectedTaskState = filterViewModel.SelectedTaskState,
                    sortOrder = sortOrder
                })
            });
        }
    }
}