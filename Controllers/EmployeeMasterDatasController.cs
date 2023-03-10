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

    public class EmployeeMasterDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeMasterDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeMasterDatas
        public async Task<IActionResult> Index()
        {
              return _context.employeeMasterData != null ? 
                          View(await _context.employeeMasterData.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.employeeMasterData'  is null.");
        }

        // GET: EmployeeMasterDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterData = await _context.employeeMasterData
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeMasterData == null)
            {
                return NotFound();
            }

            return View(employeeMasterData);
        }

        // GET: EmployeeMasterDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeMasterDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,DeptID,EmployeeFirstName,EmployeeLastName,EmployeeAddress,EmployeeEmailAddress,EmployeePhone,JoiningDate,EmployeeCNIC,EmployeeSalary,EmployeeDesignation")] EmployeeMasterData employeeMasterData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeMasterData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeMasterData);
        }

        // GET: EmployeeMasterDatas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterData = await _context.employeeMasterData.FindAsync(id);
            if (employeeMasterData == null)
            {
                return NotFound();
            }
            return View(employeeMasterData);
        }

        // POST: EmployeeMasterDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeID,DeptID,EmployeeFirstName,EmployeeLastName,EmployeeAddress,EmployeeEmailAddress,EmployeePhone,JoiningDate,EmployeeCNIC,EmployeeSalary,EmployeeDesignation")] EmployeeMasterData employeeMasterData)
        {
            if (id != employeeMasterData.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMasterData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMasterDataExists(employeeMasterData.EmployeeID))
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
            return View(employeeMasterData);
        }

        // GET: EmployeeMasterDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.employeeMasterData == null)
            {
                return NotFound();
            }

            var employeeMasterData = await _context.employeeMasterData
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeMasterData == null)
            {
                return NotFound();
            }

            return View(employeeMasterData);
        }

        // POST: EmployeeMasterDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.employeeMasterData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.employeeMasterData'  is null.");
            }
            var employeeMasterData = await _context.employeeMasterData.FindAsync(id);
            if (employeeMasterData != null)
            {
                _context.employeeMasterData.Remove(employeeMasterData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMasterDataExists(string id)
        {
          return (_context.employeeMasterData?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
    }
}
