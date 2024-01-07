using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Cupons
{
    public class EditModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public EditModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cupon Cupon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cupon == null)
            {
                return NotFound();
            }

            var cupon =  await _context.Cupon.FirstOrDefaultAsync(m => m.Id == id);
            if (cupon == null)
            {
                return NotFound();
            }
            Cupon = cupon;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponExists(Cupon.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CuponExists(int id)
        {
          return (_context.Cupon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
