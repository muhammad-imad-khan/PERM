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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
              return _context.attendance != null ? 
                          View(await _context.attendance.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.attendance'  is null.");
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.attendance
                .FirstOrDefaultAsync(m => m.AttendanceID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceID,EmployeeID,Status,AttendanceDate,AttendanceInTime,AttendanceOutTime")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AttendanceID,EmployeeID,Status,AttendanceDate,AttendanceInTime,AttendanceOutTime")] Attendance attendance)
        {
            if (id != attendance.AttendanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceID))
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
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.attendance
                .FirstOrDefaultAsync(m => m.AttendanceID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.attendance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.attendance'  is null.");
            }
            var attendance = await _context.attendance.FindAsync(id);
            if (attendance != null)
            {
                _context.attendance.Remove(attendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(string id)
        {
          return (_context.attendance?.Any(e => e.AttendanceID == id)).GetValueOrDefault();
        }
    }
}
