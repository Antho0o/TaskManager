using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManagerContext _context;

        public TasksController(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
    string? search,
    string? status,
    string? priority)
        {
            // Get ALL tasks for dashboard statistics
            var allTasks = await _context.Tasks.ToListAsync();

            // Dashboard statistics
            ViewData["TotalTasks"] = allTasks.Count;
            ViewData["CompletedTasks"] = allTasks.Count(t => t.IsCompleted);
            ViewData["PendingTasks"] = allTasks.Count(t => !t.IsCompleted);
            ViewData["HighPriorityTasks"] =
                allTasks.Count(t => t.Priority == "High" && !t.IsCompleted);

            // Start filtering
            var query = _context.Tasks.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(t =>
                    t.Title.Contains(search) ||
                    (t.Description != null && t.Description.Contains(search)));
            }

            // Status
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status.Equals("completed", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(t => t.IsCompleted);
                }
                else if (status.Equals("pending", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(t => !t.IsCompleted);
                }
            }

            // Priority
            if (!string.IsNullOrWhiteSpace(priority) &&
                !priority.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(t => t.Priority == priority);
            }

            var tasks = await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            ViewData["Search"] = search;
            ViewData["Status"] = status ?? "all";
            ViewData["Priority"] = priority ?? "all";

            return View(tasks);
        }

        // GET: /Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        // GET: /Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        // POST: /Tasks/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = !task.IsCompleted;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: /Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}