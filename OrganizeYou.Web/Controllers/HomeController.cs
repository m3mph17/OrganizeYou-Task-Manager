using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizeYou.BLL.DTO;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.Web.Models;
using System.Diagnostics;

namespace OrganizeYou.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;

        public HomeController(ILogger<HomeController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            TaskObjectDTO taskDto = _taskService.GetTask(1);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskObjectDTO, TaskViewModel>()).CreateMapper();
            var task = mapper.Map<TaskObjectDTO, TaskViewModel>(taskDto);
            return View(task);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}