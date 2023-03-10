using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERM.Constants;
using PERM.Data;
using PERM.Models;

namespace PERM.Controllers
{
    [Authorize(Roles = "SuperAdmin,PowerUser,HrAdmin ,BasicUser, ProjectManager_TeamLead")]

    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.department != null ? 
                          View(await _context.department.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.department'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.department == null)
            {
                return NotFound();
            }

            var department = await _context.department
                .FirstOrDefaultAsync(m => m.DeptID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptID,DeptNo,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.department == null)
            {
                return NotFound();
            }

            var department = await _context.department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DeptID,DeptNo,DepartmentName")] Department department)
        {
            if (id != department.DeptID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DeptID))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.department == null)
            {
                return NotFound();
            }

            var department = await _context.department
                .FirstOrDefaultAsync(m => m.DeptID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.department == null)
            {
                return Problem("Entity set 'ApplicationDbContext.department'  is null.");
            }
            var department = await _context.department.FindAsync(id);
            if (department != null)
            {
                _context.department.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(string id)
        {
          return (_context.department?.Any(e => e.DeptID == id)).GetValueOrDefault();
        }
    }
}
