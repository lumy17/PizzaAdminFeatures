using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Cupons
{
    public class DeleteModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public DeleteModel(PizzaApp.Data.ApplicationDBContext context)
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

            var cupon = await _context.Cupon.FirstOrDefaultAsync(m => m.Id == id);

            if (cupon == null)
            {
                return NotFound();
            }
            else 
            {
                Cupon = cupon;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cupon == null)
            {
                return NotFound();
            }
            var cupon = await _context.Cupon.FindAsync(id);

            if (cupon != null)
            {
                Cupon = cupon;
                _context.Cupon.Remove(Cupon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
