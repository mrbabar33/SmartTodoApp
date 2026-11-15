using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTodoApp.Models;

namespace SmartTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Database ko Controller ke andar inject kiya gaya hai
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Saare Tasks ko Database se nikal kar screen par dikhana
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.TodoItems.ToListAsync();
            return View(tasks);
        }

        // 2. Naya Task Add karne ka Action
        [HttpPost]
        public async Task<IActionResult> Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var todo = new TodoItem { Title = title, IsCompleted = false };
                _context.TodoItems.Add(todo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // 3. Task ko Delete karne ka Action
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo != null)
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
            }
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