using FakeAPI.Models;
using FakeAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FakeAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoRepo _repo;

        public HomeController(ILogger<HomeController> logger, IToDoRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.ViewAllTodos());
        }

        public IActionResult AddToDo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToDo(ToDo newTodo)
        {
            newTodo.UserId = 1;
            newTodo.IsCompleted = false;

            await _repo.AddToDo(newTodo);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateToDo(int id)
        {
            var todo = await _repo.ViewOneToDo(id);

            if (todo is null)
                return NotFound();

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateToDo(ToDo newTodo)
        {
            var test = await _repo.UpdateTodo(newTodo.Id, newTodo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteToDo(int id)
        {
            await _repo.DeleteToDo(id);
            return RedirectToAction("Index");
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