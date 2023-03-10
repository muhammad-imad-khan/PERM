using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PERM.Data;
using PERM.Models;

namespace PERM.Controllers
{
    
    public class _TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public _TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: _Tasks
        public async Task<IActionResult> Index()
        {
              return _context._tasks != null ? 
                          View(await _context._tasks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext._tasks'  is null.");
        }

        // GET: _Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _Tasks = await _context._tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (_Tasks == null)
            {
                return NotFound();
            }

            return View(_Tasks);
        }

        // GET: _Tasks/Create
        [Authorize(Roles= "SuperAdmin, BasicUser")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]

        // POST: _Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,TaskName,TaskDescription,TaskStatus,TaskType,TaskCreatedDate,TaskUpdatedDate,TaskDeadline,TaskCompletionDate,TaskAssignedTo,TaskAssignedBy")] _Tasks _Tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_Tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_Tasks);
        }
        [Authorize(Roles = "SuperAdmin")]
        // GET: _Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _Tasks = await _context._tasks.FindAsync(id);
            if (_Tasks == null)
            {
                return NotFound();
            }
            return View(_Tasks);
        }

        [Authorize(Roles = "SuperAdmin")]
        // POST: _Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,TaskName,TaskDescription,TaskStatus,TaskType,TaskCreatedDate,TaskUpdatedDate,TaskDeadline,TaskCompletionDate,TaskAssignedTo,TaskAssignedBy")] _Tasks _Tasks)
        {
            if (id != _Tasks.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_Tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_TasksExists(_Tasks.TaskID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_Tasks);
        }

        [Authorize(Roles = "SuperAdmin")]

        // GET: _Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context._tasks == null)
            {
                return NotFound();
            }

            var _Tasks = await _context._tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (_Tasks == null)
            {
                return NotFound();
            }

            return View(_Tasks);
        }

        [Authorize(Roles = "SuperAdmin")]

        // POST: _Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context._tasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext._tasks'  is null.");
            }
            var _Tasks = await _context._tasks.FindAsync(id);
            if (_Tasks != null)
            {
                _context._tasks.Remove(_Tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool _TasksExists(int id)
        {
          return (_context._tasks?.Any(e => e.TaskID == id)).GetValueOrDefault();
        }
    }
}
