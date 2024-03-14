using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models.ViewModels;
using Task = Todo.Models.Task;

namespace Todo.Controllers
{
    public class TasksController : Controller
    {
        private readonly TodoContext _context;

        public TasksController(TodoContext context)
        {
            _context = context;
        }

        // GET: Tasks1
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.Task.Include(t => t.Project);
            return View(await todoContext.ToListAsync());
        }

        // GET: Tasks1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        [HttpGet]
        public IActionResult Create()
        {
            TaskProjectViewModel taskProjectViewModel = new TaskProjectViewModel()
            {
                AllProjects = _context.Project.ToList(),
            };

            return View(taskProjectViewModel);
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProjectId,DueDate,Priority,Status")] Task task)
        {
            //Set relationship navigation based on selected ProjectId
            var project = _context.Project.Single(p => p.Id == task.ProjectId);
            task.ProjectId = project.Id;

            if (ModelState.IsValid)
            {
                _context.Task.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            TaskProjectViewModel taskProjectViewModel = new TaskProjectViewModel()
            {
                AllProjects = _context.Project.ToList(),
            };

            taskProjectViewModel.Project = task.Project;
            return View(taskProjectViewModel);
        }

        // GET: Tasks1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            TaskProjectViewModel taskProjectViewModel = new TaskProjectViewModel()
            {
                AllProjects = _context.Project.ToList(),
                Task = _context.Task.FirstOrDefault(task => task.Id == id),
            };

            //ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", task.ProjectId);
            return View(taskProjectViewModel);
        }

        // POST: Tasks1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProjectId,DueDate,Priority,Status")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Tasks", "Projects", new { id = task.ProjectId });
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", task.ProjectId);
            return View(task);
        }

        // GET: Tasks1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

  

        //POST: Tasks/Complete/5
        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                task.Status = Status.Completed;
                _context.Update(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }
    }
}
