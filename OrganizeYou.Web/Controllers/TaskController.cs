using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizeYou.BLL.DTO;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.Web.Models;
using System.Threading.Tasks;

namespace OrganizeYou.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;
        private readonly IStatusService _statusService;

        public TaskController(ILogger<TaskController> logger,
            ITaskService taskService,
            IStatusService statusService)
        {
            _logger = logger;
            _taskService = taskService;
            _statusService = statusService;
        }

        public IActionResult AllTasks()
        {
            var tasksDto = _taskService.GetTasks().ToList();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObjectDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<List<TaskObjectDTO>, List<TaskViewModel>>(tasksDto);
            return View(taskViewModel);
        }

        public IActionResult Mytask(int id)
        {
            var taskDto = _taskService.GetTask(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObjectDTO, TaskViewModel>()).CreateMapper();
            var taskViewModel = mapper.Map<TaskObjectDTO, TaskViewModel>(taskDto);
            return View(taskViewModel);
        }

        public IActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(TaskViewModel task)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskViewModel, TaskObjectDTO>()).CreateMapper();
            TaskObjectDTO taskDto = mapper.Map<TaskViewModel, TaskObjectDTO>(task);
            _taskService.CreateTask(taskDto);
            return RedirectToAction("AllTasks");
        }

        public IActionResult DeleteTask(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("AllTasks");
        }

        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            TaskObjectDTO task = _taskService.GetTask(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObjectDTO, TaskViewModel>()).CreateMapper();
            TaskViewModel taskViewModel = mapper.Map<TaskObjectDTO, TaskViewModel>(task);

            List<StatusDTO> statuses = _statusService.GetStatuses().ToList();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<StatusDTO, StatusViewModel>()).CreateMapper();
            var statusViewModels = mapper.Map<List<StatusDTO>, List<StatusViewModel>>(statuses);


            ViewData["Statuses"] = statusViewModels;
            return View(taskViewModel);
        }

        [HttpPost]
        public IActionResult UpdateTask(TaskViewModel task)
        {
            // TODO Update task code
            TaskObjectDTO taskObjectDTO = task.Convert(_statusService.GetStatus(task.Status));

            _taskService.UpdateTask(taskObjectDTO);
            return RedirectToRoute(new { controller = "Task", action = "Mytask", id = task.Id });
        }
    }
}
