using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoItems.Data;
using ToDoItems.Models;

namespace ToDoItems
{
    public class ToDoListEntriesController : Controller
    {
        private readonly DatabaseContext _context;

        public ToDoListEntriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ToDoListEntries
        public async Task<IActionResult> Index()
        {
              return _context.ToDoListEntry != null ? 
                          View(await _context.ToDoListEntry.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.ToDoListEntry'  is null.");
        }

        // GET: ToDoListEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoListEntry == null)
            {
                return NotFound();
            }

            var toDoListEntry = await _context.ToDoListEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListEntry == null)
            {
                return NotFound();
            }

            return View(toDoListEntry);
        }

        // GET: ToDoListEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoListEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateDue,StatusType")] ToDoListEntry toDoListEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoListEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoListEntry);
        }

        // GET: ToDoListEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoListEntry == null)
            {
                return NotFound();
            }

            var toDoListEntry = await _context.ToDoListEntry.FindAsync(id);
            if (toDoListEntry == null)
            {
                return NotFound();
            }
            return View(toDoListEntry);
        }

        // POST: ToDoListEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateDue,StatusType")] ToDoListEntry toDoListEntry)
        {
            if (id != toDoListEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoListEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListEntryExists(toDoListEntry.Id))
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
            return View(toDoListEntry);
        }

        // GET: ToDoListEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoListEntry == null)
            {
                return NotFound();
            }

            var toDoListEntry = await _context.ToDoListEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListEntry == null)
            {
                return NotFound();
            }

            return View(toDoListEntry);
        }

        // POST: ToDoListEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoListEntry == null)
            {
                return Problem("Entity set 'DatabaseContext.ToDoListEntry'  is null.");
            }
            var toDoListEntry = await _context.ToDoListEntry.FindAsync(id);
            if (toDoListEntry != null)
            {
                _context.ToDoListEntry.Remove(toDoListEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListEntryExists(int id)
        {
          return (_context.ToDoListEntry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
