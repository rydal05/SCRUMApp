using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRUMApp.Data;
using SCRUMApp.Models;

namespace SCRUMApp.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuggestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suggestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suggestion.ToListAsync());
        }

        // GET: Suggestions/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Suggestions/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.Suggestion.Where(j=>j.mechanic.Contains(SearchPhrase) | j.explanation.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Suggestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // GET: Suggestions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suggestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,mechanic,explanation")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suggestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suggestion);
        }

        // GET: Suggestions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestion.FindAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,mechanic,explanation")] Suggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suggestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionExists(suggestion.Id))
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
            return View(suggestion);
        }

        // GET: Suggestions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // POST: Suggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suggestion = await _context.Suggestion.FindAsync(id);
            if (suggestion != null)
            {
                _context.Suggestion.Remove(suggestion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuggestionExists(int id)
        {
            return _context.Suggestion.Any(e => e.Id == id);
        }
    }
}
